﻿
using Project.Infrastructure.Concretes;

namespace Project.Domain.Models.Entities
{
    public class Safety:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
