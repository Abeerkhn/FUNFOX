﻿using FUNFOX.NET5.Domain.Contracts;
using Microsoft.AspNetCore.Components;
using System;

namespace FUNFOX.NET5.Client.Shared.Components
{
    public abstract partial class ExtendedAttributes<TId, TEntityId, TEntity, TExtendedAttribute>
        : ExtendedAttributesBase<TId, TEntityId, TEntity, TExtendedAttribute>
            where TEntity : AuditableEntity<TEntityId>, IEntityWithExtendedAttributes<TExtendedAttribute>, IEntity<TEntityId>
            where TExtendedAttribute : AuditableEntityExtendedAttribute<TId, TEntityId, TEntity>, IEntity<TId>
            where TId : IEquatable<TId>
    {
        protected override RenderFragment Inherited() => builder => base.BuildRenderTree(builder);
    }
}