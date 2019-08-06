using System.Collections.Generic;
using NET.Core.Base.Business.Notifications;

namespace NET.Core.Base.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);
    }
}
