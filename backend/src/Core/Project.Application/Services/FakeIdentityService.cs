using Project.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services
{
    class FakeIdentityService : IIdentityService
    {
        public int? GetPrincipialId()
        {
            return 1;
        }
    }
}
