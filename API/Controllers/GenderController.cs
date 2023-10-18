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

// Gender, gender, Genders, genders

public class GenderController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenderController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<GenderDto>>> Get()
    {
        var genders = await _unitOfWork.Genders.GetAll();
        return _mapper.Map<List<GenderDto>>(genders);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<GenderDto>>> Get11([FromQuery] Params genderParams)
    {
        var gender = await _unitOfWork.Genders.GetAllAsync(genderParams.PageIndex,genderParams.PageSize,genderParams.Search);
        var lstGendersDto = _mapper.Map<List<GenderDto>>(gender.registros);
        return new Pager<GenderDto>(lstGendersDto,genderParams.Search,gender.totalRegistros,genderParams.PageIndex,genderParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenderDto>> Get(int id)
    {
        var gender = await _unitOfWork.Genders.GetById(id);
        if (gender == null){
            return NotFound();
        }
        return _mapper.Map<GenderDto>(gender);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Gender>> Post(GenderDto genderDto){
        var gender = _mapper.Map<Gender>(genderDto);
        this._unitOfWork.Genders.Add(gender);
        await _unitOfWork.SaveAsync();
        if (gender == null)
        {
            return BadRequest();
        }
        genderDto.GenderId = gender.GenderId;
        return CreatedAtAction(nameof(Post),new {id= genderDto.GenderId}, genderDto);
    }
    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]GenderDto GenderDto)
    {
        if(GenderDto == null) return BadRequest();
        Gender Gender =  await _unitOfWork.Genders.GetById(id);
        _mapper.Map(GenderDto,Gender);
        _unitOfWork.Genders.Update(Gender);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var gender = await _unitOfWork.Genders.GetById(id);
        if(gender == null){
            return NotFound();
        }
        _unitOfWork.Genders.Remove(gender);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}