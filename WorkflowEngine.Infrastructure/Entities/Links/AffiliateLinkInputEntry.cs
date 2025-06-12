using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowEngine.Infrastructure.Entities.Links
{
    /// <summary>
    /// Represents an entry for an affiliate link input, including metadata such as name, URL, and optional
    /// descriptions.
    /// </summary>
    /// <remarks>This class is used to define the structure of an affiliate link input entry, which includes
    /// required fields such as  the name and URL, as well as optional fields like a description and pre-prompt name.
    /// Each entry is associated with  a specific configuration via the <see cref="NodeInstanceId"/> property.</remarks>
    public class AffiliateLinkInputEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid NodeInstanceId { get; set; }

        [ForeignKey(nameof(NodeInstanceId))]
        public AffiliateLinkInputConfig Config { get; set; } = default!;

        [Required, MaxLength(128)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(512)]
        public string Url { get; set; } = default!;

        [MaxLength(1024)]
        public string? Description { get; set; }

        [MaxLength(128)]
        public string? PrePromptName { get; set; }

        public ICollection<LinkProcessingHistory> History { get; set; }
  = new List<LinkProcessingHistory>();

        public AffiliateLinkEntrySchedule? Schedule { get; set; }

    }
}
