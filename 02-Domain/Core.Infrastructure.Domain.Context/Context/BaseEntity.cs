using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Infrastructure.Domain.Context.Context
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public bool Status { get; set; }

        //public abstract int getId();
    }
}
