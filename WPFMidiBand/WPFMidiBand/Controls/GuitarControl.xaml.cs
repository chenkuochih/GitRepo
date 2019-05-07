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

namespace WPFMidiBand.Controls
{
    /// <summary>
    /// Interaction logic for GuitarControl.xaml
    /// </summary>
    public partial class GuitarControl : UserControl
    {
        private const int LowNoteID = 21;

        private const int HighNoteID = 109;

        Dictionary<int, Border> dicNotesOn = new Dictionary<int, Border>();

        SolidColorBrush innerColor = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush outerColor = new SolidColorBrush(Colors.White);

        SolidColorBrush stringOffColor = new SolidColorBrush(Colors.DarkGray);
        SolidColorBrush stringOnColor = new SolidColorBrush(Colors.White);
        SolidColorBrush fontBrush = new SolidColorBrush(Colors.Black);

        struct StringInfo
        {
            public int Row;
            public int Min;
            public int Max;
            public Rectangle Rect;
        }

        StringInfo[] stringInfos;

        public GuitarControl()
        {
            InitializeComponent();

            stringInfos = new StringInfo[6];
            stringInfos[0] = new StringInfo() { Row = 5, Min = 40, Max = 44, Rect = string5 };
            stringInfos[1] = new StringInfo() { Row = 4, Min = 45, Max = 49, Rect = string4 };
            stringInfos[2] = new StringInfo() { Row = 3, Min = 50, Max = 54, Rect = string3 };
            stringInfos[3] = new StringInfo() { Row = 2, Min = 55, Max = 58, Rect = string2 };
            stringInfos[4] = new StringInfo() { Row = 1, Min = 59, Max = 63, Rect = string1 };
            stringInfos[5] = new StringInfo() { Row = 0, Min = 64, Max = 90, Rect = string0 };
        }

        public void Send(ChannelMessage message)
        {
            if (message.Command == ChannelCommand.NoteOn &&
                message.Data1 >= LowNoteID && message.Data1 <= HighNoteID)
            {
                //message.Data2 > 0 means the string is being pressed
                if (message.Data2 > 0)
                {
                    if (!dicNotesOn.ContainsKey(message.Data1))
                    {
                        var row = 0;
                        var col = 0;
                        var stringId = 0;
                        Rectangle stringRect = null;

                        //We look for the StringInfo matching the
                        //note information
                        var stringInfoQuery = from si in stringInfos
                                              where message.Data1 >= si.Min && message.Data1 <= si.Max
                                              select si;

                        if (stringInfoQuery.Any())
                        {
                            var stringInfo = stringInfoQuery.First();
                            row = stringInfo.Row;
                            col = message.Data1 - stringInfo.Min;
                            stringRect = stringInfo.Rect;
                            stringId = stringInfo.Row;
                        }

                        if (stringRect != null)
                        {
                            stringRect.Stroke =
                            stringRect.Fill = stringOnColor;
                            stringRect.Height = 1;
                        }

                        //This border shows which note
                        //is being played
                        var noteOn = new Border()
                        {
                            Width = 12,
                            Height = 12,
                            Background = innerColor,
                            BorderBrush = outerColor,
                            Tag = stringId,
                            CornerRadius = new CornerRadius(2,2,2,2)
                        };

                        //This text block displays
                        //the fret number
                        var txt = new TextBlock()
                        {
                            Text = col.ToString(),
                            Foreground = fontBrush,
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                            FontWeight = FontWeights.Bold,
                            FontSize = 10
                        };

                        noteOn.Child = txt;                        

                        if (stringRect != null)
                        {
                            stringRect.Stroke =
                            stringRect.Fill = stringOnColor;
                            stringRect.Height = 1;
                        }

                        noteOn.SetValue(Grid.RowProperty, row);
                        noteOn.SetValue(Grid.ColumnProperty, col);
                        dicNotesOn.Add(message.Data1, noteOn);
                        grdArm.Children.Add(noteOn);
                    }
                }
                else if (message.Data2 == 0)
                {
                    if (dicNotesOn.ContainsKey(message.Data1))
                    {
                        var noteOff = dicNotesOn[message.Data1];
                        dicNotesOn.Remove(message.Data1);
                        grdArm.Children.Remove(noteOff);

                        var stringId = (int)noteOff.Tag;
                        TurnOffString(stringId);
                    }
                }
            }
            else if (message.Command == ChannelCommand.NoteOff)
            {
                if (dicNotesOn.ContainsKey(message.Data1))
                {
                    var noteOff = dicNotesOn[message.Data1];
                    dicNotesOn.Remove(message.Data1);
                    grdArm.Children.Remove(noteOff);

                    var stringId = (int)noteOff.Tag;
                    TurnOffString(stringId);
                }
            }
        }

        private void TurnOffString(int stringId)
        {
            var stringInfo = stringInfos[stringId];
            Rectangle stringRect = null;

            switch (stringId)
            {
                case 0:
                    stringRect = string0;
                    break;
                case 1:
                    stringRect = string1;
                    break;
                case 2:
                    stringRect = string2;
                    break;
                case 3:
                    stringRect = string3;
                    break;
                case 4:
                    stringRect = string4;
                    break;
                case 5:
                    stringRect = string5;
                    break;
            }
            stringRect.Height = 1;
            stringRect.Stroke =
            stringRect.Fill = stringOffColor;
        }

        public void Clear()
        {
            dicNotesOn.Clear();
            for (var i = grdArm.Children.Count - 1; i >= 0; i--)
            {
                if (grdArm.Children[i] is Border)
                {
                    var ell = grdArm.Children[i] as Border;
                    if (ell.Tag != null)
                    {
                        grdArm.Children.RemoveAt(i);
                    }
                }
            }

            for (var i = 0; i < 6; i++)
            {
                TurnOffString(i);
            }
        }
    }
}
