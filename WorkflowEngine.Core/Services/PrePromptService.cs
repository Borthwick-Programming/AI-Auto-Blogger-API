using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowEngine.Core.Interfaces;
using WorkflowEngine.Core.Models.PrePrompt;
using WorkflowEngine.Infrastructure.Data;
using WorkflowEngine.Infrastructure.Entities.Links;

namespace WorkflowEngine.Core.Services
{
    public class PrePromptService : IPrePromptService
    {
        private readonly WorkflowEngineDbContext _db;

        public PrePromptService(WorkflowEngineDbContext db) => _db = db;

        public async Task<PrePromptDto> CreateAsync(CreatePrePromptRequest req)
        {
            var e = new PrePrompt { Name = req.Name, PromptText = req.PromptText };
            _db.PrePrompts.Add(e);
            await _db.SaveChangesAsync();
            return new PrePromptDto(e.Id, e.Name, e.PromptText);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _db.PrePrompts.FindAsync(id);
            if (e is null) return false;
            _db.PrePrompts.Remove(e);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PrePromptDto>> ListAsync() =>
            await _db.PrePrompts
            .Select(p => new PrePromptDto(p.Id, p.Name, p.PromptText))
            .ToListAsync();

        public async Task<bool> UpdateAsync(UpdatePrePromptRequest req)
        {
            var e = await _db.PrePrompts.FindAsync(req.Id);
            if (e is null) return false;
            e.Name = req.Name;
            e.PromptText = req.PromptText;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
