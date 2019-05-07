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
using Sanford.Multimedia.Midi;
using System.Windows.Media.Animation;

namespace WPFMidiBand.Controls
{
    /// <summary>
    /// Interaction logic for DrumControl.xaml
    /// </summary>
    public partial class DrumControl : UserControl
    {
        private const int LowNoteID = 21;

        private const int HighNoteID = 109;

        public DrumControl()
        {
            InitializeComponent();
        }

        public void Send(ChannelMessage message)
        {
            if (message.Command == ChannelCommand.NoteOn &&
                message.Data1 >= LowNoteID && message.Data1 <= HighNoteID)
            {
                if (message.Data2 > 0)
                {
                    string sbName = "";
                    switch (message.Data1)
                    {
                        case (int)DrumInstrument.AcousticSnare: //38,
                        case (int)DrumInstrument.ElectricSnare: //40,
                            sbName = "sbSnareDrum";
                            break;
                        case (int)DrumInstrument.LowFloorTom: //41,
                        case (int)DrumInstrument.HighFloorTom: //43,
                            sbName = "sbFloorTom";
                            break;
                        case (int)DrumInstrument.LowTom: //45,
                        case (int)DrumInstrument.LowMidTom: //47,
                            sbName = "sbTom1";
                            break;
                        case (int)DrumInstrument.HiMidtom: //48,
                        case (int)DrumInstrument.HighTom: //50,
                            sbName = "sbTom2";
                            break;
                        case (int)DrumInstrument.AcousticBassDrum: //35,
                        case (int)DrumInstrument.BassDrum: //36,
                            sbName = "sbBassDrum";
                            break;
                        case (int)DrumInstrument.ClosedHiHat: //42,
                        case (int)DrumInstrument.PedalHiHat: //44,
                        case (int)DrumInstrument.OpenHiHat: //46,
                            sbName = "sbHiHat";
                            break;
                        case (int)DrumInstrument.CrashCymbal: //49,
                        case (int)DrumInstrument.RideCymbal: //51
                            sbName = "sbRideCymbal";
                            break;
                    }
                    if (!string.IsNullOrEmpty(sbName))
                    {
                        Storyboard sb = (Storyboard)FindResource(sbName);
                        sb.Stop();
                        sb.Begin();
                    }
                }
            }
        }
    }

    public enum DrumInstrument
    {
        AcousticSnare = 38,
        ElectricSnare = 40,

        LowFloorTom = 41,
        HighFloorTom = 43,

        LowTom = 45,
        LowMidTom = 47,

        HiMidtom = 48,
        HighTom = 50,

        AcousticBassDrum = 35,
        BassDrum = 36,

        ClosedHiHat = 42,
        PedalHiHat = 44,
        OpenHiHat = 46,
        CrashCymbal = 49,

        RideCymbal = 51
    }
}
