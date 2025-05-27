namespace ExtraHours.Core.dto
{
    public class RegisterHourDto
    {
        public DateOnly DateInput { get; set; }
        public required string StarHour { get; set; }
        public required string EndHour { get; set; }
        public required string Code { get; set; }

        public RegisterHourDto(DateOnly dateInput, string starHour, string endHour, string code)
        {
            DateInput = dateInput;
            StarHour = starHour;
            EndHour = endHour;
            Code = code;
        }
        public RegisterHourDto()
        {
        }

    }
}
