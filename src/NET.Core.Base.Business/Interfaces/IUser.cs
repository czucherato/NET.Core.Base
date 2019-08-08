using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace NET.Core.Base.Business.Interfaces
{
    public interface IUser
    {
        string Name { get; }

        Guid GetUserId();

        string GetUserEmail();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetClaimsIdentity();
    }
}
