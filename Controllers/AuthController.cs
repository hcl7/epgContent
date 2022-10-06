﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using EPG_Api.Models;
using EPG_Api.Modules;
using System.Security.Cryptography;

namespace EPG_Api.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] User usr)
        {
            if (usr == null)
            {
                return BadRequest("Post Mismatch!");
            }
            Models.EPGContext client = new Models.EPGContext();
            var user = new User
            {
                Usr = usr.Usr,
                Email = usr.Email,
                Passwd = BCrypt.Net.BCrypt.HashPassword(usr.Passwd)
            };
            client.Users.Add(user);
            client.SaveChanges();
            client.Dispose();
            return Ok("Registered!");
        }
        [HttpPost]
        public IActionResult Login([FromBody] User usr)
        {
            if(usr == null)
            {
                return Ok("Post is Empty!");
            }
            Models.EPGContext client = new Models.EPGContext();
            var user = client.Users.Where(x => x.Email == usr.Email).Select(s => new User()
            {
                Id = s.Id,
                Usr = s.Usr,
                Passwd = s.Passwd,
                Email = s.Email
            }).FirstOrDefault();
            if (user == null) return BadRequest(new { message = "Invalid Credentials!" });
            if (!BCrypt.Net.BCrypt.Verify(usr.Passwd, user.Passwd))
            {
                return BadRequest(new { message = "Invalid Credentials!" });
            }
            var jwt = JwtService.Generate(usr);
            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
            client.Dispose();
            return Ok(new { jwt, user, message = "Logged In!" });
        }

        public string GenerateApiKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }
        public string ApiKeyGen()
        {
            var key = new byte[16];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);
            return apiKey;
        }
        public User GetByEmail(string email)
        {
            Models.EPGContext client = new Models.EPGContext();
            var usr = client.Users.FirstOrDefault(u => u.Email == email);
            client.Dispose();
            return usr;
        }

        [HttpGet]
        public IActionResult Usr()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = JwtService.Verify(jwt);
                string email = token.Claims.FirstOrDefault(x => x.Type == "email").Value;
                var user = GetByEmail(email);
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "Logged Out!"
            });
        }
    }
}
