
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNFOX.NET5.Application.Requests.Catalog
{
    public class GetEnrolledUsersRequest: PagedRequest
    {
       
        public string SearchString { get; set; }
    }
}
