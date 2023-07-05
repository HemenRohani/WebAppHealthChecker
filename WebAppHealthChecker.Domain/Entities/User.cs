﻿using WebAppHealthChecker.Domain.Common;

namespace WebAppHealthChecker.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<WebApp> Apps { get; set; }
    }
}
