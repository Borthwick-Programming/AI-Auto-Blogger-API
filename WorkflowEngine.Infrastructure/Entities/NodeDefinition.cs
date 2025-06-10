// WorkflowEngine.Infrastructure/Entities/NodeDefinition.cs
using System.ComponentModel.DataAnnotations;

namespace WorkflowEngine.Infrastructure.Entities;

/// <summary>
/// Blueprint that tells the engine how a node behaves and how the designer
/// should render it.  Seeded from Configuration/NodeDefinitions/*.json,
/// but can also be created at runtime via POST /api/nodes.
/// </summary>
public class NodeDefinition
{
    [Key]
    [MaxLength(64)]
    public string Id { get; set; } = default!;          // "affiliate-link-input"

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = default!;        // "Affiliate Link Input"

    [MaxLength(512)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(64)]
    public string NodeType { get; set; } = "source";    // "source", "action", etc.

    /// <summary>JSON schema or example that the UI can use for validation.</summary>
    public string? ConfigurationSchemaJson { get; set; }

    /// <summary>Ports expressed as JSON array so we stay 1-table simple.</summary>
    public string InputsJson { get; set; } = "[]";     // e.g. '[{ "name":"in", "type":"Link"}]'
    public string OutputsJson { get; set; } = "[]";     // e.g. '[{ "name":"out","type":"Link"}]'

    [MaxLength(64)]
    public string? Icon { get; set; }                   // "RiLinksLine"

    [MaxLength(128)]
    public string? ReactComponent { get; set; }         // "AffiliateLinkInputNode"
}
