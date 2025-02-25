﻿using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume.Application.Modules.AccountModule.Commands.PrincipalFillCommand;
using System.Reflection;
using System.Security.Claims;

namespace Project.Api.AppCode.Pipeline
{
    public class AppClaimsTransformation : IClaimsTransformation
    {
        private readonly IMediator mediator;

        internal static string[] policies = null;

        static AppClaimsTransformation()
        {
            policies = GetPolicies();

        }

        public AppClaimsTransformation(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal?.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {

                await mediator.Send(new PrincipalFillRequest(identity));
            }

            return principal;
        }

        static string[] GetPolicies()
        {
            var types = typeof(AppClaimsTransformation).Assembly.GetTypes();

            var policies = types
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
                .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                .Union(
                types
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic
                 && !method.IsDefined(typeof(NonActionAttribute), true)
                 && method.IsDefined(typeof(AuthorizeAttribute), true))
                 .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                )
                .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                .SelectMany(a => a.Policy.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .ToArray();

            return policies;
        }
    }
}
