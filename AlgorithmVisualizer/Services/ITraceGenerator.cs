using AlgorithmVisualizer.Models;

namespace AlgorithmVisualizer.Services;

public interface ITraceGenerator
{
    string AlgorithmName { get; }
    List<TraceStep> GenerateTrace(object input);
} 