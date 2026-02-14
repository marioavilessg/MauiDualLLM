using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Models
{
    public class ChatMessage
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public bool IsMine { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
