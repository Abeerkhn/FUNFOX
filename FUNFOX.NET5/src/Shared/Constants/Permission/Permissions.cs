using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FUNFOX.NET5.Shared.Constants.Permission
{
    public static class Permissions
    {
        public static class Classes
        {
            public const string View = "Permissions.Classes.View";
            public const string Create = "Permissions.Classes.Create";
            public const string Edit = "Permissions.Classes.Edit";
            public const string Delete = "Permissions.Classes.Delete";
            public const string Export = "Permissions.Classes.Export";
            public const string Search = "Permissions.Classes.Search";
            public const string EnrolledUser = "Permissions.Classes.ViewEnrolledUser";


        }

        public static class BasicUserPermissions
        {
            
            public const string EnrollInClasses = "Permissions.Classes.Enroll";
            // Add more permissions as needed
        }



       
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }

        public static class Communication
        {
            public const string Chat = "Permissions.Communication.Chat";
        }

        public static class Preferences
        {
            public const string ChangeLanguage = "Permissions.Preferences.ChangeLanguage";

            //TODO - add permissions
        }

        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

       
       
        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permissions = new HashSet<string>(); // Use HashSet to automatically filter out duplicates
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    var permission = propertyValue.ToString();
                    if (!IsBasicUserPermission(permission)) // Exclude permissions from BasicUserPermissions
                        permissions.Add(permission);
                }
            }
            return permissions.ToList(); // Convert HashSet back to List
        }

        public static List<string> GetPermissionsByCategory(string category)
        {
            var permissions = new List<string>();
            var categoryType = typeof(Permissions).GetNestedType(category);
            if (categoryType != null)
            {
                permissions.AddRange(categoryType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where(prop => prop.GetValue(null) is not null)
                    .Select(prop => prop.GetValue(null).ToString()));
            }
            else
            {
                throw new ArgumentException($"Permission category '{category}' not found.");
            }
            return permissions;
        }
        private static bool IsBasicUserPermission(string permission)
        {
            var basicUserPermissions = typeof(BasicUserPermissions).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(prop => prop.GetValue(null) is not null)
                .Select(prop => prop.GetValue(null).ToString());
            return basicUserPermissions.Contains(permission);
        }
    }
}
