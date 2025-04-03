namespace ExtraHours.Core.dto
{
    public class ExtraHourTypeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int QuantityHour { get; set; }
        // public required decimal RateMultiplier { get; set;}

        public ExtraHourTypeDto() { }
        public ExtraHourTypeDto(int id, string name, int quantityHour)
        {
            Id = id;
            Name = name;
            QuantityHour = quantityHour;
        }
    }
}
