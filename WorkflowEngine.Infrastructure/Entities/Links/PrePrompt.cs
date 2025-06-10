using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowEngine.Infrastructure.Entities.Links
{
    /// <summary>
    /// Represents a predefined prompt with associated metadata.
    /// </summary>
    /// <remarks>This class is typically used to store and manage prompts AffiliateLinkInputEntries can use
    /// predefined text inputs. Each instance includes a unique identifier, a name, the prompt text, and the creation
    /// timestamp.</remarks>
    public class PrePrompt
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; } = default!;

        [Required]
        public string PromptText { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
