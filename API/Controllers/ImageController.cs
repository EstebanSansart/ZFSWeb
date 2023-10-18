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

// Image, image, Images, images

public class ImageController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ImageController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrador")]
    //[MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<ImageDto>>> Get()
    {
        var images = await _unitOfWork.Images.GetAll();
        return _mapper.Map<List<ImageDto>>(images);
    }
    [HttpGet("Pager")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ImageDto>>> Get11([FromQuery] Params imageParams)
    {
        var image = await _unitOfWork.Images.GetAllAsync(imageParams.PageIndex,imageParams.PageSize,imageParams.Search);
        var lstImagesDto = _mapper.Map<List<ImageDto>>(image.registros);
        return new Pager<ImageDto>(lstImagesDto,imageParams.Search,image.totalRegistros,imageParams.PageIndex,imageParams.PageSize);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ImageDto>> Get(int id)
    {
        var image = await _unitOfWork.Images.GetById(id);
        if (image == null){
            return NotFound();
        }
        return _mapper.Map<ImageDto>(image);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Image>> Post(ImageDto imageDto){
        var image = _mapper.Map<Image>(imageDto);
        this._unitOfWork.Images.Add(image);
        await _unitOfWork.SaveAsync();
        if (image == null)
        {
            return BadRequest();
        }
        imageDto.ImageId = image.ImageId;
        return CreatedAtAction(nameof(Post),new {id= imageDto.ImageId}, imageDto);
    }
    [HttpPut]
   // [Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Update(int id, [FromBody]ImageDto ImageDto)
    {
        if(ImageDto == null) return BadRequest();
        Image Image =  await _unitOfWork.Images.GetById(id);
        _mapper.Map(ImageDto,Image);
        _unitOfWork.Images.Update(Image);
        int numeroCambios = await _unitOfWork.SaveAsync();
        if(numeroCambios == 0 ) return BadRequest();
        return Ok("Registro actualizado con exito");
    } 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var image = await _unitOfWork.Images.GetById(id);
        if(image == null){
            return NotFound();
        }
        _unitOfWork.Images.Remove(image);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}