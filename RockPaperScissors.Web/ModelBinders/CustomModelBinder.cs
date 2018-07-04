using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace RockPaperScissors.Web.ModelBinders
{
    public class CustomModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));
            var values = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (bindingContext.BindingSource == null || values.Length == 0)
                return Task.CompletedTask;

            var type = Type.GetType(bindingContext.ModelType.AssemblyQualifiedName);
            var result = JsonConvert.DeserializeObject(values.FirstValue, type);
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }
}