using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Api.Extensions
{
    public class AspNetUser : IUser
    {
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private readonly IHttpContextAccessor _accessor;

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.NewGuid();
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
