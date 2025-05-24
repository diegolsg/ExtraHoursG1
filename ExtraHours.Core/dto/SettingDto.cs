using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraHours.Core.dto
{
    public class SettingDto
    {
        public SettingDto() { }
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
        public TimeSpan endHourDay { get; set; }
        public TimeSpan startHourDay { get; set; }
        public int ClosingDay { get; set; }

        public SettingDto(int id, string key, string value, TimeSpan endHourDay, TimeSpan startHourDay, int closingDay)
        {
            Id = id;
            Key = key;
            Value = value;
            this.endHourDay = endHourDay;
            this.startHourDay = startHourDay;
            ClosingDay = closingDay;
        }
    }
}
