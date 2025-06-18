namespace ExtraHours.Core.Models
{
    public class Report
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public decimal Salary { get; set; }
        public decimal TotalExtraValue { get; set; }
        public decimal TotalSalaryWithExtras => Salary + TotalExtraValue;
    }
}
