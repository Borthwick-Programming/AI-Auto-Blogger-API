using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowEngine.Infrastructure.Entities.Links
{
    /// <summary>
    /// Represents the configuration for affiliate link input processing.
    /// </summary>
    /// <remarks>This class defines the settings and data required for processing affiliate link inputs. The
    /// configuration can operate in different modes, such as "inline" or "csv", and may include a collection of input
    /// entries and an optional CSV file path.</remarks>
    public class AffiliateLinkInputConfig
    {
        [Key]
        [ForeignKey(nameof(NodeInstance))]
        public Guid NodeInstanceId { get; set; }

        public string Mode { get; set; } = "inline";    // "inline" or "csv"
        public string? CsvPath { get; set; }

        public NodeInstance NodeInstance { get; set; } = default!;

        public ICollection<AffiliateLinkInputEntry> Entries
        { get; set; } = new List<AffiliateLinkInputEntry>();
    }
}
