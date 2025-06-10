// WorkflowEngine.Runtime/Services/InMemoryNodeRegistry.cs
using System.Collections.Concurrent;
using WorkflowEngine.Domain.Models;
using WorkflowEngine.Runtime.Interfaces;

namespace WorkflowEngine.Runtime.Services
{
    /// <summary>
    /// In-memory registry backed by a thread-safe dictionary.
    /// </summary>
    public class InMemoryNodeRegistry : INodeRegistry
    {
        private readonly ConcurrentDictionary<string, NodeDefinition> _nodes = new();

        // <<Get>>
        public NodeDefinition? Get(string id) =>
            _nodes.TryGetValue(id, out var def) ? def : null;

        // <<GetAll>>
        public IEnumerable<NodeDefinition> GetAll() => _nodes.Values;

        // <<Register>>
        public void Register(NodeDefinition definition) =>
            _nodes.TryAdd(definition.Id, definition);

        // <<Update>>
        public void Update(string id, NodeDefinition definition) =>
            _nodes[id] = definition;

        // <<Remove>>
        public void Remove(string id) =>
            _nodes.TryRemove(id, out _);
    }
}
