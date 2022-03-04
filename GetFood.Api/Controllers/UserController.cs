﻿using GetFood.Business.Abstract;
using GetFood.Entities.Base;
using GetFood.Entities.Dtos;
using GetFood.Entities.IBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {

        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IResponse<List<UserDto>> GetAll()
        {
            var response = service.GetAll();
            return response;

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IResponse<UserDto> Find(int id)
        {
            var response = service.Find(id);
            return response;
        }

        [HttpGet("GetUser")]
        public IResponse<UserDto> GetUser()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = service.ReadUserToken(identity);

            if(userId > 0)
            {
                var response = service.Find(userId);
                return response;
            }
            else
            {
                return new Response<UserDto>
                {
                    Message = "Success",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null
                };

            }


            #region RESERVE IDENTITY READ PROCESS
            /*
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                var email = identity.FindFirst(ClaimTypes.Email);
                // or
                //identity.FindFirst("ClaimName").Value;
                var newtest = claims;
                var new2 = claims.ToList();
                var new3 = new2[1];
                var new35 = new3.Value;
                var new4 = "hello";

                //return new35;
            }
            */
            #endregion



        }

        [HttpGet("GetRestaurantOfUser")]
        public IResponse<RestaurantDto> GetRestaurantOfUser()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = service.ReadUserToken(identity);
            var response = service.GetRestaurantOfUser(userId);
            return response;

        }



        [HttpPost("Register")]
        [AllowAnonymous]
        public IResponse<UserToken> Register(UserRegisterDto userRegister)
        {
            var response = service.Register(userRegister);
            return response;

        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public IResponse<UserToken> Login(UserLoginDto userLogin)
        {
            var response = service.Login(userLogin);
            return response;
        }



        /*
        It is temporary, it will take id from token       
        */

        [HttpPost("BindRestaurant")]
        [AllowAnonymous]
        public IResponse<UserDto> BindRestaurantToUser(RestaurantCreateDto restaurant)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = service.ReadUserToken(identity);
            var response = service.BindRestaurantToUser(userId, restaurant);
            return response;

        }



        









        [HttpGet("TokenTest")]
        public string TokenTester()
        {
            return "have token";
        }



        [HttpGet("TokenRead")]
        [AllowAnonymous]
        public string TokenRead()
        {
            /*
            TOKEN READ TEST
            */
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                var email = identity.FindFirst(ClaimTypes.Email);
                // or
                //identity.FindFirst("ClaimName").Value;
                var newtest = claims;
                var new2 = claims.ToList();
                var new3 = new2[1];
                var new35 = new3.Value;
                var new4 = "hello";

                return new35;

            }




            return "hello token";





        }






    }
}
