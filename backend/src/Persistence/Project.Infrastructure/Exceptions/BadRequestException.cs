﻿namespace Project.Infrastructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
                : base(message)
        {
        }

        public BadRequestException(string message, Dictionary<string, IEnumerable<string>> errors)
                : this(message)
        {
            Errors = errors;
        }

        public Dictionary<string, IEnumerable<string>> Errors { get; private set; }
    }
}
