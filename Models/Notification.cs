using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Recipient { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public Boolean Status { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<DateTime> DateRead { get; set; }
    }

    public class NotificationTemplate
    {
        public Int16 Id { get; set; }
        public string Event { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
    }

    public class EmailTemplate
    {
        public Int16 Id { get; set; }
        public string Event { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
    }
}
