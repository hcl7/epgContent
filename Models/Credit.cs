using System;
using System.Collections.Generic;

#nullable disable

namespace EPG_Api.Models
{
    public partial class Credit
    {
        public int Id { get; set; }
        public int? AssetId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime? DateInserted { get; set; }

        public virtual Epg Asset { get; set; }
    }
}
