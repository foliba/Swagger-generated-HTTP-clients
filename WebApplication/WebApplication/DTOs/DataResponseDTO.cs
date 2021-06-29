using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ModelBinder;

namespace WebApplication.DTOs
{
    /// <summary>
    /// </summary>
    public class DataResponseDTO : BaseResponseDTO
    {
        /// <summary>
        ///     We don't have to make this nullable, as we are in control of creating the response and can skip some<br />
        ///     input validation
        /// </summary>
        [ModelBinder(BinderType = typeof(RFC3339DateTimeModelBinder))]
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Just some list of strings as an example for some loaded data
        /// </summary>
        public List<string> DataSet { get; set; }
    }
}