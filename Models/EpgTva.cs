using System;
using System.Collections.Generic;

#nullable disable

namespace EPG_Api.Models
{
    public partial class EpgTva
    {
        public int Id { get; set; }
        public int? Eid { get; set; }
        public DateTime? StartTime { get; set; }
        public string Duration { get; set; }
        public string SedNameAlb { get; set; }
        public string SedLangAlb { get; set; }
        public string SedNameEng { get; set; }
        public string SedLangEng { get; set; }
        public string EedLangAlb { get; set; }
        public string EedTextAlb { get; set; }
        public string EedLangEng { get; set; }
        public string EedTextEng { get; set; }
        public short? CdNibble1 { get; set; }
        public short? CdNibble2 { get; set; }
        public string PrdCountryCode { get; set; }
        public short? PrdValue { get; set; }
        public DateTime? DateProd { get; set; }
        public byte[] Genre { get; set; }
        public string Channel { get; set; }
        public short? Status { get; set; }
    }
}
