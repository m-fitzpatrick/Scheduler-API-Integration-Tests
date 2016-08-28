using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    public class TimeCardDetailViewModel
    {
        public string Schedule { get; set; }

        public string EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public IEnumerable<string> JobCodes { get; set; }

        public string Task { get; set; }

        public string Notes { get; set; }

        public IEnumerable<TimeCardMileageViewModel> Trips { get; set; }
    }

    public class GetTimeCardDetailViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The Employee's sytem identifier
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// The Department's system identifier
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// The Schedule's system identifier
        /// </summary>
        public int? SiteId { get; set; }
    }
}