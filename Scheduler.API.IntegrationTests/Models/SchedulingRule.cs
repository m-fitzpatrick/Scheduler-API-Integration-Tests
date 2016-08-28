using System.ComponentModel;

namespace Scheduler.API.IntegrationTests.Models
{
    public enum SchedulingRule
    {
        [Description("NotRequired")]
        MembershipNotRequired = 0,

        [Description("Notify")]
        MembershipEdit = 1,

        [Description("Required")]
        MembershipRequired = 2
    }
}