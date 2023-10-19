
using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    public class EventAttendanceController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventAttendanceController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

           public async Task<ActionResult> Add(EventoAttendaceDto EventeAttendanceDto)
        {
            if(EventeAttendanceDto == null)
                    return BadRequest();

            EventAttendance EventAttendace = _mapper.Map<EventAttendance>(EventeAttendanceDto);
            _unitOfWork.EventAttendances.Add(EventAttendace);  

            int num = await _unitOfWork.SaveAsync();

            if(num == 0)
                return BadRequest();

            return CreatedAtAction(nameof(Add), new {id = EventAttendace.UserCc,EventAttendace.EventId },EventAttendace);
        }


        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EventoAttendaceDto>> GetById(string cedula,int EventoId)
        {
            EventAttendance eventAttendance =await  _unitOfWork.EventAttendances.GetByIdAttendance(cedula,EventoId);

                if(eventAttendance == null)
                    return BadRequest();

            return _mapper.Map<EventoAttendaceDto>(eventAttendance);

        }

        
    }
