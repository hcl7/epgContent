using System;
using System.Collections.Generic;

#nullable disable

namespace EPG_Api.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Usr { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
        public string Apikey { get; set; }
        public int? Nrequests { get; set; }
        public short? Subscription { get; set; }
        public DateTime? Dateinserted { get; set; }
        public string Company { get; set; }
    }
}
