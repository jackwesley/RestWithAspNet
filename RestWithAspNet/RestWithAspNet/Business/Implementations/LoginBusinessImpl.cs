﻿using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository;
using RestWithAspNet.Security.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RestWithAspNet.Business.Implementations
{
    public class LoginBusinessImpl : ILoginBusiness
    {
        private readonly IUserRepository _userRepository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public LoginBusinessImpl(IUserRepository userRepository,
                                 SigningConfigurations signingConfigurations,
                                 TokenConfigurations tokenConfigurations)
        {
            _userRepository = userRepository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public object FindByLogin(UserVO user)
        {
            bool credentialIsValid = false;
            if (user != null && !String.IsNullOrEmpty(user.Login) && !String.IsNullOrEmpty(user.AccessKey))
            {
                var baseUser = _userRepository.FindByLogin(user.Login);
                credentialIsValid = (baseUser != null && baseUser.Login == user.Login && baseUser.AccessKey == user.AccessKey);
            }

            if (credentialIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    }
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);
                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }

        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate,
                                   DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {

                autenticated = false,
                message = "Failed to authenticate"
            };

        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
