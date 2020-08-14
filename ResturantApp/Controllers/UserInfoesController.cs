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
        private readonly IUserService _repository;
        private readonly IContex _context;
        private readonly IMapper _mapper;
        public UserInfoesController(IUserService repository, IMapper mapper, IContex context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;

        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserReadDto>> Authenticate([FromBody] LoginDto loginDto)
        {

            var userModel = _mapper.Map<UserInfo>(loginDto);
            var password = DePasswordHandler.Encrypt(userModel.Password);
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
            var allUsers = await _context.GetAllObjects();
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
            var userById = await _context.GetObjectById(id);
            if (userById != null)
                return Ok(_mapper.Map<UserReadDto>(userById));
            return NotFound();
        }

        //put/userinfo/{id}
        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id,[FromBody] UpdateUserDto updateUserDto)
        {
            var checkUserFRomRepo = await _context.GetObjectById(id);
            if(checkUserFRomRepo == null)
            {
                return NotFound();
            }
           _mapper.Map(updateUserDto, checkUserFRomRepo);
            _context.SaveChanges();
            return NoContent();
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> Register(RegisterUserDto registerUserDto)
        {
            var registerModel = _mapper.Map<UserInfo>(registerUserDto);
            var registerUser = await _context.RegisterObject(registerModel);
            var readUserModel = _mapper.Map<UserReadDto>(registerUser);
            return CreatedAtRoute(nameof(GetUserInfo), new { Id = readUserModel.UserId }, readUserModel);
        }
    }
}
