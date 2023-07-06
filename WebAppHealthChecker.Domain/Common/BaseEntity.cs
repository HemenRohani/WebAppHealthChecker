using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHealthChecker.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }
    }
}
