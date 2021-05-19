using System;
using System.Collections.Generic;

namespace appointment
{
    [Serializable]
    public class AppDate
    {
        public AppDate()
        {

        }

        public DateTime  Date { get; set; }
        public IList<Dictionary<string,string> >Time { get; set; }

        //public string To { get; set; }
        //public string From { get; set; }
        //public string ServiceGroupId { get; set; }
        //public string UserId { get; set; }
    }
}