using Microsoft.AspNetCore.Mvc;
using AlgorithmVisualizer.Services;

namespace AlgorithmVisualizer.Controllers;

public class HomeController : Controller
{
    private readonly SortingService _sortingService;
    private readonly DataStructureService _dataStructureService;

    public HomeController(SortingService sortingService, DataStructureService dataStructureService)
    {
        _sortingService = sortingService;
        _dataStructureService = dataStructureService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Sorting()
    {
        return View();
    }

    public IActionResult DataStructures()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetSortingSteps(string algorithm, int[] array)
    {
        var result = algorithm.ToLower() switch
        {
            "bubble" => _sortingService.BubbleSort(array),
            "quick" => _sortingService.QuickSort(array),
            "selection" => _sortingService.SelectionSort(array),
            _ => throw new ArgumentException("Неподдържан алгоритъм")
        };

        return Json(result);
    }

    [HttpPost]
    public IActionResult GetDataStructureSteps(string type, List<string> operations)
    {
        var result = type.ToLower() switch
        {
            "array" => _dataStructureService.SimulateArrayOperations(operations),
            "stack" => _dataStructureService.SimulateStackOperations(operations),
            "queue" => _dataStructureService.SimulateQueueOperations(operations),
            _ => throw new ArgumentException("Неподдържан тип структура")
        };

        return Json(result);
    }
}