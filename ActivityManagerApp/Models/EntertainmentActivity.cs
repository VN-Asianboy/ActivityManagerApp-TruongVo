using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagerApp.Models
{
    public class EntertainmentActivity
    {
        public DateTime DateStartTime { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int MinParticipants { get; set; }
    }
}
