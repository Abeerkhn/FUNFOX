using FUNFOX.NET5.Application.Interfaces.Chat;
using FUNFOX.NET5.Application.Models.Chat;
using FUNFOX.NET5.Domain.Contracts;
using FUNFOX.NET5.Domain.Entities.Catalog;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FUNFOX.NET5.Infrastructure.Models.Identity
{
    public class BlazorHeroUser : IdentityUser<string>, IChatUser, IAuditableEntity<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string CreatedBy { get; set; }

        [Column(TypeName = "text")]
        public string ProfilePictureDataUrl { get; set; }
        public ICollection<Class> EnrolledClasses { get; set; } = new HashSet<Class>();
        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<ChatHistory<BlazorHeroUser>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<BlazorHeroUser>> ChatHistoryToUsers { get; set; }
        public Guid? VerificationToken { get; set; }

        public BlazorHeroUser()
        {
            ChatHistoryFromUsers = new HashSet<ChatHistory<BlazorHeroUser>>();
            ChatHistoryToUsers = new HashSet<ChatHistory<BlazorHeroUser>>();
        }
    }
}