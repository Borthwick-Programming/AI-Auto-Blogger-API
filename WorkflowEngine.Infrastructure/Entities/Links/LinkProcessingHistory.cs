using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowEngine.Infrastructure.Entities.Links
{
    public class LinkProcessingHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EntryId { get; set; }    // FK → AffiliateLinkInputEntry.Id

        [Required]
        public DateTime ProcessedAt { get; set; }

        public AffiliateLinkInputEntry Entry { get; set; } = default!;
    }
}
