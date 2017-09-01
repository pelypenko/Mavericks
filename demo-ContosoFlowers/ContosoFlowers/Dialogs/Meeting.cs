using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoFlowers.Dialogs
{
    [Serializable]
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Person { get; set; }
        public string Notes { get; set; }
        public string Date { get; internal set; }
    }
}