using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using RPL.Core.Interfaces;
using RPL.Core.Result;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RPL.Web.Endpoints.ProjectEndpoints
{
    public class ListIncomplete : BaseAsyncEndpoint
        .WithRequest<ListIncompleteRequest>
        .WithResponse<ListIncompleteResponse>
    {
        private readonly IToDoItemSearchService _searchService;

        public ListIncomplete(IToDoItemSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("/Projects/{ProjectId}/IncompleteItems")]
        [SwaggerOperation(
            Summary = "Gets a list of a project's incomplete items",
            Description = "Gets a list of a project's incomplete items",
            OperationId = "Project.ListIncomplete",
            Tags = new[] { "ProjectEndpoints" })
        ]
        public override async Task<ActionResult<ListIncompleteResponse>> HandleAsync([FromQuery] ListIncompleteRequest request, CancellationToken cancellationToken)
        {
            var response = new ListIncompleteResponse();
            var result = await _searchService.GetAllIncompleteItemsAsync(request.ProjectId, request.SearchString);

            if (result.Status == ResultStatus.Ok)
            {
                response.ProjectId = request.ProjectId;
                response.IncompleteItems = new List<ToDoItemRecord>(
                        result.Data.Select(
                            item => new ToDoItemRecord(item.Id,
                            item.Title,
                            item.Description,
                            item.IsDone)));
            }
            else if (result.Status == ResultStatus.BadRequest)
            {
                return BadRequest();
            }
            else if (result.Status == ResultStatus.InternalServerError)
            {
                return UnprocessableEntity();
            }

            return Ok(response);
        }
    }
}
