using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Mvc.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        private readonly INotificador _notificador;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
            notificacoes.ForEach(n => ViewData.ModelState.AddModelError(string.Empty, n.Mensagem));

            return View();
        }
    }
}
