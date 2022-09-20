using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EPG_Api.Attributes;
using EPG_Api.Models;

namespace EPG_Api.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    [ApiKey]
    public class ChannelsController : ControllerBase
    {
        public IActionResult View()
        {
            EPGContext epg = new EPGContext();
            var result = epg.Epgs.OrderBy(o => o.Channel).Select(s => s.Channel).Distinct().ToList();
            epg.Dispose();
            return Ok(result);
        }

        public IActionResult All()
        {
            EPGContext epg = new EPGContext();
            var result = epg.Channels.Where(w => w.Status == 0).Select(s => new Channels() 
            { 
                Id = s.Id,
                Channel = s.Channel, 
                Status = s.Status 
            }).ToList();
            epg.Dispose();
            return Ok(result);
        }
    }
}
