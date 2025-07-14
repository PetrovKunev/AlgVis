namespace AlgorithmVisualizer.Models;

public class DataStructure
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<DataStructureStep> Steps { get; set; } = new();
    public string[] Operations { get; set; } = Array.Empty<string>();
}

public class DataStructureStep
{
    public int Id { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public object[] Data { get; set; } = Array.Empty<object>();
    public int? HighlightIndex { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Message { get; set; }
}