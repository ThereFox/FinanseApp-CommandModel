namespace Infrastructure.TransactionalOutbox.DTOs;

public class ChangeEventDTO
{
    public Guid Id { get; set; }
    
    public string TargetTopic { get; set; }
    
    public DateTime CreateTime { get; set; }
    public DateTime? PublishTime { get; set; }
    public string Message { get; set; }
}