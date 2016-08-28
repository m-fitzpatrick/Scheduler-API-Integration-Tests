using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.API.IntegrationTests.Models
{
    public class StatusViewModel
    {
        /// <summary>
        /// The system identifier
        /// </summary>
        public int Id { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// The type of the status FullTime = 0, PartTime = 1
        /// </summary>
        public StatusType? Type { get; set; }

        public bool IsBenefitEligible { get; set; }

        public bool IsHolidayEligible { get; set; }
    }

    public class GetStatusViewModel
    {
        public GetStatusViewModel()
        {
            Types = Enum.GetValues(typeof(StatusType))
                        .Cast<StatusType>()
                        .ToDictionary(x => (int) x, x => x.GetDescription());
        }

        public IEnumerable<StatusViewModel> Statuses { get; set; }

        public Dictionary<int, string> Types { get; private set; }
    }

    public class PostStatusViewModel
    {
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// The type of the status FullTime = 0, PartTime = 1
        /// </summary>
        public StatusType? Type { get; set; }

        public bool IsBenefitEligible { get; set; }

        public bool IsHolidayEligible { get; set; }
    }
}