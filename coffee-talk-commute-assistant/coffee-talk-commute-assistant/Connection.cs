using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coffee_talk_commute_assistant
{ 
    public class Connection
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int BoardId { get; set; }
        public int StopId { get; set; }
        public string StopName { get; set; }
        public string Origin { get; set; }
        public string Track { get; set; }

        private string dateTime;
        public string DateTime
        {
            get
            {
                return dateTime.Substring(dateTime.IndexOf("T") + 1);
            }
            set
            {
                dateTime = value;
            }
        }
    }
}
