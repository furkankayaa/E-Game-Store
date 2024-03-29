﻿using App.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.API.Data
{
    public class AccessTokenGenerator
    {
        public AuthContext _context { get; set; }
        public IConfiguration _config { get; set; }
        public AppUser _appUser { get; set; }
        public string _appRole { get; set; }

        /// <summary>
        /// Class'ın oluşturulması.
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_config"></param>
        /// <param name="_appUser"></param>
        /// <returns></returns>
        public AccessTokenGenerator(AuthContext context,
                                    IConfiguration config,
                                    AppUser appUser,
                                    string appRole)
        {
            _config = config;
            _context = context;
            _appUser = appUser;
            _appRole = appRole;
        }

        /// <summary>
        /// Kullanıcı üzerinde tanımlı tokenı döner;Token yoksa oluşturur. Expire olmuşsa update eder.
        /// </summary>
        /// <returns></returns>
        public AppUserTokens GetToken()
        {
            AppUserTokens userTokens = null;
            TokenInfo tokenInfo = null;

            //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
            if (_context.AppUserTokens.Count(x => x.UserId == _appUser.Id) > 0)
            {
                //İlgili token bilgileri bulunur.
                userTokens = _context.AppUserTokens.FirstOrDefault(x => x.UserId == _appUser.Id);

                //Expire olmuş ise yeni token oluşturup günceller.
                if (userTokens.ExpireDate <= DateTime.Now)
                {
                    //Create new token
                    tokenInfo = GenerateToken();

                    userTokens.ExpireDate = tokenInfo.ExpireDate;
                    userTokens.Value = tokenInfo.Token;

                    _context.AppUserTokens.Update(userTokens);
                }
            }
            else
            {
                //Create new token
                tokenInfo = GenerateToken();

                userTokens = new AppUserTokens();

                userTokens.UserId = _appUser.Id;
                userTokens.LoginProvider = "SystemAPI";
                userTokens.Name = _appUser.FullName;
                userTokens.ExpireDate = tokenInfo.ExpireDate;
                userTokens.Value = tokenInfo.Token;
                userTokens.Role = _appRole;

                _context.AppUserTokens.Add(userTokens);
            }

            _context.SaveChangesAsync();

            return userTokens;
        }

        /// <summary>
        /// Kullanıcıya ait tokenı siler.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteToken()
        {
            bool ret = true;

            try
            {
                //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
                if (_context.AppUserTokens.Count(x => x.UserId == _appUser.Id) > 0)
                {
                    AppUserTokens userTokens = _context.AppUserTokens.FirstOrDefault(x => x.UserId == _appUser.Id);

                    _context.AppUserTokens.Remove(userTokens);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Yeni token oluşturur.
        /// </summary>
        /// <returns></returns>
        private TokenInfo GenerateToken()
        {
            //ExpireDate set ediliyor
            DateTime expireDate = DateTime.Now.AddMinutes(60);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Application:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config["Application:Audience"],
                Issuer = _config["Application:Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //Claim tanımları yapılır. Burada en önemlisi Id ve emaildir.
                    //Id üzerinden, aktif kullanıcıyı buluyor olacağız.
                    new Claim(ClaimTypes.NameIdentifier, _appUser.Id),
                    new Claim(ClaimTypes.Name, _appUser.FullName),
                    new Claim(ClaimTypes.Email, _appUser.Email),
                    new Claim(ClaimTypes.Role, _appRole)
                }),

                //ExpireDate
                Expires = expireDate,

                //Şifreleme türünü belirtiyoruz: HmacSha256Signature
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            TokenInfo tokenInfo = new TokenInfo();

            tokenInfo.Token = tokenString;
            tokenInfo.ExpireDate = expireDate;
            tokenInfo.Role = _appRole;

            return tokenInfo;
        }
    }
}