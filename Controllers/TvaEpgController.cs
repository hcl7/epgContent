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
    public class TvaEpgController : ControllerBase
    {
        [HttpPost]
        public IActionResult View([FromForm] string channel)
        {
            EPGContext epg = new EPGContext();
            var result = epg.Epgs.Where(x => x.Status == 1 && x.Channel.Equals(channel)).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert([FromForm] Epg data)
        {
            EPGContext epg = new EPGContext();
            try
            {
                if (data == null)
                {
                    return BadRequest("Post Mismatch!");
                }
                var result = epg.Epgs.FirstOrDefault(x => x.ShortEng == data.ShortEng);
                if (result != null)
                {
                    result.StartTime = data.StartTime;
                    result.Status = 1;
                    epg.SaveChanges();
                    epg.Dispose();
                    return Ok("Updated!");
                }
                else
                {
                    epg.Epgs.AddRange(data);
                    epg.SaveChanges();
                    epg.Dispose();
                    return Ok("Inserted!");
                }

            }
            catch (Exception e)
            {
                epg.Dispose();
                return BadRequest("Error Insert: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int Id)
        {
            EPGContext epg = new EPGContext();
            List<Epg> result = new List<Epg>();

            result = epg.Epgs.Where(w => w.Id == Id).Select(s => new Epg()
            {
                Id = s.Id,
                Eid = s.Eid,
                StartTime = s.StartTime,
                Duration = s.Duration,
                ShortAlb = s.ShortAlb,
                ShortEng = s.ShortEng,
                ExtendedAlb = s.ExtendedAlb,
                ExtendedEng = s.ExtendedEng,
                CdNibble1 = s.CdNibble1,
                CdNibble2 = s.CdNibble2,
                PrdCountryCode = s.PrdCountryCode,
                Poster = s.Poster,
                Trailer = s.Trailer,
                Prd = s.Prd,
            }).ToList();
            epg.Dispose();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromForm] Epg data)
        {
            EPGContext epg = new EPGContext();
            try
            {
                if (id != data.Id)
                {
                    return BadRequest("Id Mismatch!");
                }
                var result = epg.Epgs.FirstOrDefault(x => x.Id == id);
                result.StartTime = data.StartTime;
                result.Duration = data.Duration;
                result.ShortAlb = data.ShortAlb;
                result.ShortEng = data.ShortEng;
                result.ExtendedAlb = data.ExtendedAlb;
                result.ExtendedEng = data.ExtendedEng;
                result.CdNibble1 = data.CdNibble1;
                result.CdNibble2 = data.CdNibble2;
                result.PrdCountryCode = data.PrdCountryCode;
                result.Prd = data.Prd;
                result.Poster = data.Poster;
                result.Trailer = data.Trailer;
                result.Status = 1;
                epg.SaveChanges();
                epg.Dispose();
                return Ok("Updated!");
            }
            catch (Exception e)
            {
                epg.Dispose();
                return BadRequest("Error update asset: " + e.Message);
            }
        }
    }
}
