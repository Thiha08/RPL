using Microsoft.AspNetCore.Mvc;
using RPL.Core;
using RPL.Core.ProjectAggregate;
using RPL.Core.ProjectAggregate.Specifications;
using RPL.SharedKernel.Interfaces;
using RPL.Web.ApiModels;
using RPL.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RPL.Web.Controllers
{
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly IRepository<Project> _projectRepository;

        public ProjectController(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET project/{projectId?}
        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> Index(int projectId = 1)
        {
            var spec = new ProjectByIdWithItemsSpec(projectId);
            var project = await _projectRepository.GetBySpecAsync(spec);

            var dto = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Items = project.Items
                            .Select(item => ToDoItemViewModel.FromToDoItem(item))
                            .ToList()
            };
            return View(dto);
        }
    }
}
