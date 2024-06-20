using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Abstracts
{
    public interface IPageable
    {
        int Page { get; set; }
        int Size { get; set; }
    }
}
