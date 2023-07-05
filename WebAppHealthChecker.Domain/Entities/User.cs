using System.ComponentModel.DataAnnotations;
using WebAppHealthChecker.Domain.Common;

namespace WebAppHealthChecker.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }


        public virtual ICollection<WebApp> Apps { get; set; }
    }
}
