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
            var result = epg.EpgTvas.Where(x => x.Status == 1).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert([FromForm] EpgTva data)
        {
            EPGContext epg = new EPGContext();
            try
            {
                if (data == null)
                {
                    return BadRequest("Post Mismatch!");
                }
                var result = epg.EpgTvas.FirstOrDefault(x => x.SedNameEng == data.SedNameEng);
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
                    epg.EpgTvas.AddRange(data);
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
            List<EpgTva> result = new List<EpgTva>();

            result = epg.EpgTvas.Where(w => w.Id == Id).Select(s => new EpgTva()
            {
                Id = s.Id,
                Eid = s.Eid,
                StartTime = s.StartTime,
                Duration = s.Duration,
                SedNameAlb = s.SedNameAlb,
                SedLangAlb = s.SedLangAlb,
                SedNameEng = s.SedNameEng,
                SedLangEng = s.SedLangEng,
                EedTextAlb = s.EedTextAlb,
                EedLangAlb = s.EedLangAlb,
                EedTextEng = s.EedTextEng,
                EedLangEng = s.EedLangEng,
                CdNibble1 = s.CdNibble1,
                CdNibble2 = s.CdNibble2,
                PrdCountryCode = s.PrdCountryCode,
                Poster = s.Poster,
                Trailer = s.Trailer,
                PrdValue = s.PrdValue,
            }).ToList();
            epg.Dispose();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromForm] EpgTva data)
        {
            EPGContext epg = new EPGContext();
            try
            {
                if (id != data.Id)
                {
                    return BadRequest("Id Mismatch!");
                }
                var result = epg.EpgTvas.FirstOrDefault(x => x.Id == id);
                result.StartTime = data.StartTime;
                result.Duration = data.Duration;
                result.SedNameAlb = data.SedNameAlb;
                result.SedLangAlb = data.SedLangAlb;
                result.SedNameEng = data.SedNameEng;
                result.SedLangEng = data.SedLangEng;
                result.EedTextAlb = data.EedTextAlb;
                result.EedLangAlb = data.EedLangAlb;
                result.EedTextEng = data.EedTextEng;
                result.CdNibble1 = data.CdNibble1;
                result.CdNibble2 = data.CdNibble2;
                result.PrdCountryCode = data.PrdCountryCode;
                result.PrdValue = data.PrdValue;
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
