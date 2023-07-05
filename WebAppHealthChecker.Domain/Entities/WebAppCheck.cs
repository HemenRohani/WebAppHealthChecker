using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppHealthChecker.Domain.Common;

namespace WebAppHealthChecker.Domain.Entities
{
    public class WebAppCheck : BaseEntity
    {
        public int WebAppId { get; set; }
        [ForeignKey(nameof(WebAppId))]
        public virtual WebApp WebApp { get; set; }
        public DateTime DateTime { get; set; }
        public int StatusCode { get; set; }
        public virtual ICollection<WebAppCheck> Checks { get; set; }
    }
}
