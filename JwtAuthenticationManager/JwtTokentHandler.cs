using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationManager;

public class JwtTokentHandler
{
    public const string JTW_SECURITY_KEY = "yPKCqn4kSWLtadrfwgdfhsfghfghfghfghfhghfgh";
    private const int JWT_TOKEN_VALIDITY_MINS = 20;
    private readonly List<UserAccount> _userAccounts;
    public JwtTokentHandler()
    {
        _userAccounts = new List<UserAccount>
        {
            new UserAccount { UserName = "admin", Password = "admin", Role = "Administrator" },
            new UserAccount { UserName = "skbt", Password = "skbt", Role = "admin" }
        };
    }

    public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
    {
        if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) ||
            string.IsNullOrWhiteSpace(authenticationRequest.Password))
            return null;
        /*validation*/
        var userAccount = _userAccounts.Where(x =>
            x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
        if (userAccount == null) return null;

        var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        var tokenKey = Encoding.ASCII.GetBytes(JTW_SECURITY_KEY);
        var claimsIdentity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
            new Claim(ClaimTypes.Role, userAccount.Role)
        });

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredentials
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptior);

        var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        return new AuthenticationResponse
        {
            UserName = userAccount.UserName,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            JwtToken = token
        };

    }
    
}