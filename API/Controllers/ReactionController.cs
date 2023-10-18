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

// Reaction, reaction, Reactions, reactions

public class ReactionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReactionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<ReactionDto>>> Get()
    {
        var reactions = await _unitOfWork.Reactions.GetAll();
        return _mapper.Map<List<ReactionDto>>(reactions);
    }
    [HttpGet("Pager")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ReactionDto>>> Get11([FromQuery] Params reactionParams)
    {
        var reaction = await _unitOfWork.Reactions.GetAllAsync(reactionParams.PageIndex,reactionParams.PageSize,reactionParams.Search);
        var lstReactionsDto = _mapper.Map<List<ReactionDto>>(reaction.registros);
        return new Pager<ReactionDto>(lstReactionsDto,reactionParams.Search,reaction.totalRegistros,reactionParams.PageIndex,reactionParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReactionDto>> Get(int id)
    {
        var reaction = await _unitOfWork.Reactions.GetById(id);
        if (reaction == null){
            return NotFound();
        }
        return _mapper.Map<ReactionDto>(reaction);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Reaction>> Post(ReactionDto reactionDto){
        var reaction = _mapper.Map<Reaction>(reactionDto);
        this._unitOfWork.Reactions.Add(reaction);
        await _unitOfWork.SaveAsync();
        if (reaction == null)
        {
            return BadRequest();
        }
        reactionDto.ReactionId = reaction.ReactionId;
        return CreatedAtAction(nameof(Post),new {id= reactionDto.ReactionId}, reactionDto);
    }
    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]ReactionDto ReactionDto)
    {
        if(ReactionDto == null) return BadRequest();
        Reaction Reaction =  await _unitOfWork.Reactions.GetById(id);
        _mapper.Map(ReactionDto,Reaction);
        _unitOfWork.Reactions.Update(Reaction);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var reaction = await _unitOfWork.Reactions.GetById(id);
        if(reaction == null){
            return NotFound();
        }
        _unitOfWork.Reactions.Remove(reaction);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}