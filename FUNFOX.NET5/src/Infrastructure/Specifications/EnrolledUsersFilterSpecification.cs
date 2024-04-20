using FUNFOX.NET5.Application.Specifications.Base;
using FUNFOX.NET5.Domain.Entities.Catalog;
using FUNFOX.NET5.Infrastructure.Models;

namespace FUNFOX.NET5.Application.Specifications.Catalog
{
    public class EnrolledUsersFilterSpecification : HeroSpecification<UserClassEnrollment>
    {
        public EnrolledUsersFilterSpecification(string searchString)
        {
          
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => (p.BlazorHeroUser.FirstName.Contains(searchString) || p.Class.Name.Contains(searchString)); 
            }
            else
            {
                Criteria = p => !string.IsNullOrEmpty(p.BlazorHeroUser.FirstName) || !string.IsNullOrEmpty(p.Class.Name);
            }
        }
    }
}