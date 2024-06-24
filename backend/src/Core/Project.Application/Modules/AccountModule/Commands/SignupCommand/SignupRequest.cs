﻿using MediatR;

namespace Project.Application.Modules.AccountModule.Commands.SignupCommand
{
    public class SignupRequest : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
