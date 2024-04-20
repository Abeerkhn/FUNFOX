using System.Linq;

namespace FUNFOX.NET5.Client.Infrastructure.Routes
{
    public static class ClassesEndpoints
    {
        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/Classes?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;
        }

        public static string GetCount = "api/v1/class/count";

        public static string GetProductImage(int productId)
        {
            return $"api/v1/Classes/image/{productId}";
        }
        public static string GetEnrolledUsers(int pageNumber, int pageSize, string searchString, string[] orderBy,int classid)
        {
            var url = $"api/v1/Classes/Enrolled-Users/?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // remove trailing comma
            }
            return $"{url}&classId={classid}";
        }

       

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string EnrolledUsersExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Save = "api/v1/Classes";
        public static string Delete = "api/v1/Classes";
        public static string Export = "api/v1/Classes/export";
        public static string EnrolledUsersExport = "api/v1/Classes/export-enrolledusers";
        public static string ChangePassword = "api/identity/account/changepassword";
        public static string UpdateProfile = "api/identity/account/updateprofile";
        public static string EnrollUsers = "api/v1/Classes/enroll-user";
        public static string DeleteEnrollment = "api/v1/Classes/delete-enrollment";
    }
}