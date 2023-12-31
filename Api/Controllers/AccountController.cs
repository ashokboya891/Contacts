using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Extensions;
using AutoMapper;
using Core.Entites.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,ITokenService tokenService,IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
               var user= await _userManager.FindByEmailAsync(loginDto.Email);
            if(user==null) return Unauthorized("Login credentials are required");
            var result=await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            if(!result.Succeeded) return Unauthorized("your are not authorized");
            // var userDto = _mapper.Map<UserDto>(user);
            // return userDto;
            return new UserDto
            {
                Email=user.Email,
                DisplayName=user.DisplayName,
                Token=_tokenService.CreateToken(user)
                
            };
        }
            [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return  BadRequest("email already existed try with another one");
            }
            var user=new AppUser
            {
                DisplayName=registerDto.DisplayName,
                Email=registerDto.Email,
                UserName=registerDto.Email
            };
            var result=await _userManager.CreateAsync(user,registerDto.Password);
            if(!result.Succeeded) return BadRequest("register stopper abruptly");
            return new UserDto{
                Email=registerDto.Email,
                DisplayName=registerDto.DisplayName,
                Token=_tokenService.CreateToken(user)
            };
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery]string email)
        {
            return await _userManager.FindByEmailAsync(email)!=null;
        }
        
        [HttpPut("address")]
        public async Task<ActionResult<DetailsDto>> UpdateUserAddress(DetailsDto details)
        {
            var user= await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            user.Details=_mapper.Map<DetailsDto,Details>(details);
            var result= await _userManager.UpdateAsync(user);
            if(result.Succeeded) return Ok(user.Details);
            return BadRequest("Problem updating user");

        }
    
     
    }
}