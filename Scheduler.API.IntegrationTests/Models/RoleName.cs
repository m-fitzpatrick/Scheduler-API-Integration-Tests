using System.ComponentModel;

namespace Scheduler.API.IntegrationTests.Models
{
    public enum RoleName
    {
        [Description("Administrator")]
        Administrator = 0,
        [Description("Supervisor")]
        Supervisor = 1,
        [Description("Schedule Controller")]
        Schedule_Controller = 2,
        [Description("Scheduler")]
        Scheduler = 3,
        [Description("Schedulee")]
        Schedulee = 4
    }
}
