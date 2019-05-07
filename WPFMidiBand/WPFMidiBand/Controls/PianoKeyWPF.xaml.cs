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
using WPFMidiBand.Controls;

namespace WPFMidiBand.Controls
{
    /// <summary>
    /// Interaction logic for PianoKeyWPF.xaml
    /// </summary>
    public partial class PianoKeyWPF : UserControl
    {
        private PianoControlWPF.KeyType keyType = PianoControlWPF.KeyType.White;

        private bool on = false;

        private LinearGradientBrush whiteKeyOnBrush;
        private LinearGradientBrush blackKeyOnBrush;

        private SolidColorBrush whiteKeyOffBrush = new SolidColorBrush(Colors.White);

        private int noteID = 60;

        public PianoKeyWPF(PianoControlWPF.KeyType keyType)
        {
            this.keyType = keyType;
            whiteKeyOnBrush = new LinearGradientBrush();
            whiteKeyOnBrush.GradientStops.Add(new GradientStop(Colors.White, 0.0));
            whiteKeyOnBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x20, 0x20, 0x20), 1.0));

            blackKeyOnBrush = new LinearGradientBrush();
            blackKeyOnBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0.0));
            blackKeyOnBrush.GradientStops.Add(new GradientStop(Colors.Black, 1.0));

            InitializeComponent();
        }

        public void PressPianoKey()
        {
            brdInner.Dispatcher.Invoke(
          System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
                delegate()
                {
                    if (keyType == PianoControlWPF.KeyType.White)
                        brdInner.Background = whiteKeyOnBrush;
                    else
                        brdInner.Background = blackKeyOnBrush;
                }
            ));

            on = true;
        }

        public void ReleasePianoKey()
        {
            brdInner.Dispatcher.Invoke(
          System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
                delegate()
                {
                    brdInner.Background = whiteKeyOffBrush;
                }
            ));

            on = false;
        }

        public int NoteID
        {
            get
            {
                return noteID;
            }
            set
            {
                #region Require

                if (value < 0 || value > ShortMessage.DataMaxValue)
                {
                    throw new ArgumentOutOfRangeException("NoteID", noteID,
                        "Note ID out of range.");
                }

                #endregion

                noteID = value;
            }
        }

        public bool IsPianoKeyPressed
        {
            get
            {
                return on;
            }
        }

        public Color NoteOnColor
        {
            set
            {
                Brush brush = null;
                if (keyType == PianoControlWPF.KeyType.White)
                    brush = whiteKeyOnBrush;
                else
                    brush = blackKeyOnBrush;
                brdInner.Background = brush;
            }
        }

        public Color NoteOffColor
        {
            get
            {
                return whiteKeyOffBrush.Color;
            }
            set
            {
                whiteKeyOffBrush.Color = value;
                brdInner.Background = whiteKeyOffBrush;
            }
        }

        public PianoControlWPF.KeyType KeyType { get; set; }
    }
}
