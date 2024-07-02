using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Common
{
    public class StripeServiceOptions
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}
