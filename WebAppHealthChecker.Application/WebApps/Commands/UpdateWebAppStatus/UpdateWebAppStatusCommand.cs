using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHealthChecker.Application.WebApps.Commands.UpdateWebAppStatus
{
    public record UpdateWebAppStatusCommand : IRequest<bool>
    {
        public Guid Guid { get; set; }
        public DateTime? LastCheck { get; set; }
        public int LastStatusCode { get; set; }
    }
}
