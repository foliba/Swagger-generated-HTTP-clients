using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication.ModelBinder
{
    /// <summary>
    ///     RFC3339: https://tools.ietf.org/html/rfc3339
    ///     If a valid ISO date is given without timezone designator, the date is assumed to be a local date.
    ///     This fails for cases where the API is in a different timezone. This model binder rejects binding of
    ///     ISO dates with missing timezone designator.
    /// </summary>
    /// <inheritdoc />
    public class RFC3339DateTimeModelBinder : IModelBinder
    {
        /// <summary>
        ///     Regex describing the RFC3339 format specification for a date string
        /// </summary>
        private const string RFC_3339_REGEX =
            @"^([0-9]+)-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])[Tt\s]([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]|60)(\.[0-9]+)?(([Zz])|([\+|\-]([01][0-9]|2[0-3])(:[0-5][0-9])?))$";

        /// <inheritdoc />
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrWhiteSpace(value)) return Task.CompletedTask;

            // Check if given string is RFC3339 compatible
            if (!Regex.IsMatch(value, RFC_3339_REGEX))
            {
                bindingContext.ModelState.TryAddModelError(
                    modelName, "date must be valid RFC3339 formatted date string");
                return Task.CompletedTask;
            }

            var result = DateTime.Parse(value).ToUniversalTime();
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}