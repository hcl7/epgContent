using System;
using System.Collections.Generic;

#nullable disable

namespace EPG_Api.Models
{
    public partial class Channels
    {
        public int Id { get; set; }
        public string Channel { get; set; }
        public short? Status { get; set; }
    }
}

