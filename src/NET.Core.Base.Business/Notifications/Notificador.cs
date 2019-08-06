using System.Linq;
using System.Collections.Generic;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Business.Notifications
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes { get; set; } = new List<Notificacao>();

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
