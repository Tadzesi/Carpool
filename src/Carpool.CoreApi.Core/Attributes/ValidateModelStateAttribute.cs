using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Carpool.CoreApi.Core.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                foreach (ParameterInfo parameter in descriptor.MethodInfo.GetParameters())
                {
                    object? args = null;
                    if (context.ActionArguments.ContainsKey(parameter.Name!))
                    {
                        args = context.ActionArguments[parameter.Name!];
                    }

                    ValidateAttributes(parameter, args, context.ModelState);
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        private static void ValidateAttributes(ParameterInfo parameter, object? args, ModelStateDictionary modelState)
        {
            if (parameter?.Name != null)
            {
                foreach (CustomAttributeData attributeData in parameter.CustomAttributes)
                {
                    Attribute? attributeInstance = parameter.GetCustomAttribute(attributeData.AttributeType);
                    if (attributeInstance is ValidationAttribute validationAttribute)
                    {
                        bool isValid = validationAttribute.IsValid(args);
                        if (!isValid)
                        {
                            modelState.AddModelError(parameter.Name, validationAttribute.FormatErrorMessage(parameter.Name));
                        }
                    }
                }
            }
        }
    }
}
