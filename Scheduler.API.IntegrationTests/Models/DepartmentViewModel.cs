using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    /// <summary>
    /// Department View Model
    /// </summary>
    public class DepartmentViewModel
    {
        /// <summary>
        /// The System Identifier 
        /// </summary>
        public int Id { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// Require membership for claiming shifts in this department
        /// </summary>
        public bool RequireMembership { get; set; }

        /// <summary>
        /// Determines if an employee must be a member of this Department to be scheduled on it's Schedules
        /// </summary>
        private SchedulingRule DeptRule { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? ModifieDate { get; set; }
    }

    /// <summary>
    /// Retrieve Department List View Model
    /// </summary>
    public class GetDepartmentViewModel
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public GetDepartmentViewModel()
        {
            MembershipSchedulingRules = Enum.GetValues(typeof(SchedulingRule))
                                            .Cast<SchedulingRule>()
                                            .ToDictionary(x => (int) x, x => x.GetDescription());
        }

        /// <summary>
        /// The List of Departments
        /// </summary>
        public IEnumerable<DepartmentViewModel> Departments { get; set; }

        /// <summary>
        /// The rules determining if an employee must be a member of this Department to be scheduled on it's Schedules
        /// </summary>
        public Dictionary<int, string> MembershipSchedulingRules { get; private set; }
    }

    /// <summary>
    /// Add and update Department Model
    /// </summary>
    public class PostDepartmentViewModel
    {
        [Required]
        public string Description { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// Determines if Department Membership is Required to claim shifts in this Department
        /// </summary>
        [Required]
        public bool RequireMembership { get; set; }

        /// <summary>
        /// Determines if an employee must be a member of this Department to be scheduled on it's Schedules
        /// Membership Not Required = 0, Notify When not a member = 1, Membership Required = 2
        /// </summary>
        [Required]
        public SchedulingRule DeptRule { get; set; }
    }

    /// <summary>
    /// Add Department with Default Shedule
    /// </summary>
    public class PostAddDepartmentViewModel : PostDepartmentViewModel
    {
        /// <summary>
        /// The Schedule to add with the new Department
        /// </summary>
        [Required]
        public PostScheduleViewModel Schedule { get; set; }
    }
}