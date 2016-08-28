using System;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    public class JobCodeSpanViewModel
    {
        public int Id { get; set; }

        public JobCodeViewModel JobCodeModel { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? PreviousEndDate { get; set; }
        
    }

    public class PostJobCodeSpanViewModel
    {
        public int JobCodeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}