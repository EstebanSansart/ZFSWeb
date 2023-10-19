using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

// UserReaction, userReaction, UserReactions, userReactions

public class UserReactionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserReactionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<UserReactionDto>>> Get()
    {
        var userReactions = await _unitOfWork.UserReactions.GetAll();
        return _mapper.Map<List<UserReactionDto>>(userReactions);
    }
    [HttpGet("Pager")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<UserReactionDto>>> Get11([FromQuery] Params userReactionParams)
    {
        var userReaction = await _unitOfWork.UserReactions.GetAllAsync(userReactionParams.PageIndex,userReactionParams.PageSize,userReactionParams.Search);
        var lstUserReactionsDto = _mapper.Map<List<UserReactionDto>>(userReaction.registros);
        return new Pager<UserReactionDto>(lstUserReactionsDto,userReactionParams.Search,userReaction.totalRegistros,userReactionParams.PageIndex,userReactionParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReactionDto>> Get(int id)
    {
        var userReaction = await _unitOfWork.UserReactions.GetById(id);
        if (userReaction == null){
            return NotFound();
        }
        return _mapper.Map<UserReactionDto>(userReaction);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserReaction>> Post(UserReactionDto userReactionDto){
        var userReaction = _mapper.Map<UserReaction>(userReactionDto);
        this._unitOfWork.UserReactions.Add(userReaction);
        await _unitOfWork.SaveAsync();
        if (userReaction == null)
        {
            return BadRequest();
        }
        userReactionDto.UserCc = userReaction.UserCc;
        return CreatedAtAction(nameof(Post),new {id= userReactionDto.UserCc}, userReactionDto);
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(string UserCc,int ReactionId , [FromBody]UserReactionDto UserReactionDto)
    {
        if(UserReactionDto == null)
            return BadRequest();

        UserReaction UserReaction = await _unitOfWork.UserReactions.GetByIdAsync(UserCc,ReactionId);
        _unitOfWork.UserReactions.Remove(UserReaction);
        await _unitOfWork.SaveAsync();
        _mapper.Map(UserReactionDto, UserReaction);
        
        _unitOfWork.UserReactions.Add(UserReaction);

        int num = await _unitOfWork.SaveAsync();

        if(num == 0)
            return BadRequest();

        return Ok("REGISTRO ACTUALIZADO CON EXITO");
    }
    [HttpDelete("{UserCc},{ReactionId}")]  
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(string UserCc,int ReactionId)
    {
        UserReaction UserReaction = await _unitOfWork.UserReactions.GetByIdAsync(UserCc,ReactionId);

        if(UserReaction == null)
            return BadRequest();

        _unitOfWork.UserReactions.Remove(UserReaction);

        int num = await _unitOfWork.SaveAsync();

        if (num == 0)
            return BadRequest();

        return NoContent();
    }
}