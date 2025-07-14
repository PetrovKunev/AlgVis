namespace AlgorithmVisualizer.Models;

public class TraceStep
{
    public int StepNumber { get; set; }
    public string Action { get; set; } = string.Empty;
    public object State { get; set; } = new();
    public string Description { get; set; } = string.Empty;
} 