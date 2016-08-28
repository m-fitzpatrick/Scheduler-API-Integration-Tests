using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    public class TaskViewModel
    {
        /// <summary>
        ///     The system identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The Tas's Description
        /// </summary>
        public string Description { get; set; }
    }

    public class PostTaskViewModel
    {
        /// <summary>
        ///     The Tas's Description
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}