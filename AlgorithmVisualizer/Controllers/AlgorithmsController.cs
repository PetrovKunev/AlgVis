using Microsoft.AspNetCore.Mvc;
using AlgorithmVisualizer.Models;
using AlgorithmVisualizer.Services;

namespace AlgorithmVisualizer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlgorithmsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Algorithm>> GetAlgorithms()
    {
        return Ok(StaticAlgorithmData.GetAlgorithms());
    }

    [HttpGet("{id}")]
    public ActionResult<Algorithm> GetAlgorithm(int id)
    {
        var algorithm = StaticAlgorithmData.GetAlgorithmById(id);
        if (algorithm == null)
        {
            return NotFound();
        }
        return Ok(algorithm);
    }

    [HttpPost("{id}/trace")]
    public ActionResult<List<TraceStep>> GenerateTrace(int id, [FromBody] TraceRequest request)
    {
        var algorithm = StaticAlgorithmData.GetAlgorithmById(id);
        if (algorithm == null)
        {
            return NotFound();
        }

        ITraceGenerator? generator = algorithm.Name switch
        {
            "Bubble Sort" => new BubbleSortTraceGenerator(),
            _ => null
        };

        if (generator == null)
        {
            return BadRequest($"Algorithm '{algorithm.Name}' is not yet implemented");
        }

        try
        {
            var trace = generator.GenerateTrace(request.Input);
            return Ok(trace);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error generating trace: {ex.Message}");
        }
    }
}

public class TraceRequest
{
    public object Input { get; set; } = new();
} 