using API.Dtos;

namespace Api.Dtos;
public class  EventoImageDto
{
   public int EventId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public bool State { get; set; }
    public int EventPoints { get; set; }
    public DateTime Date { get; set; }
    public string Sponsorship { get; set; }
    public List<ImageDto> Images { get; set; }

}
