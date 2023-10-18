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

// Tag, tag, Tags, tags

public class TagController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TagController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<TagDto>>> Get()
    {
        var tags = await _unitOfWork.Tags.GetAll();
        return _mapper.Map<List<TagDto>>(tags);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TagDto>>> Get11([FromQuery] Params tagParams)
    {
        var tag = await _unitOfWork.Tags.GetAllAsync(tagParams.PageIndex,tagParams.PageSize,tagParams.Search);
        var lstTagsDto = _mapper.Map<List<TagDto>>(tag.registros);
        return new Pager<TagDto>(lstTagsDto,tagParams.Search,tag.totalRegistros,tagParams.PageIndex,tagParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagDto>> Get(int id)
    {
        var tag = await _unitOfWork.Tags.GetById(id);
        if (tag == null){
            return NotFound();
        }
        return _mapper.Map<TagDto>(tag);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Tag>> Post(TagDto tagDto){
        var tag = _mapper.Map<Tag>(tagDto);
        this._unitOfWork.Tags.Add(tag);
        await _unitOfWork.SaveAsync();
        if (tag == null)
        {
            return BadRequest();
        }
        tagDto.TagId = tag.TagId;
        return CreatedAtAction(nameof(Post),new {id= tagDto.TagId}, tagDto);
    }
    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]TagDto TagDto)
    {
        if(TagDto == null) return BadRequest();
        Tag Tag =  await _unitOfWork.Tags.GetById(id);
        _mapper.Map(TagDto,Tag);
        _unitOfWork.Tags.Update(Tag);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var tag = await _unitOfWork.Tags.GetById(id);
        if(tag == null){
            return NotFound();
        }
        _unitOfWork.Tags.Remove(tag);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}