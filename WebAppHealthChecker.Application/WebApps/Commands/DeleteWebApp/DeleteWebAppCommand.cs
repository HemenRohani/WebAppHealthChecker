using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHealthChecker.Application.WebApps.Commands.DeleteWebApp
{
    public record DeleteWebAppCommand : IRequest<Guid>
    {
        public Guid Guid { get; set; }
    }
}
