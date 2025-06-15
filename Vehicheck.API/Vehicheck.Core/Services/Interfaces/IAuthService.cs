using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;

namespace Vehicheck.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(UserLoginRequest request);
    }
}
