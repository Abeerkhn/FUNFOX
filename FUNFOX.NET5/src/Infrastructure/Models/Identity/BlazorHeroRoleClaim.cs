﻿using FUNFOX.NET5.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace FUNFOX.NET5.Infrastructure.Models.Identity
{
    public class BlazorHeroRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        public string Description { get; set; }
        public string Group { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual BlazorHeroRole Role { get; set; }

        public BlazorHeroRoleClaim() : base()
        {
        }

        public BlazorHeroRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}