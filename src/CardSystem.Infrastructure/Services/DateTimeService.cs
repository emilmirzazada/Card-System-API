using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardSystem.Application.Common.Interfaces;

namespace CardSystem.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        TimeZoneInfo fleTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
        public DateTime Now => TimeZoneInfo.ConvertTime(DateTime.Now, fleTimeZone);
    }
}
