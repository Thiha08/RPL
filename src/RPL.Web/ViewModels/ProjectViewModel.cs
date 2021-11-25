using System.Collections.Generic;

namespace RPL.Web.ViewModels
{
    public class ProjectViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ToDoItemViewModel> Items = new();
    }
}
