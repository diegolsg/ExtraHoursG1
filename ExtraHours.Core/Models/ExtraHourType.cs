public class ExtraHourType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal RateMultiplier { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
