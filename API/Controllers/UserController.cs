using Api.Dtos;
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
    public async  Task<ActionResult<IEnumerable<UserNoLevelDto>>> Get()
    {
        var users = await _unitOfWork.Users.GetAll();
        return _mapper.Map<List<UserNoLevelDto>>(users);
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
    public async Task<ActionResult<UserDto>> Get(string id)
    {
        var user = await _unitOfWork.Users.GetByIdString(id);
        if (user == null){
            return NotFound();
        }
        return _mapper.Map<UserDto>(user);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Post(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Password = await _unitOfWork.Users.GenerarPasswordAleatoria();
        user.LevelId = 1;
        _unitOfWork.Users.Add(user);
        await _unitOfWork.SaveAsync();

        if (user == null)
        {
            return BadRequest();
        }

        userDto.UserCc = user.UserCc;
        return CreatedAtAction(nameof(Post), new { id = userDto.UserCc }, userDto);
    }


    [HttpPost("ActualizarPuntos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<UserDto> ActualizarPuntos(string Cedula)
    {
        var Usuario = await _unitOfWork.Users.ActualizarNivelSegunPuntosAsistencia(Cedula);

        return _mapper.Map<UserDto>(Usuario);
    }
    


    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginDto>> PostLogin(LoginDto loginDto){
        bool autenticacion = await _unitOfWork.Users.ValidarUsuario(loginDto.UserCc,loginDto.Password);
        if (autenticacion)
        {
           
            return Ok("Usuario logueado");
        }
        return NotFound();
    }

    [HttpPost("Autenticar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Autenticar([FromBody] AuthRequest auth)
    {
        var AuthResult = await _authService.ReturnToken(auth);
        if(AuthResult == null)
            return NotFound();
            Console.WriteLine(AuthResult);
        return Ok(AuthResult);
     }

     [HttpPost("TokenValidate")]
     //[Authorize(Roles="")]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]


     public async Task<ActionResult<bool>> ValidarToken(string Token)
     {
        if(Token != null)
        {

        return  Ok(_authService.ValidarToken(Token));
        }

        return Ok(false) ;
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


    [HttpPut("UpdatePassword")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> UpdatePassword(AuthRequest dataUpdate)
    {
        User user = await _unitOfWork.Users.GetByIdString(dataUpdate.Cedula);
        user.Password = dataUpdate.Password;

        

        int num = await _unitOfWork.SaveAsync();

        if(num != 0) return Ok($"Contraseña actualizada");

        return BadRequest();

    }
    

}