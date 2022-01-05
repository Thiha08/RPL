using Ardalis.Specification;
using RPL.Core.Entities;

namespace RPL.Core.Specifications
{
    public class IncompleteItemsSearchSpec : Specification<ToDoItem>
    {
        public IncompleteItemsSearchSpec(string searchString)
        {
            Query
                .Where(item => !item.IsDone &&
                (item.Title.Contains(searchString) ||
                item.Description.Contains(searchString)));
        }
    }
}
