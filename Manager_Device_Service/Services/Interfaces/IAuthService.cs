using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Model.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Manager_Device_Service.Services.Interfaces
{
    public interface IAuthService
    {

        Task<string> CreateToken(User user);

        Task<bool> SendOtpMail(string toMail, string subject, string body);
    
        Task<string> VerifyOtpLoginEmail(VerifyOtpLoginEmailRequest request);

        #region Token
        JwtSecurityToken CreateToken(List<Claim> claims);

        string CreateRefreshToken();

        Task<bool> RevokeRefreshToken(string username);

        Task<LoginResult> RefreshTokenNoTranRole(string refreshToken);

        Task<LoginResult> RefreshToken(string refreshToken);

        #endregion


        #region User
        Task<bool> Logout();

        #endregion
    }
}
