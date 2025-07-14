using AlgorithmVisualizer.Models;
using System.Text.Json;

namespace AlgorithmVisualizer.Services;

public class BubbleSortTraceGenerator : ITraceGenerator
{
    public string AlgorithmName => "Bubble Sort";

    public List<TraceStep> GenerateTrace(object input)
    {
        var trace = new List<TraceStep>();
        
        if (input is not int[] array)
        {
            array = ParseInput(input.ToString() ?? "");
        }

        var stepNumber = 1;
        var n = array.Length;
        var arrayCopy = (int[])array.Clone();

        trace.Add(new TraceStep
        {
            StepNumber = stepNumber++,
            Action = "start",
            State = new { array = arrayCopy, message = "Starting Bubble Sort algorithm" },
            Description = "Initializing the algorithm with the input array"
        });

        for (int i = 0; i < n - 1; i++)
        {
            trace.Add(new TraceStep
            {
                StepNumber = stepNumber++,
                Action = "outer_loop",
                State = new { array = (int[])arrayCopy.Clone(), outerIndex = i, message = $"Starting pass {i + 1}" },
                Description = $"Beginning pass {i + 1} of {n - 1}"
            });

            for (int j = 0; j < n - i - 1; j++)
            {
                trace.Add(new TraceStep
                {
                    StepNumber = stepNumber++,
                    Action = "compare",
                    State = new { array = (int[])arrayCopy.Clone(), leftIndex = j, rightIndex = j + 1, message = $"Comparing elements at positions {j} and {j + 1}" },
                    Description = $"Comparing {arrayCopy[j]} and {arrayCopy[j + 1]}"
                });

                if (arrayCopy[j] > arrayCopy[j + 1])
                {
                    // Swap elements
                    (arrayCopy[j], arrayCopy[j + 1]) = (arrayCopy[j + 1], arrayCopy[j]);
                    
                    trace.Add(new TraceStep
                    {
                        StepNumber = stepNumber++,
                        Action = "swap",
                        State = new { array = (int[])arrayCopy.Clone(), leftIndex = j, rightIndex = j + 1, message = $"Swapped {arrayCopy[j + 1]} and {arrayCopy[j]}" },
                        Description = $"Swapped {arrayCopy[j + 1]} and {arrayCopy[j]} because {arrayCopy[j + 1]} > {arrayCopy[j]}"
                    });
                }
                else
                {
                    trace.Add(new TraceStep
                    {
                        StepNumber = stepNumber++,
                        Action = "no_swap",
                        State = new { array = (int[])arrayCopy.Clone(), leftIndex = j, rightIndex = j + 1, message = "No swap needed" },
                        Description = $"No swap needed: {arrayCopy[j]} <= {arrayCopy[j + 1]}"
                    });
                }
            }

            trace.Add(new TraceStep
            {
                StepNumber = stepNumber++,
                Action = "pass_complete",
                State = new { array = (int[])arrayCopy.Clone(), sortedCount = i + 1, message = $"Pass {i + 1} complete" },
                Description = $"Pass {i + 1} complete. Largest {i + 1} elements are now in their correct positions."
            });
        }

        trace.Add(new TraceStep
        {
            StepNumber = stepNumber++,
            Action = "complete",
            State = new { array = (int[])arrayCopy.Clone(), message = "Bubble Sort complete!" },
            Description = "Algorithm complete! Array is now sorted in ascending order."
        });

        return trace;
    }

    private int[] ParseInput(string input)
    {
        try
        {
            return input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                       .Select(int.Parse)
                       .ToArray();
        }
        catch
        {
            return new int[] { 64, 34, 25, 12, 22, 11, 90 };
        }
    }
} 