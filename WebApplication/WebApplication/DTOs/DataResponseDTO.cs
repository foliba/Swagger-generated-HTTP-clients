using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ModelBinder;

namespace WebApplication.DTOs
{
    public class DataResponseDTO : BaseResponseDTO
    {
        [ModelBinder(BinderType = typeof(RFC3339DateTimeModelBinder))]
        public DateTime Date { get; set; }
        
        public List<string> DataSet { get; set; }
    }
}