using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace NET.Core.Base.Api.Extensions
{
    public class JsonWithFilesFormDataModelBinder : IModelBinder
    {
        public JsonWithFilesFormDataModelBinder(IOptions<MvcJsonOptions> jsonOptions, ILoggerFactory loggerFactory)
        {
            _jsonOptions = jsonOptions;
            _formFileModelBinder = new FormFileModelBinder(loggerFactory);
        }

        private readonly IOptions<MvcJsonOptions> _jsonOptions;
        private readonly FormFileModelBinder _formFileModelBinder;

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentException(nameof(bindingContext));

            // Retrieve the form part containing the JSON
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueResult == ValueProviderResult.None)
            {
                // The JSON was not found
                string message = bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(bindingContext.FieldName);
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
                return;
            }

            var rawValue = valueResult.FirstValue;

            // Deserialize the JSON
            object model = JsonConvert.DeserializeObject(rawValue, bindingContext.ModelType, _jsonOptions.Value.SerializerSettings);

            // Now, bind each of th IFormFile properties from the other form parts
            foreach (ModelMetadata property in bindingContext.ModelMetadata.Properties)
            {
                if (property.ModelType != typeof(IFormFile)) continue;

                string fieldName = property.BinderModelName ?? property.PropertyName;
                string modelName = fieldName;
                object propertyModel = property.PropertyGetter(bindingContext.Model);
                ModelBindingResult propertyResult;
                using (bindingContext.EnterNestedScope(property, fieldName, modelName, propertyModel))
                {
                    await _formFileModelBinder.BindModelAsync(bindingContext);
                    propertyResult = bindingContext.Result;
                }

                if (propertyResult.IsModelSet)
                {
                    // The IFormFile was succesfully bound, assign it ot the corresponding property of the model
                    property.PropertySetter(model, propertyResult.Model);
                }
                else if (property.IsBindingRequired)
                {
                    string message = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
                    bindingContext.ModelState.TryAddModelError(modelName, message);
                }
            }

            // Set the succesfully constructed model as the result of the model binding
            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
