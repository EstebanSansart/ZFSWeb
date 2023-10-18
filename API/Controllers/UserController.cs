using API.Dtos;
using API.Dtos.Custom;
using API.Helpers;
using API.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

// User, user, Users, users

public class UserController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public UserController(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var users = await _unitOfWork.Users.GetAll();
        return _mapper.Map<List<UserDto>>(users);
    }
    [HttpGet("Pager")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<UserDto>>> Get11([FromQuery] Params userParams)
    {
        var user = await _unitOfWork.Users.GetAllAsync(userParams.PageIndex,userParams.PageSize,userParams.Search);
        var lstUsersDto = _mapper.Map<List<UserDto>>(user.registros);
        return new Pager<UserDto>(lstUsersDto,userParams.Search,user.totalRegistros,userParams.PageIndex,userParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var user = await _unitOfWork.Users.GetById(id);
        if (user == null){
            return NotFound();
        }
        return _mapper.Map<UserDto>(user);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Post(UserDto userDto){
        var user = _mapper.Map<User>(userDto);
        this._unitOfWork.Users.Add(user);
        await _unitOfWork.SaveAsync();
        if (user == null)
        {
            return BadRequest();
        }
        userDto.UserCc = user.UserCc;
        return CreatedAtAction(nameof(Post),new {id= userDto.UserCc}, userDto);
    }

    [HttpPost]
    [Route("Autenticar")]
    public async Task<IActionResult> Autenticar([FromBody] AuthRequest auth){
        var AuthResult = await _authService.ReturnToken(auth);
        if(AuthResult == null)
            return Unauthorized();
        return Ok(AuthResult);
}

    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]UserDto UserDto)
    {
        if(UserDto == null) return BadRequest();
        User User =  await _unitOfWork.Users.GetById(id);
        _mapper.Map(UserDto,User);
        _unitOfWork.Users.Update(User);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var user = await _unitOfWork.Users.GetById(id);
        if(user == null){
            return NotFound();
        }
        _unitOfWork.Users.Remove(user);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}