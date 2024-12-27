using DijkstrasAlgorithmApi.Models;
using DijkstrasAlgorithmApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DijkstrasAlgorithmApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortestPathController : ControllerBase
    {
        private readonly PathFinderService _pathFinderService;

        public ShortestPathController(PathFinderService pathFinderService)
        {
            _pathFinderService = pathFinderService;
        }

        [HttpPost]
        public ActionResult<ShortestPathData> GetShortestPath([FromBody] PathRequest request)
        {
            try
            {
                var result = _pathFinderService.ShortestPath(request.FromNode, request.ToNode);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
