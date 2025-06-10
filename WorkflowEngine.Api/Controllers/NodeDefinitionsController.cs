using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkflowEngine.Domain.Models;
using WorkflowEngine.Runtime.Interfaces;

namespace WorkflowEngine.Api.Controllers
{
    /// <summary>
    /// API Controller for exposing available node definitions.
    /// Provides a GET endpoint at /api/nodes to retrieve all registered nodes.
    /// </summary>
    [Route("api/nodes")]
    [ApiController]
    public class NodeDefinitionsController : ControllerBase
    {
        private readonly INodeRegistry _registry;

        /// <summary>
        /// Injects the node registry (in-memory or future extensible implementation).
        /// </summary>
        /// <param name="registry">The node registry instance that holds all node definitions.</param>
        public NodeDefinitionsController(INodeRegistry registry)
        {
            _registry = registry;
        }

        /// <summary>
        /// GET api/nodes
        /// Returns all registered node definitions.
        /// </summary>
        /// <returns>HTTP 200 OK with a JSON array of node definitions.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var nodes = _registry.GetAll();
            return Ok(nodes);
        }

        /// <summary>
        /// Retrieves an item by its unique identifier.
        /// </summary>
        /// <remarks>This method returns an HTTP 200 response with the item if it exists, or an HTTP 404
        /// response if the item is not found.</remarks>
        /// <param name="id">The unique identifier of the item to retrieve. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> containing the requested item if found, or an appropriate HTTP response
        /// indicating the result of the operation.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        { 
         return Ok(_registry.Get(id));
        }

        /// <summary>
        /// Creates a new node definition and registers it in the system.
        /// </summary>
        /// <param name="dto">The node definition to be created and registered. Must not be null.</param>
        /// <returns>An <see cref="IActionResult"/> containing the HTTP 201 Created response,  including the location of the
        /// newly created resource and its details.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] NodeDefinition dto)
        {
            _registry.Register(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Updates the specified node definition in the registry.
        /// </summary>
        /// <remarks>This method performs an update operation on an existing node in the registry. Ensure
        /// that the <paramref name="id"/> corresponds to a valid node and that the <paramref name="dto"/> contains the
        /// necessary data for the update.</remarks>
        /// <param name="id">The unique identifier of the node to update. Cannot be null or empty.</param>
        /// <param name="dto">The updated node definition to apply. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns <see langword="404"/> if the
        /// node with the specified <paramref name="id"/> is not found. Returns <see langword="204"/> if the update is
        /// successful.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] NodeDefinition dto)
        {
            if (_registry.Get(id) is null) return NotFound();
            _registry.Update(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Deletes the resource identified by the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the resource to delete. Cannot be null or empty.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see cref="NotFoundResult"/>
        /// if the resource does not exist, or <see cref="NoContentResult"/> if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (_registry.Get(id) is null) return NotFound();
            _registry.Remove(id);
            return NoContent();
        }
    }
}
