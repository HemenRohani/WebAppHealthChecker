using System.ComponentModel.DataAnnotations.Schema;
using WebAppHealthChecker.Domain.Common;

namespace WebAppHealthChecker.Domain.Entities
{
    public class WebApp : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int  CheckInterval { get; set; }
        public DateTime LastCheck { get; set; }

    }
}
