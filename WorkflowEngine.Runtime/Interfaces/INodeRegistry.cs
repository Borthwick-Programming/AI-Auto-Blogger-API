using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowEngine.Domain.Models;

namespace WorkflowEngine.Runtime.Interfaces
{
    /// <summary>
    /// Defines the contract for a registry that stores available node definitions.
    /// This interface allows consumers (e.g., the API or execution engine)
    /// to query and register nodes without knowing the storage mechanism.
    /// </summary>
    public interface INodeRegistry
    {
        /// <summary>
        /// Returns all currently registered node definitions.
        /// </summary>
        IEnumerable<NodeDefinition> GetAll();

        // <<CRUD>> ----------------------------------------------------------------

        /// <summary>
        /// read-one
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        NodeDefinition? Get(string id);

        /// <summary>
        /// Registers a new node definition into the registry.
        /// </summary>
        /// <param name="definition">The node to add</param>
        void Register(NodeDefinition definition);

        /// <summary>
        /// Updates the specified node definition identified by the given ID.
        /// </summary>
        /// <remarks>This method modifies the existing node definition associated with the specified ID.
        /// Ensure that the ID corresponds to an existing node and that the definition contains valid data.</remarks>
        /// <param name="id">The unique identifier of the node to update. Cannot be null or empty.</param>
        /// <param name="definition">The new definition of the node, containing updated properties and values. Cannot be null.</param>
        void Update(string id, NodeDefinition definition); // update

        /// <summary>
        /// Removes the item with the specified identifier from the collection.
        /// </summary>
        /// <remarks>If the item with the specified identifier does not exist in the collection, no action
        /// is taken.</remarks>
        /// <param name="id">The unique identifier of the item to remove. Cannot be null or empty.</param>
        void Remove(string id);  // delete
    }
}
