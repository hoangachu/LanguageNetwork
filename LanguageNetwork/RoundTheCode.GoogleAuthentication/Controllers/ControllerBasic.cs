using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageNetwork.Controllers
{
    public class ControllerBasic : ControllerBase, IAuthorizationFilter
    {
        public ControllerBasic()
        {
            CheckLogin();
        }
        public void CheckLogin()
        {
            bool Islogin = User.Claims.FirstOrDefault(x => x.Type.Equals("Name")).Value == null ? false : true;
            if (!Islogin)
            {
                //return Redirect("Home");
            }
            //return Ok();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
