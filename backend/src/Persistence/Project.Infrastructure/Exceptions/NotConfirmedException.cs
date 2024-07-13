using System;
using System.Collections.Generic;

namespace Project.Infrastructure.Exceptions
{
    public class NotConfirmedException : Exception
    {
        public string Property { get; set; }
        public NotConfirmedException(string property) : base($"{property.ToUpper()} not confirmed. Please confirm your {property.ToUpper()}.")
        {
            Property = property;
        }
    }
}
