using FUNFOX.NET5.Application.Interfaces.Services;
using System;

namespace FUNFOX.NET5.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}