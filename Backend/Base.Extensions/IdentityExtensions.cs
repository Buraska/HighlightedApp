﻿using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
// using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

// using Microsoft.IdentityModel.Tokens;

namespace Base.Extensions;

public static class IdentityExtensions
{
    // this - keyword. Adds extension method to static class
    // (methods to existing types without creating a new derived type). SO now user has method GetUserId
    public static Guid GetUserId(this ClaimsPrincipal user) => GetUserId<Guid>(user);
    public static TKeyType GetUserId<TKeyType>(this ClaimsPrincipal user)
    {
        if (typeof(TKeyType) != typeof(Guid) &&
            typeof(TKeyType) != typeof(string) &&
            typeof(TKeyType) != typeof(int)
           )
        {
            throw new ApplicationException($"This type of User id {typeof(TKeyType).Name} is not supported!");
        }

        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (idClaim == null)
        {
            throw new NullReferenceException("NameIdentifier claim not found");
        }

        var res = (TKeyType) TypeDescriptor
            .GetConverter(typeof(TKeyType))
            .ConvertFromInvariantString(idClaim.Value)!;
        
        return res;
    }

    public static string GetUserName(this ClaimsPrincipal user)
    {
        return
            (user.Claims.FirstOrDefault(c => c.Type == "aspnet.firstname")?.Value ?? "???") +
            " " +
            (user.Claims.FirstOrDefault(c => c.Type == "aspnet.lastname")?.Value ?? "???");

    }

    public static string GenerateJwt(
        IEnumerable<Claim> claims, 
        string key, 
        string issuer, string audience, // how made jwt, who should receive it
        DateTime expirationDateTime)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(
            signingKey, 
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expirationDateTime,
            signingCredentials:signingCredentials
            );
 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 