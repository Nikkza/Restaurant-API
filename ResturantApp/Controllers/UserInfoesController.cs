using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantApp.Models;
using ResturantApp.Services;
using AutoMapper;
using ResturantApp.Dtos;

namespace ResturantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoesController : ControllerBase
    {
        private readonly IUserService<UserInfo> _repository;
        private readonly IMapper _mapper;
        public UserInfoesController(IUserService<UserInfo> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserReadDto>> Authenticate([FromBody] LoginDto loginDto)
        {

            var userModel = _mapper.Map<UserInfo>(loginDto);
            var password = LogicHandler.Encrypt(userModel.Password);
            userModel = await _repository.Authenticate(userModel.UserName, password);
            if (userModel == null)
            {
                return NotFound("password or username is incorrect");
            }
            return Ok(_mapper.Map<UserReadDto>(userModel));
        }

        // GET: api/UserInfoes
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfo()
        {
            var allUsers = await _repository.GetAllUsers();
            if (allUsers != null)
            {
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(allUsers));
            }
            return NotFound();
        }

        //GET: api/UserInfoes/5
        [HttpGet("{id}", Name = "GetUserInfo")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int id)
        {
            var userById = await _repository.GetUserById(id);
            if (userById != null)
                return Ok(_mapper.Map<UserReadDto>(userById));
            return NotFound();
        }

        //put/userinfo/{id}
        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id,[FromBody] UpdateUserDto updateUserDto)
        {
            var checkUserFRomRepo = await _repository.GetUserById(id);
            if(checkUserFRomRepo == null)
            {
                return NotFound();
            }
           _mapper.Map(updateUserDto, checkUserFRomRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> Register(RegisterUserDto registerUserDto)
        {
            var registerModel = _mapper.Map<UserInfo>(registerUserDto);
            var registerUser = await _repository.RegisterUser(registerModel);
            var readUserModel = _mapper.Map<UserReadDto>(registerUser);
            return CreatedAtRoute(nameof(GetUserInfo), new { Id = readUserModel.UserId }, readUserModel);
        }
    }
}
