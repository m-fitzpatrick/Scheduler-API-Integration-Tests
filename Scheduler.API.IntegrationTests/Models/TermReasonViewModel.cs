using System.ComponentModel.DataAnnotations;

namespace Scheduler.API.IntegrationTests.Models
{
    public class TermReasonViewModel
    {
        /// <summary>
        ///     The system identifier of the Term Reason
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The Description of the Term Reason
        /// </summary>
        public string Description { get; set; }
    }

    public class PostTermReasonViewModel
    {
        /// <summary>
        ///     The Description of the Term Reason
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}