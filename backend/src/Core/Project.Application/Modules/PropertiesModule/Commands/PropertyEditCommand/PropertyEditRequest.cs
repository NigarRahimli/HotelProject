using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand
{
    public class PropertyEditRequest : IRequest<Property>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GuestNum { get; set; }
        public int DescriptionId { get; set; }
        public int KindId { get; }
    }
}
