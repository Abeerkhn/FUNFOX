using FUNFOX.NET5.Domain.Entities.Catalog;
using System;

namespace FUNFOX.NET5.Application.Features.Classes.Queries.GetAllPaged
{
    public class GetAllPagedClassesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        public string Description { get; set; }
       public string Level { get; set; }
        public string  StartTime { get; set; }
        public string Frequency { get; set; }
        public string EndTime { get; set; }

        public int Maxclasssize { get; set; }
        public string ImageUrl { get; set; }
    
    }

   
}