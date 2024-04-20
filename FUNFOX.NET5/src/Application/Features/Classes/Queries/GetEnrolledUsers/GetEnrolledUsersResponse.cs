using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Features.Products.Queries.GetEnrolledUsers
{
    public class GetEnrolledUsersResponse
    {

        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public int ClassId { get; set; }
        
    }
}
