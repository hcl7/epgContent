using EPG_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPG_Api.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class TvaEpgController : ControllerBase
    {
        public IActionResult View()
        {
            EPGContext epg = new EPGContext();
            var result = epg.EpgTvas.ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert([FromForm] EpgTva data)
        {
            EPGContext epg = new EPGContext();
            try
            {
                if (data.Id == 0)
                {
                    return BadRequest("Id Mismatch!");
                }
                epg.AddRange(data);
                epg.SaveChanges();
                epg.Dispose();
                return Ok("Updated!");
            }
            catch (Exception e)
            {
                epg.Dispose();
                return BadRequest("error: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int Id)
        {
            EPGContext epg = new EPGContext();
            List<EpgTva> result = new List<EpgTva>();

            result = epg.EpgTvas.Where(w => w.Id == Id).Select(s => new EpgTva()
            {
                Id = s.Id,
                StartTime = s.StartTime,
                Duration = s.Duration
            }).ToList();
            epg.Dispose();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
