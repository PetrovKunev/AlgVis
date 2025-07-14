namespace AlgorithmVisualizer.Models;

public class Algorithm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string[] ExampleInputs { get; set; } = Array.Empty<string>();
} 