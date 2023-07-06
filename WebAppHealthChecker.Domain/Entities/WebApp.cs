using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppHealthChecker.Domain.Common;

namespace WebAppHealthChecker.Domain.Entities
{
    public class WebApp : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
        public int  CheckInterval { get; set; }
        public DateTime? LastCheck { get; set; }
        public int LastStatusCode { get; set; }

    }
}
