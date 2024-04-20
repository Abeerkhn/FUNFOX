using FUNFOX.NET5.Application.Responses.Identity;
using System.Collections.Generic;

namespace FUNFOX.NET5.Application.Requests.Identity
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}