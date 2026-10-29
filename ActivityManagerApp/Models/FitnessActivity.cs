using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManagerApp.Models
{
    public class FitnessActivity
    {
        public DateTime DateStartTime { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
