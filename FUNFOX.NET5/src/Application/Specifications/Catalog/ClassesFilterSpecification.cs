using FUNFOX.NET5.Application.Specifications.Base;
using FUNFOX.NET5.Domain.Entities.Catalog;

namespace FUNFOX.NET5.Application.Specifications.Catalog
{
    public class ClassesFilterSpecification : HeroSpecification<Class>
    {
        public ClassesFilterSpecification(string searchString)
        {
          
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => (p.Name.Contains(searchString) || p.Description.Contains(searchString)); 
            }
            else
            {
                Criteria = p => !string.IsNullOrEmpty(p.Name) || !string.IsNullOrEmpty(p.Description);
            }
        }
    }
}