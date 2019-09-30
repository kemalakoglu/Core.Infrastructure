using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Infrastructure.Domain.Aggregate;
using Core.Infrastructure.Domain.Aggregate.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefValue:BaseEntity
    {
        private readonly ILazyLoader lazyLoader;
        public RefValue() { }
        public RefValue(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        public RefValue(string value, bool status, DateTime? insertDate, DateTime? updateDate, bool isActive, RefType refType, string name)
        {
            this.Status = status;
            this.InsertDate = insertDate;
            this.UpdateDate = updateDate;
            this.Value = value;
            this.IsActive = isActive;
            this.RefType = refType;
            this.Name = name;
        }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Value { get; protected set; }
        public string Name { get; protected set; }
        public RefType RefType
        {
            get => lazyLoader.Load(this, ref refType);
            set => refType = value;
        }

        public RefType refType;
    }
}
