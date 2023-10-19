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

// UserTag, userTag, UserTags, userTags

public class UserTagController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserTagController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<UserTagDto>>> Get()
    {
        var userTags = await _unitOfWork.UserTags.GetAll();
        return _mapper.Map<List<UserTagDto>>(userTags);
    }
    [HttpGet("Pager")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<UserTagDto>>> Get11([FromQuery] Params userTagParams)
    {
        var userTag = await _unitOfWork.UserTags.GetAllAsync(userTagParams.PageIndex,userTagParams.PageSize,userTagParams.Search);
        var lstUserTagsDto = _mapper.Map<List<UserTagDto>>(userTag.registros);
        return new Pager<UserTagDto>(lstUserTagsDto,userTagParams.Search,userTag.totalRegistros,userTagParams.PageIndex,userTagParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserTagDto>> Get(int id)
    {
        var userTag = await _unitOfWork.UserTags.GetById(id);
        if (userTag == null){
            return NotFound();
        }
        return _mapper.Map<UserTagDto>(userTag);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserTag>> Post(UserTagDto userTagDto){
        var userTag = _mapper.Map<UserTag>(userTagDto);
        this._unitOfWork.UserTags.Add(userTag);
        await _unitOfWork.SaveAsync();
        if (userTag == null)
        {
            return BadRequest();
        }
        userTagDto.UserCc = userTag.UserCc;
        return CreatedAtAction(nameof(Post),new {id= userTagDto.UserCc}, userTagDto);
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(string UserCc,int TagId , [FromBody]UserTagDto UserTagDto)
    {
        if(UserTagDto == null)
            return BadRequest();

        UserTag UserTag = await _unitOfWork.UserTags.GetByIdAsync(UserCc,TagId);
        _unitOfWork.UserTags.Remove(UserTag);
        await _unitOfWork.SaveAsync();
        _mapper.Map(UserTagDto, UserTag);
        
        _unitOfWork.UserTags.Add(UserTag);

        int num = await _unitOfWork.SaveAsync();

        if(num == 0)
            return BadRequest();

        return Ok("REGISTRO ACTUALIZADO CON EXITO");
    }
    [HttpDelete("{UserCc},{TagId}")]  
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(string UserCc,int TagId)
    {
        UserTag UserTag = await _unitOfWork.UserTags.GetByIdAsync(UserCc,TagId);

        if(UserTag == null)
            return BadRequest();

        _unitOfWork.UserTags.Remove(UserTag);

        int num = await _unitOfWork.SaveAsync();

        if (num == 0)
            return BadRequest();

        return NoContent();
    }
}