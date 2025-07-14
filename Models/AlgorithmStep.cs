namespace AlgorithmVisualizer.Models;

public class AlgorithmStep
{
    public int Id { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int[]? Array { get; set; }
    public int[]? Indices { get; set; }
    public string? Highlight { get; set; }
    public bool IsCompleted { get; set; }
    public int Duration { get; set; } = 1000;
}