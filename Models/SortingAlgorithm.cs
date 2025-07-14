namespace AlgorithmVisualizer.Models;

public class SortingAlgorithm
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TimeComplexity { get; set; } = string.Empty;
    public string SpaceComplexity { get; set; } = string.Empty;
    public List<AlgorithmStep> Steps { get; set; } = new();
    public bool IsStable { get; set; }
    public bool IsInPlace { get; set; }
}