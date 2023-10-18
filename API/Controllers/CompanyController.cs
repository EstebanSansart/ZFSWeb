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

// Company, company, Companies, companies

public class CompanyController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CompanyController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<CompanyDto>>> Get()
    {
        var companies = await _unitOfWork.Companies.GetAll();
        return _mapper.Map<List<CompanyDto>>(companies);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CompanyDto>>> Get11([FromQuery] Params companyParams)
    {
        var company = await _unitOfWork.Companies.GetAllAsync(companyParams.PageIndex,companyParams.PageSize,companyParams.Search);
        var lstCompaniesDto = _mapper.Map<List<CompanyDto>>(company.registros);
        return new Pager<CompanyDto>(lstCompaniesDto,companyParams.Search,company.totalRegistros,companyParams.PageIndex,companyParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDto>> Get(int id)
    {
        var company = await _unitOfWork.Companies.GetById(id);
        if (company == null){
            return NotFound();
        }
        return _mapper.Map<CompanyDto>(company);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Company>> Post(CompanyDto companyDto){
        var company = _mapper.Map<Company>(companyDto);
        this._unitOfWork.Companies.Add(company);
        await _unitOfWork.SaveAsync();
        if (company == null)
        {
            return BadRequest();
        }
        companyDto.CompanyId = company.CompanyId;
        return CreatedAtAction(nameof(Post),new {id= companyDto.CompanyId}, companyDto);
    }
    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]CompanyDto CompanyDto)
    {
        if(CompanyDto == null) return BadRequest();
        Company Company =  await _unitOfWork.Companies.GetById(id);
        _mapper.Map(CompanyDto,Company);
        _unitOfWork.Companies.Update(Company);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var company = await _unitOfWork.Companies.GetById(id);
        if(company == null){
            return NotFound();
        }
        _unitOfWork.Companies.Remove(company);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}