using RPL.Core.Entities;
using RPL.Core.Result;
using RPL.Core.Specifications;
using RPL.Infrastructure.Services.Interfaces;
using RPL.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services
{
    public class ToDoItemSearchService : IToDoItemSearchService
    {
        private readonly IRepository<Project> _repository;

        public ToDoItemSearchService(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return Result<List<ToDoItem>>.BadRequest($"{nameof(searchString)} is required.");
            }

            var projectSpec = new ProjectByIdWithItemsSpec(projectId);
            var project = await _repository.GetBySpecAsync(projectSpec);

            // TODO: Optionally use Ardalis.GuardClauses Guard.Against.NotFound and catch
            if (project == null) return Result<List<ToDoItem>>.BadRequest();

            var incompleteSpec = new IncompleteItemsSearchSpec(searchString);

            try
            {
                var items = incompleteSpec.Evaluate(project.Items).ToList();

                return Result<List<ToDoItem>>.Ok(items);
            }
            catch (Exception ex)
            {
                // TODO: Log details here
                return Result<List<ToDoItem>>.BadRequest(ex.Message);
            }
        }

        public async Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId)
        {
            var projectSpec = new ProjectByIdWithItemsSpec(projectId);
            var project = await _repository.GetBySpecAsync(projectSpec);

            var incompleteSpec = new IncompleteItemsSpec();

            var items = incompleteSpec.Evaluate(project.Items).ToList();

            if (!items.Any())
            {
                return Result<ToDoItem>.BadRequest();
            }

            return Result<ToDoItem>.Ok(items.First());
        }
    }
}
