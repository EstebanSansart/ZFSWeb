namespace Domain.Entities;
public class Image
{
    public int ImageId { get; set; }
    public string Url { get; set; }
    public int EventoId { get; set; }
    public Event Event { get; set; }
}