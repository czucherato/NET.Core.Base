using System;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NET.Core.Base.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace NET.Core.Base.Api.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteController : MainController
    {
        public TesteController(
            INotificador notificador,
            IUser user,
            ILogger<TesteController> logger)
            : base(user, notificador)
        {
            _logger = logger;
        }

        private readonly ILogger _logger;

        [HttpGet]
        [AllowAnonymous]
        public string Valor()
        {
            throw new NotImplementedException();
            //try
            //{
            //    var i = 0;
            //    var result = 42 / i;
            //}
            //catch(DivideByZeroException e)
            //{
            //    e.Ship(HttpContext);
            //}
            return "Sou a V2";
        }
    }
}
