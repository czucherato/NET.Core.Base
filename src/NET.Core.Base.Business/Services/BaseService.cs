using FluentValidation;
using FluentValidation.Results;
using NET.Core.Base.Business.Models;
using NET.Core.Base.Business.Interfaces;
using NET.Core.Base.Business.Notifications;

namespace NET.Core.Base.Business.Services
{
    public abstract class BaseService
    {
        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        private readonly INotificador _notificador;

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);
            if (validator.IsValid) return true;

            Notificar(validator);
            return false;
        }
    }
}
