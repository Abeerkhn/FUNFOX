using FUNFOX.NET5.Domain.Contracts;

using FUNFOX.NET5.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FUNFOX.NET5.Domain.Entities.Catalog
{
    public class Class : AuditableEntity<int>
    {
        public string Name { get; set; }
        
        [Column(TypeName = "text")]
        public string ImageDataURL { get; set; }

        public string Description { get; set; }
      
        public string Level { get; set; }
        public string StartTime { get; set; }

        public string EndTime { get; set; }
        public int MaxClassSize { get; set; }

        public string Frequency { get; set; }



    }
    
}