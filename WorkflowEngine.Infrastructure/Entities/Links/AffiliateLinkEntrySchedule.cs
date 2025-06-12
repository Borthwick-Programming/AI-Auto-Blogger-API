using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowEngine.Infrastructure.Entities.Links
{
    public class AffiliateLinkEntrySchedule
    {
        [Key, ForeignKey(nameof(Entry))]
        public int EntryId { get; set; }

        [Required, MaxLength(16)]
        public string Frequency { get; set; } = "Daily";

        public AffiliateLinkInputEntry Entry { get; set; } = default!;
    }
}
