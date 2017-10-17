using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SupportManagementSystem
{
    [DataContract]
    public class DayResult
    {
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public List<string> Engeneers { get; set; }
    }
}
