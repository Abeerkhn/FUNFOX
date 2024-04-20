using FUNFOX.NET5.Domain.Contracts;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Infrastructure.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Infrastructure.Models
{
    public class UserClassEnrollment:AuditableEntity<int>
    {
        public string UserId { get; set; }
        public BlazorHeroUser BlazorHeroUser { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
