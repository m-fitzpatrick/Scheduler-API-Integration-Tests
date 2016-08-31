using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    /// <summary>
    /// Address Model
    /// </summary>
    public class AddressViewModel
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }

    /// <summary>
    /// Retrieve Employee Model
    /// </summary>
    public class GetEmployeeViewModel
    {
        /// <summary>
        /// System Identifier
        /// </summary>
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AddressViewModel Address { get; set; }

        public string CellPhone { get; set; }

        public string HomePhone { get; set; }

        [Required]
        public string Username { get; set; }

        public DateTime? HireDate { get; set; }

        /// <summary>
        /// A third party identifier for this Employee
        /// </summary>
        public string EmployeeId { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public Guid? SupervisorId { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }

    /// <summary>
    /// Add Update model for Employees
    /// </summary>
    public class PostEmployeeViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AddressViewModel Address { get; set; }

        public string CellPhone { get; set; }

        public string HomePhone { get; set; }
        
        public DateTime HireDate { get; set; }

        /// <summary>
        ///  A third party identifier for this Employee
        /// </summary>
        public string EmployeeId { get; set; }
    }

    /// <summary>
    /// Add and Update model for Employees
    /// </summary>
    public class PostAddEmployeeViewModel : PostEmployeeViewModel
    {
        /// <summary>
        /// The Role for this employee
        /// Administrator = 0,Supervisor = 1,Schedule_Controller = 2,Scheduler = 3,Schedulee = 4
        /// </summary>
        public RoleName Role { get; set; }

        /// <summary>
        /// FullTime = 0,PartTime = 1
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Determines if this Employee can be assigned as other employee's supervisor
        /// </summary>
        public bool IsSupervisor { get; set; }

        /// <summary>
        /// Determines if this employee can be assigned to Schedules as a Scheduler
        /// </summary>
        public bool IsScheduler { get; set; }

        /// <summary>
        /// The system identifier of the Title for this Employee
        /// </summary>
        public int? TitleId { get; set; }

        /// <summary>
        /// The system identifier of the Employee that is this Employee's Supervisor
        /// </summary>
        public Guid? SupervisorId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}