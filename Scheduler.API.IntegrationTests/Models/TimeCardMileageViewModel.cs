using System.Collections.Generic;

namespace Scheduler.API.IntegrationTests.Models {
    public class TimeCardMileageViewModel {

        public virtual IEnumerable<string> JobCodes { get; set; }
        public virtual double Miles { get; set; }
        public virtual string Notes { get; set; }
    }
}