namespace Scheduler.API.IntegrationTests.Models
{
    public class TitleViewModel
    {
        /// <summary>
        ///     The system identifier
        /// </summary>
        public int Id { get; set; }

        public string Description { get; set; }

        /// <summary>
        ///     Indicates that this title is exempt from hour and wage requirements
        /// </summary>
        public bool IsExempt { get; set; }
    }
}