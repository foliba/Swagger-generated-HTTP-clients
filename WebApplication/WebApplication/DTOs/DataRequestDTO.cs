using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using WebApplication.ModelBinder;

namespace WebApplication.DTOs
{
    [JsonConverter(typeof(JsonInheritanceConverter), "eventType")]
    public class DataRequestDTO
    {
        [ModelBinder(BinderType = typeof(RFC3339DateTimeModelBinder))]
        [Required]
        public DateTime? Date { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page must be a positive non zero integer")]
        public int? MaxResultCount { get; set; }
    }
}