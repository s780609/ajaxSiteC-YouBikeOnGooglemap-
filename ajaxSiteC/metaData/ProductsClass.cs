using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ajaxSiteC.Models
{
    [MetadataType(typeof(ProductsMetaData))]
    public partial class Products
    {
     
    }

    internal class ProductsMetaData
    {
        [JsonIgnore]
        public virtual Categories Categories { get; set; }
    }
}