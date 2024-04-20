using System.Collections.Generic;

namespace FUNFOX.NET5.Application.Responses.Identity
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; }
    }
}