using System;
using System.Collections.Generic;

#nullable disable

namespace EPG_Api.Models
{
    public partial class Epg
    {
        public Epg()
        {
            Credits = new HashSet<Credit>();
        }

        public int Id { get; set; }
        public int? Eid { get; set; }
        public DateTime? StartTime { get; set; }
        public string Duration { get; set; }
        public string ShortAlb { get; set; }
        public string ShortEng { get; set; }
        public string ExtendedAlb { get; set; }
        public string ExtendedEng { get; set; }
        public short? CdNibble1 { get; set; }
        public short? CdNibble2 { get; set; }
        public string PrdCountryCode { get; set; }
        public short? Prd { get; set; }
        public DateTime? DateProd { get; set; }
        public byte[] Genre { get; set; }
        public string Channel { get; set; }
        public short? Status { get; set; }
        public string Poster { get; set; }
        public string Trailer { get; set; }
        public double? Rating { get; set; }

        public virtual ICollection<Credit> Credits { get; set; }
    }
}
