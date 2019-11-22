using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace NET.Core.Base.Mvc.Extensions
{
    public class MoedaValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

            if (attribute is MoedaAttribute moedaAttribute)
            {
                return new MoedaAttributeAdapter(moedaAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
