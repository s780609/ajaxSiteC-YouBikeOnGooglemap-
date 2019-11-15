using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



    namespace ajaxSiteC.Models
    {
        using global::ajaxSiteC.Models;
        using Newtonsoft.Json;
        using System;
        using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType( typeof( CategoriesMetaData ) )]
        public partial class Categories
        {
           
        }

    internal class CategoriesMetaData
    {
        [JsonIgnore]
        public virtual ICollection<Products> Products { get; set; }
    }
}

