using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NET.Core.Base.Business.Interfaces;
using NET.Core.Base.Business.Notifications;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace NET.Core.Base.Api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        public MainController(
            IUser user,
            INotificador notificador)
        {
            _user = user;
            _notificador = notificador;

            if (user.IsAuthenticated())
            {
                UsuarioId = user.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        private readonly INotificador _notificador;

        public readonly IUser _user;

        protected Guid UsuarioId { get; set; }

        protected bool UsuarioAutenticado { get; set; }

        protected bool OperacaoValida() => !_notificador.TemNotificacao();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                data = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            List<ModelError> erros = modelState.Values.SelectMany(e => e.Errors).ToList();
            erros.ForEach(e => NotificarErro(e.Exception?.Message ?? e.ErrorMessage));
        }

        protected void NotificarErro(string mensagem) => _notificador.Handle(new Notificacao(mensagem));
    }
}
