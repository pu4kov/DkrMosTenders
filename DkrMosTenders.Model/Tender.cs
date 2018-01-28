using System;

namespace DkrMosTenders.Model
{
    public class Tender
    {
        public string DkrNumber { get; set; }
        public string FullName { get; set; }
        public TenderObject Object { get; set; }
        public string Url { get; set; }
    }
}
