using System.ComponentModel;

namespace Scheduler.API.IntegrationTests.Models
{
    public enum StatusType
    {
        [Description("Full Time")]
        FullTime = 0,

        [Description("Part Time")]
        PartTime = 1
    }
}
