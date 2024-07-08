using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Abstracts
{
    public interface ISMSService
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}
