using System;
using System.Collections.Generic;
using System.Text;
using Core.Infrastructure.Domain.Aggregate;
using Core.Infrastructure.Domain.Aggregate.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefType:BaseEntity
    {
        private readonly ILazyLoader lazyLoader;

        public RefType(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }

        public RefType(bool status, DateTime? insertDate, string name, bool isActive)
        {
            this.Status = status;
            this.InsertDate = insertDate;
            this.Name = name;
            this.IsActive = isActive;
        }

        public RefType(bool status, string name, bool isActive, DateTime? updateDate)
        {
            this.Status = status;
            this.InsertDate = updateDate;
            this.Name = name;
            this.IsActive = isActive;
        }
        public string Name { get; protected set; }

        public RefType Parent
        {
            get => lazyLoader.Load(this, ref parent);
            set => parent = value;
        }

        public RefType parent;

        public void SetParent(RefType parent)
        {
            this.Parent = parent;
        }
        public void Update(bool status, string name, bool isActive, DateTime? updateDate)
        {
            this.Status = status;
            this.InsertDate = updateDate;
            this.Name = name;
            this.IsActive = isActive;
        }
    }
}
