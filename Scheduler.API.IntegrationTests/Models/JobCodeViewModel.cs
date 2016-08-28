using System;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    public class JobCodeViewModel
    {
        /// <summary>
        /// The system identifier 
        /// </summary>
        public int Id { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// An Alternate third party code for this job code
        /// </summary>
        public string AltCode { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// The last date that this Job Code can be assigned to a Schedule of Department
        /// </summary>
        public DateTime? LastDate { get; set; }
    }

    public class PostJobCodeViewModel
    {
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// An Alternate third party code for this job code
        /// </summary>
        public string AltCode { get; set; }

        [Required]
        public string Description { get; set; }

        /// <summary>
        /// The last date that this Job Code can be assigned to a Schedule of Department
        /// </summary>
        public DateTime? LastDate { get; set; }
    }
}