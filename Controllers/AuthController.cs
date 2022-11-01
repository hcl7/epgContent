using Microsoft.AspNetCore.Http;
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
            var checkEmailExists = client.Users.Where(w => w.Email.Equals(usr.Email)).SingleOrDefault();
            if (checkEmailExists != null)
            {
                return Ok("Email Already Exists!");
            }
            else
            {
                var user = new User
                {
                    Usr = usr.Usr,
                    Fname = usr.Fname,
                    Lname = usr.Lname,
                    Company = usr.Company,
                    Email = usr.Email,
                    Subscription = 0,
                    Nrequests = 100,
                    Dateinserted = DateTime.Now,
                    Apikey = GenerateApiKey(),
                    VerifyCode = GenVerCode(),
                    AccountStatus = 0,
                    Passwd = BCrypt.Net.BCrypt.HashPassword(usr.Passwd)
                };
                client.Users.Add(user);
                client.SaveChanges();
                client.Dispose();
                return Ok("Registered!");
            }
            
        }
        [HttpPost]
        public IActionResult Login([FromBody] User usr)
        {
            if(usr == null)
            {
                return BadRequest(new { message = "Bad Request" });
            }
            else
            {
                Models.EPGContext client = new Models.EPGContext();
                var user = client.Users.Where(x => x.Email == usr.Email && x.AccountStatus == 1).Select(s => new User()
                {
                    Id = s.Id,
                    Usr = s.Usr,
                    Passwd = s.Passwd,
                    Email = s.Email,
                    Apikey = s.Apikey
                }).FirstOrDefault();
                if (user == null) return Ok(new { message = "Invalid Credentials" });
                if (!BCrypt.Net.BCrypt.Verify(usr.Passwd, user.Passwd))
                {
                    return Ok(new { message = "Invalid Password" });
                }
                else
                {
                    var jwt = JwtService.Generate(user);
                    Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
                    client.Dispose();
                    return Ok(new { jwt, user, message = "Logged In" });
                }
            }
        }

        [HttpPost]
        public IActionResult UserStatusUpdate([FromForm] string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    EPGContext client = new EPGContext();
                    var user = client.Users.Where(x => x.VerifyCode.Equals(code)).Select(s => new User()
                    {
                        VerifyCode = s.VerifyCode
                    }).FirstOrDefault();

                    if(user != null)
                    {
                        client.Users.Where(w => w.VerifyCode.Equals(code)).ToList().ForEach(x => x.AccountStatus = 1);
                        client.SaveChanges();
                        return Ok("User Account Activated!");
                    }
                    else
                    {
                        return Ok("Invalid Code!");
                    }
                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            else
            {
                return Ok("Null Code");
            }
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

        public string GenVerCode()
        {
            var chars = "0123456789";
            var stringChars = new char[8];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }

        public string GenerateVerifyCode()
        {
            var key = new byte[8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key);
            }
            string verifycode = Convert.ToBase64String(key);
            return verifycode;
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
