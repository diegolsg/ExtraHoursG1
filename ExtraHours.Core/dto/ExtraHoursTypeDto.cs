namespace ExtraHours.Core.dto
{
    public class ExtraHourTypeDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required decimal RateMultiplier { get; set; }
    }
}