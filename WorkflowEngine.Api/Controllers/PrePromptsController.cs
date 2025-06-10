using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkflowEngine.Core.Interfaces;
using WorkflowEngine.Core.Models.PrePrompt;

namespace WorkflowEngine.Api.Controllers
{
    [Route("api/preprompts")]
    [ApiController]
    public class PrePromptsController : ControllerBase
    {
        private readonly IPrePromptService _svc;
        public PrePromptsController(IPrePromptService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> List() =>
          Ok(await _svc.ListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreatePrePromptRequest req)
        {
            var dto = await _svc.CreateAsync(req);
            return CreatedAtAction(null, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePrePromptRequest req)
        {
            if (id != req.Id) return BadRequest();
            return await _svc.UpdateAsync(req)
              ? Ok(req)
              : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
          await _svc.DeleteAsync(id)
            ? NoContent()
            : NotFound();
    }
}
