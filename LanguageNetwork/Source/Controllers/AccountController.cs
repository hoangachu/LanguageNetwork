using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MulLan.Models.User;


namespace RoundTheCode.GoogleAuthentication.Controllers
{
    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            List<Claim> claims = new List<Claim>();

            var claim = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                }).ToList();
            List<Claim> claimss = new List<Claim>();
            claim.ForEach(x => claimss.Add(new Claim(x.Type, x.Value)));
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
            //create principle
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            //sign-in
            await HttpContext.SignInAsync(
               scheme: "SecurityScheme",
               principal: claimsPrincipal,
               properties: new AuthenticationProperties()
               {
                   IsPersistent = true,
                   ExpiresUtc = DateTime.UtcNow.AddMinutes(100)

               }

           );

            return Redirect("/Account/ReponseMainPage");
        }
        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser(string user, string password)
        {
            User User = new User();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        User = GetUserByUserName(user);
                        if (user == null)
                        {
                            return NoContent();
                        }
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim("Name",User.UserName),
                            new Claim("Password",User.Password)
                        };
                        // Create identity
                        ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
                        //create principle
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                        //sign-in
                        await HttpContext.SignInAsync(
                           scheme: "SecurityScheme",
                           principal: claimsPrincipal,
                           properties: new AuthenticationProperties()
                           {
                               IsPersistent = true,
                               ExpiresUtc = DateTime.UtcNow.AddMinutes(100)

                           }

                       );

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }

                return Ok(new { data = 1, url = "/Account/ReponseMainPage" });
            }
        }
        public User GetUserByUserName(string UserName)
        {
            User User = new User();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserByUserName", con))
                {
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;

                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                User.UserName = (string)dr["UserName"];
                                User.Password = (string)dr["Password"];
                                User.UserID = (int)dr["UserID"];
                                User.imgURL = (string)dr["FileURL"];
                            }



                        }
                        catch (Exception e)
                        {
                            throw e;
                        }

                        con.Close();
                    }


                }
            }
            return User;
        }
        public User GetUserByUserID(int UserID)
        {
            User User = new User();
            using (SqlConnection con = new SqlConnection(Startup.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserByUserID", con))
                {
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;

                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                User.UserName = (string)dr["UserName"];
                                User.Password = (string)dr["Password"];
                                User.UserID = (int)dr["UserID"];
                                User.imgURL = (string)dr["ImagesUrl"];
                            }




                        }
                        catch (Exception e)
                        {
                            throw e;
                        }

                        con.Close();
                    }


                }
            }
            return User;
        }
        [Route("ReponseMainPage")]
        public ActionResult ReponseMainPage()
        {
            return View();
        }
    }
}
