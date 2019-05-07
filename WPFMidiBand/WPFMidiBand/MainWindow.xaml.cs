using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sanford.Multimedia.Midi.UI;
using Sanford.Multimedia.Midi;

using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WPFMidiBand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region atrributes
        private bool scrolling = false;
        private bool playing = false;
        private bool closing = false;
        private int outDeviceID = 0;
        private const int LowNoteID = 21;
        private const int HighNoteID = 109;
        private OutputDeviceDialog outDialog = new OutputDeviceDialog();
        private OutputDevice outDevice;
        private Sequence sequence1 = new Sequence();
        private Sequencer sequencer1 = new Sequencer();
        OpenFileDialog openMidiFileDialog = new OpenFileDialog();
        List<MessageDto> messageList = new List<MessageDto>();
        Dictionary<int, int> dicChannel = new Dictionary<int, int>();
        private bool dragStarted = false;

        string fileName = "";

        DispatcherTimer timer1 = new DispatcherTimer();

        #endregion atrributes

        #region ctor
        public MainWindow()
        {
            InitializeComponent();

            InitializeSequencer();

            var path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            openMidiFileDialog.InitialDirectory = System.IO.Path.Combine(path, "Midis");

            timer1.Interval = new TimeSpan(0, 0, 0, 1);
            timer1.Tick += new EventHandler(timer1_Tick);
        }
        #endregion ctor

        #region methods
        private void InitializeSequencer()
        {
            sequencer1.Stop();
            playing = false;

            outDevice = new OutputDevice(outDeviceID);
            this.sequence1.Format = 1;
            this.sequencer1.Position = 0;
            this.sequencer1.Sequence = this.sequence1;
            this.sequencer1.PlayingCompleted += new System.EventHandler(this.HandlePlayingCompleted);
            this.sequencer1.ChannelMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.ChannelMessageEventArgs>(this.HandleChannelMessagePlayed);
            this.sequencer1.Stopped += new System.EventHandler<Sanford.Multimedia.Midi.StoppedEventArgs>(this.HandleStopped);
            this.sequencer1.SysExMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.SysExMessageEventArgs>(this.HandleSysExMessagePlayed);
            this.sequencer1.Chased += new System.EventHandler<Sanford.Multimedia.Midi.ChasedEventArgs>(this.HandleChased);
            this.sequence1.LoadProgressChanged += HandleLoadProgressChanged;
            this.sequence1.LoadCompleted += HandleLoadCompleted;
        }

        private void ClearInstruments()
        {
            dicChannel.Clear();
            pianoControl1.Clear();
            guitarControl1.Clear();
            bassControl1.Clear();
        }
        #endregion methods

        #region events

        protected void OnLoad(EventArgs e)
        {
            if (OutputDevice.DeviceCount == 0)
            {
                System.Windows.MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Stop);

                Close();
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);

                    sequence1.LoadProgressChanged += HandleLoadProgressChanged;
                    sequence1.LoadCompleted += HandleLoadCompleted;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Stop);

                    Close();
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            closing = true;

            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            sequence1.Dispose();

            if (outDevice != null)
            {
                outDevice.Dispose();
            }

            outDialog.Dispose();

            base.OnClosed(e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleLoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            needleRotation.Angle = (e.ProgressPercentage / 100.0) * 360.0;
                        }
                    )
            );
        }

        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.Title = string.Format("WPF Midi Band - {0}", new FileInfo(fileName).Name);
            ClearInstruments();

            this.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                    delegate()
                    {
                        Storyboard sbClockClose = (Storyboard)FindResource("sbClockClose");
                        sbClockClose.Begin();
                    }
                )
            );

            this.Dispatcher.Invoke(
    System.Windows.Threading.DispatcherPriority.Normal,
        new Action(
        delegate()
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
            btnStart.IsEnabled = true;
            btnContinue.IsEnabled = true;
            btnStop.IsEnabled = true;
            btnOpen.IsEnabled = true;
            slider1.Value = 0;
        }
    )
);


            slider1.Value = 0;
            slider1.Maximum = sequence1.GetLength();

            sequencer1.Start();
            timer1.Start();
        }


        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            if (closing)
            {
                return;
            }

            outDevice.Send(e.Message);

            if (e.Message.Command == ChannelCommand.ProgramChange)
            {
                if (!dicChannel.ContainsKey(e.Message.MidiChannel))
                {
                    dicChannel.Add(e.Message.MidiChannel, e.Message.Data1);
                }
            }

            if (e.Message.MidiChannel == 9) // Channel 9 is reserved for drums
            {
                this.Dispatcher.Invoke(
                  System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            drumControl1.Send(e.Message);
                        }
                    ));
            }
            else if (dicChannel.ContainsKey(e.Message.MidiChannel))
            {
                switch (dicChannel[e.Message.MidiChannel])
                {
                    case (int)MIDIInstrument.AcousticGrandPiano://1
                    case (int)MIDIInstrument.BrightAcousticPiano://2
                    case (int)MIDIInstrument.ElectricGrandPiano://3
                    case (int)MIDIInstrument.HonkytonkPiano://4
                    case (int)MIDIInstrument.ElectricPiano1://5
                    case (int)MIDIInstrument.ElectricPiano2://6
                    case (int)MIDIInstrument.Harpsichord://7
                    case (int)MIDIInstrument.Clavinet://8
                        pianoControl1.Send(e.Message);
                        break;
                    case (int)MIDIInstrument.AcousticGuitarnylon://25
                    case (int)MIDIInstrument.AcousticGuitarsteel://26
                    case (int)MIDIInstrument.ElectricGuitarjazz://27
                    case (int)MIDIInstrument.ElectricGuitarclean://28
                    case (int)MIDIInstrument.ElectricGuitarmuted://29
                    case (int)MIDIInstrument.OverdrivenGuitar://30
                    case (int)MIDIInstrument.DistortionGuitar://31
                    case (int)MIDIInstrument.GuitarHarmonics://32
                        this.Dispatcher.Invoke(
                          System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(
                                delegate()
                                {
                                    guitarControl1.Send(e.Message);
                                }
                            ));
                        break;
                    case (int)MIDIInstrument.AcousticBass://33
                    case (int)MIDIInstrument.ElectricBassfinger://34
                    case (int)MIDIInstrument.ElectricBasspick://35
                    case (int)MIDIInstrument.FretlessBass://36
                    case (int)MIDIInstrument.SlapBass1://37
                    case (int)MIDIInstrument.SlapBass2://38
                    case (int)MIDIInstrument.SynthBass1://39
                    case (int)MIDIInstrument.SynthBass2://40
                        this.Dispatcher.Invoke(
                          System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(
                                delegate()
                                {
                                    bassControl1.Send(e.Message);
                                }
                            ));
                        break;
                    default:
                        pianoControl1.Send(e.Message);
                        break;
                }
            }
        }

        private void HandleChased(object sender, ChasedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
            outDevice.Send(e.Message); //Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandlePlayingCompleted(object sender, EventArgs e)
        {
            var cArray = new string(' ', 88).ToCharArray();
            List<string> noteList = new List<string>();
            List<string> fretList = new List<string>();
            var count = messageList.Count;
            foreach (var message in messageList)
            {
                if (message.MidiChannel == 1)
                {
                    var noteId = message.Data1 - LowNoteID;

                    string s = string.Format("{0}: {1}", message.Ticks.ToString("000000000"), new string(cArray));

                    noteList.Add(s);
                }
            }

            //Calculate diffs
            MessageDto currentMessageNoteOn = null;
            MessageDto previousMessageNoteOn = null;
            count = messageList.Count();
            for (var i = 0; i < count; i++)
            {
                var message = messageList[i];
                if (message.ChannelCommand == ChannelCommand.NoteOn &&
                    message.Data2 > 0)
                {
                    if (currentMessageNoteOn != null)
                        previousMessageNoteOn = currentMessageNoteOn;

                    currentMessageNoteOn = messageList[i];

                    if (previousMessageNoteOn == null)
                    {
                        currentMessageNoteOn.FretPosition = 3; //first note must fall at the middle of the fret
                    }
                    else
                    {
                        currentMessageNoteOn.NoteDiffToPrevious = currentMessageNoteOn.Data1 - previousMessageNoteOn.Data1;
                        previousMessageNoteOn.NoteDiffToNext = previousMessageNoteOn.Data1 - currentMessageNoteOn.Data1;
                        currentMessageNoteOn.TickDiffToPrevious = (int)(currentMessageNoteOn.Ticks - previousMessageNoteOn.Ticks);
                        previousMessageNoteOn.TickDiffToNext = (int)(previousMessageNoteOn.Ticks - currentMessageNoteOn.Ticks);

                        if (currentMessageNoteOn.Data1 == previousMessageNoteOn.Data1)
                        {
                            currentMessageNoteOn.FretPosition = previousMessageNoteOn.FretPosition; //keep the same fret position as the previous note
                        }
                        else if (currentMessageNoteOn.Data1 > previousMessageNoteOn.Data1)
                        {
                            currentMessageNoteOn.FretPosition = previousMessageNoteOn.FretPosition + 1; //one fret to the right
                        }
                        else if (currentMessageNoteOn.Data1 < previousMessageNoteOn.Data1)
                        {
                            currentMessageNoteOn.FretPosition = previousMessageNoteOn.FretPosition - 1; //one fret to the left
                        }
                    }
                }
            }


            //var fret = "||";
            //var id = 0;

            //count = messageList.Count;
            //foreach (var message in messageList)
            //{
            //    if (message.MidiChannel == 1)
            //    {
            //        var noteId = message.Data1 - LowNoteID;

            //        if (message.ChannelCommand == ChannelCommand.NoteOn)
            //        {
            //            if (message.Data2 > 0)
            //            {
            //                string s = string.Format("{0}: {1}", message.FretPosition, new string(cArray));
            //            }
            //        }
            //    }

            //    sequence1.LoadAsync(fileName);
            //}
        }

        private void pianoControl1_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {
            #region Guard

            if (playing)
            {
                return;
            }

            #endregion

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
        }

        private void pianoControl1_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {
            #region Guard

            if (playing)
            {
                return;
            }

            #endregion

            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!scrolling)
            {
                slider1.Value = sequencer1.Position;
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (openMidiFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openMidiFileDialog.FileName;

                try
                {
                    sequencer1.Stop();
                    playing = false;
                    sequence1.LoadAsync(fileName);
                    this.Cursor = System.Windows.Input.Cursors.Wait;
                    btnStart.IsEnabled = false;
                    btnContinue.IsEnabled = false;
                    btnStop.IsEnabled = false;
                    btnOpen.IsEnabled = false;

                    this.Dispatcher.Invoke(
                        System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(
                            delegate()
                            {
                                Storyboard sbClockOpen = (Storyboard)FindResource("sbClockOpen");
                                grdClock.Visibility = System.Windows.Visibility.Visible;
                                sbClockOpen.Begin();
                            }
                        )
                    );

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                playing = false;
                Console.WriteLine("Stop button in");
                sequencer1.Stop();
                Console.WriteLine("Sequencer1 Stop @btn Stop");
                timer1.Stop();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                playing = true;
                sequencer1.GetTracks();
                sequencer1.Start();
                timer1.Start();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                playing = true;
                sequencer1.Continue();
                timer1.Start();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (!dragStarted)
            //    sequencer1.Position = (int)e.NewValue;
        }

        private void slider1_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this.dragStarted = true;
        }

        private void slider1_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            sequencer1.Position = (int)((Slider)sender).Value;
            this.dragStarted = false;
        }
        #endregion events


    }

    #region MIDIInstrument
    public enum MIDIInstrument
    {
        //PIANO
        AcousticGrandPiano,//1
        BrightAcousticPiano,//2
        ElectricGrandPiano,//3
        HonkytonkPiano,//4
        ElectricPiano1,//5
        ElectricPiano2,//6
        Harpsichord,//7
        Clavinet,//8


        //CHROMATICPERCUSSION
        Celesta,//9
        Glockenspiel,//10
        MusicBox,//11
        Vibraphone,//12
        Marimba,//13
        Xylophone,//14
        TubularBells,//15
        Dulcimer,//16


        //ORGAN
        DrawbarOrgan,//17
        PercussiveOrgan,//18
        RockOrgan,//19
        ChurchOrgan,//20
        ReedOrgan,//21
        Accordion,//22
        Harmonica,//23
        TangoAccordion,//24


        //GUITAR
        AcousticGuitarnylon,//25
        AcousticGuitarsteel,//26
        ElectricGuitarjazz,//27
        ElectricGuitarclean,//28
        ElectricGuitarmuted,//29
        OverdrivenGuitar,//30
        DistortionGuitar,//31
        GuitarHarmonics,//32

        //BASS
        AcousticBass,//33
        ElectricBassfinger,//34
        ElectricBasspick,//35
        FretlessBass,//36
        SlapBass1,//37
        SlapBass2,//38
        SynthBass1,//39
        SynthBass2,//40

        //STRINGS
        Violin,//41
        Viola,//42
        Cello,//43
        Contrabass,//44
        TremoloStrings,//45
        PizzicatoStrings,//46
        OrchestralHarp,//47
        Timpani,//48

        //ENSEMBLE
        StringEnsemble1,//49
        StringEnsemble2,//50
        SynthStrings1,//51
        SynthStrings2,//52
        ChoirAahs,//53
        VoiceOohs,//54
        SynthChoir,//55
        OrchestraHit,//56
        //BRASS
        Trumpet,//57
        Trombone,//58
        Tuba,//59
        MutedTrumpet,//60
        FrenchHorn,//61
        BrassSection,//62
        SynthBrass1,//63
        SynthBrass2,//64
        //REED
        SopranoSax,//65
        AltoSax,//66
        TenorSax,//67
        BaritoneSax,//68
        Oboe,//69
        EnglishHorn,//70
        Bassoon,//71
        Clarinet,//72

        //PIPE
        Piccolo,//73
        Flute,//74
        Recorder,//75
        PanFlute,//76
        BlownBottle,//77
        Shakuhachi,//78
        Whistle,//79
        Ocarina,//80
        //SYNTHLEAD
        Lead1square,//81
        Lead2sawtooth,//82
        Lead3calliope,//83
        Lead4chiff,//84
        Lead5charang,//85
        Lead6voice,//86
        Lead7fifths,//87
        Lead8basslead,//88
        //SYNTHPAD
        Pad1newage,//89
        Pad2warm,//90
        Pad3polysynth,//91
        Pad4choir,//92
        Pad5bowed,//93
        Pad6metallic,//94
        Pad7halo,//95
        Pad8sweep,//96

        //SynthEffects,
        FX1rain,//97
        FX2soundtrack,//98
        FX3crystal,//99
        x0FX4atmosphere,//100
        x1FX5brightness,//101
        x2FX6goblins,//102
        x3FX7echoes,//103
        x4FX8scifi,//104
        //ETHNIC
        Sitar,//105
        Banjo,//106
        Shamisen,//107
        Koto,//108
        Kalimba,//109
        Bagpipe,//110
        Fiddle,//111
        Shanai,//112
        //PERCUSSIVE
        TinkleBell,//113
        Agogo,//114
        SteelDrums,//115
        Woodblock,//116
        TaikoDrum,//117
        MelodicTom,//118
        SynthDrum,//119
        //SOUNDEFFECTS
        ReverseCymbal,//120
        GuitarFretNoise,//121
        BreathNoise,//122
        Seashore,//123
        BirdTweet,//124
        TelephoneRing,//125
        Helicopter,//126
        Applause,//127
        Gunshot//128
    }
    #endregion MIDIInstrument
}
