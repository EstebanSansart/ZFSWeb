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

// Level, level, Levels, levels

public class LevelController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LevelController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<LevelDto>>> Get()
    {
        var levels = await _unitOfWork.Levels.GetAll();
        return _mapper.Map<List<LevelDto>>(levels);
    }
    [HttpGet("Pager")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<LevelDto>>> Get11([FromQuery] Params levelParams)
    {
        var level = await _unitOfWork.Levels.GetAllAsync(levelParams.PageIndex,levelParams.PageSize,levelParams.Search);
        var lstLevelsDto = _mapper.Map<List<LevelDto>>(level.registros);
        return new Pager<LevelDto>(lstLevelsDto,levelParams.Search,level.totalRegistros,levelParams.PageIndex,levelParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LevelDto>> Get(int id)
    {
        var level = await _unitOfWork.Levels.GetById(id);
        if (level == null){
            return NotFound();
        }
        return _mapper.Map<LevelDto>(level);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Level>> Post(LevelDto levelDto){
        var level = _mapper.Map<Level>(levelDto);
        this._unitOfWork.Levels.Add(level);
        await _unitOfWork.SaveAsync();
        if (level == null)
        {
            return BadRequest();
        }
        levelDto.LevelId = level.LevelId;
        return CreatedAtAction(nameof(Post),new {id= levelDto.LevelId}, levelDto);
    }
    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]LevelDto LevelDto)
    {
        if(LevelDto == null) return BadRequest();
        Level Level =  await _unitOfWork.Levels.GetById(id);
        _mapper.Map(LevelDto,Level);
        _unitOfWork.Levels.Update(Level);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var level = await _unitOfWork.Levels.GetById(id);
        if(level == null){
            return NotFound();
        }
        _unitOfWork.Levels.Remove(level);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}