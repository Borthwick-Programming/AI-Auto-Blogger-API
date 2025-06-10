using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowEngine.Core.Models.PrePrompt
{
    public record UpdatePrePromptRequest(int Id, string Name, string PromptText);
}
