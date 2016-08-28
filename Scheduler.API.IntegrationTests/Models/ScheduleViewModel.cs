using System;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    /// <summary>
    /// Schedule
    /// </summary>
    public class ScheduleViewModel
    {
        /// <summary>
        /// System Identifier
        /// </summary>
        public int Id { get; set; }

        public string Description { get; set; }

        public string ScheduleCode { get; set; }

        /// <summary>
        /// Determines if Job Codes can be added to scheduled shifts and Time Card Entries for this Schedule
        /// </summary>
        public bool JobCodesApplyToShift { get; set; }

        /// <summary>
        /// Determines if Job Codes can be added to Mileage Entries in Time Card Entries for this Schedule
        /// </summary>
        public bool JobCodesApplyToTravel { get; set; }

        /// <summary>
        /// The Last Date that scheduled shifts and Time Card Entries can be added to this Schedule
        /// </summary>
        public DateTime? LastDate { get; set; }
    }

    /// <summary>
    /// Add Update model for Schedules
    /// </summary>
    public class PostScheduleViewModel
    {
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// A third party id to associate with this Schedule
        /// </summary>
        public string ScheduleCode { get; set; }

        /// <summary>
        /// Determines if Job Codes can be added to scheduled shifts and Time Card Entries for this Schedule
        /// </summary>
        public bool JobCodesApplyToShift { get; set; }

        /// <summary>
        /// Determines if Job Codes can be added to Mileage Entries in Time Card Entries for this Schedule
        /// </summary>
        public bool JobCodesApplyToTravel { get; set; }

        /// <summary>
        /// The Last Date that scheduled shifts and Time Card Entries can be added to this Schedule
        /// </summary>
        public DateTime? LastDate { get; set; }
    }
}