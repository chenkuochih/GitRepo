using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sanford.Multimedia.Midi;

namespace WPFMidiBand
{
    public class MessageDto
    {
        public int id { get; set; }
        public ChannelCommand ChannelCommand { get; set; }
        public int MidiChannel { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public MessageType MessageType { get; set; }
        public int NoteDiffToPrevious { get; set; }
        public int NoteDiffToNext { get; set; }
        public int TickDiffToPrevious { get; set; }
        public int TickDiffToNext { get; set; }
        public int FretPosition { get; set; }
        public long Ticks { get; set; }
    }
}
