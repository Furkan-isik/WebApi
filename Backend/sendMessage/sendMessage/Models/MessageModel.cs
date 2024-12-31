using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sendMessage.Models
{
    public class MessageModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public int Check { get; set; } 
        public string Message { get; set; }
    }
}