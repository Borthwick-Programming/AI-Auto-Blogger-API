using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowEngine.Core.Models.PrePrompt;

namespace WorkflowEngine.Core.Interfaces
{
    public interface IPrePromptService
    {
        Task<IEnumerable<PrePromptDto>> ListAsync();
        Task<PrePromptDto> CreateAsync(CreatePrePromptRequest req);
        Task<bool> UpdateAsync(UpdatePrePromptRequest req);
        Task<bool> DeleteAsync(int id);
    }
}
