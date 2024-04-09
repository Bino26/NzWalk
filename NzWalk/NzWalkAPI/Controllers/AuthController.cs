﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NzWalkAPI.Models.DTO;
using NzWalkAPI.Repositories;

namespace NzWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Register User
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email,


            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add Roles to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }

                }
            }

            return BadRequest("Something went wrong");
        }

        //Login
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if(user != null) {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get Roles of the user
                    var roles = await userManager.GetRolesAsync(user);

                    //Create Token

                    var jwtToken =  tokenRepository.CreateJWTToken(user, roles.ToList());
                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken
                    };
                    return Ok(response);
                }
            }
            return BadRequest("Username or Password is incorrect");
        }

        //GetById
        [HttpGet]
        [Route("Getuser/{id}")]
        [Authorize]

        public async Task<IActionResult> GetUser([FromRoute]string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("User not found");
        }

        [HttpDelete]
        [Route("Deleteuser/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteUser([FromRoute]string id)
        {
            var user = userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
               

            }

            return BadRequest("User not found");
        }

    }



}


