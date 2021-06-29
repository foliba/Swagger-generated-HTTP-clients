using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using WebApplication.ModelBinder;

namespace WebApplication.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(JsonInheritanceConverter), "eventType")]
    public class DataRequestDTO
    {
        /// <summary>
        /// DateTime using the <see cref="RFC3339DateTimeModelBinder" />
        /// Required but nullable so we exclude accidentally used default values
        /// </summary>
        [ModelBinder(BinderType = typeof(RFC3339DateTimeModelBinder))]
        [Required]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Page must be a positive non zero integer
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Page must be a positive non zero integer")]
        public int? MaxResultCount { get; set; }
    }
}