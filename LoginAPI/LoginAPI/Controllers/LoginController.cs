using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace LoginAPI.Controllers
{
    // Allow cors
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        // POST: LoginController/Create
        [HttpPost]
        public ActionResult isAuth(string account, string password)
        {
            try
            {
                if (account == "test" && password == "1234")
                {
                    isAuthModel isAuth = new isAuthModel()
                    {
                        isAuth = true,
                        display = "Testing",
                        first = "1"
                    };

                    return Ok(isAuth);
                }
                else if (account == "ryuk" && password == "5678")
                {
                    isAuthModel isAuth = new isAuthModel()
                    {
                        isAuth = true,
                        display = "Ryuk",
                        first = "2"
                    };

                    return Ok(isAuth);
                }
                else
                {
                    isAuthModel isAuth = new isAuthModel()
                    {
                        isAuth = false
                    };

                    return Ok(isAuth);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
