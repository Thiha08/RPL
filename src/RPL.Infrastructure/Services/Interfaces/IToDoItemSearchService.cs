using RPL.Core.Entities;
using RPL.Core.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPL.Infrastructure.Services.Interfaces
{
    public interface IToDoItemSearchService
    {
        Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
        Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
    }
}
