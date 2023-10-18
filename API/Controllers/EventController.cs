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

// Event, eventt, Events, events

public class EventController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EventController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<EventDto>>> Get()
    {
        var events = await _unitOfWork.Events.GetAll();
        return _mapper.Map<List<EventDto>>(events);
    }
    [HttpGet("Pager")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EventDto>>> Get11([FromQuery] Params eventtParams)
    {
        var eventt = await _unitOfWork.Events.GetAllAsync(eventtParams.PageIndex,eventtParams.PageSize,eventtParams.Search);
        var lstEventsDto = _mapper.Map<List<EventDto>>(eventt.registros);
        return new Pager<EventDto>(lstEventsDto,eventtParams.Search,eventt.totalRegistros,eventtParams.PageIndex,eventtParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> Get(int id)
    {
        var eventt = await _unitOfWork.Events.GetById(id);
        if (eventt == null){
            return NotFound();
        }
        return _mapper.Map<EventDto>(eventt);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Event>> Post(EventDto eventtDto){
        var eventt = _mapper.Map<Event>(eventtDto);
        this._unitOfWork.Events.Add(eventt);
        await _unitOfWork.SaveAsync();
        if (eventt == null)
        {
            return BadRequest();
        }
        eventtDto.EventId = eventt.EventId;
        return CreatedAtAction(nameof(Post),new {id= eventtDto.EventId}, eventtDto);
    }
    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]EventDto EventDto)
    {
        if(EventDto == null) return BadRequest();
        Event Event =  await _unitOfWork.Events.GetById(id);
        _mapper.Map(EventDto,Event);
        _unitOfWork.Events.Update(Event);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var eventt = await _unitOfWork.Events.GetById(id);
        if(eventt == null){
            return NotFound();
        }
        _unitOfWork.Events.Remove(eventt);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}