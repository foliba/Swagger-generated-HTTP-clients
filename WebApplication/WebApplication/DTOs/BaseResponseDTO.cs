using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace WebApplication.DTOs
{
    /// <summary>
    /// Base response DTO which just adds the current DateTime in UTC to the DTO  
    /// </summary>
    [JsonConverter(typeof(JsonInheritanceConverter), "eventType")]
    [KnownType(typeof(DataResponseDTO))]
    public abstract class BaseResponseDTO
    {
        /// <summary>
        /// Always automatically returns DateTime.UtcNow
        /// </summary>
        public DateTime RequestTime => DateTime.UtcNow;
    }
}