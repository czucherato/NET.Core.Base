using Microsoft.AspNetCore.Mvc;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Mvc.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        private readonly INotificador _notificador;

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}
