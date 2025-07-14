using AlgorithmVisualizer.Models;

namespace AlgorithmVisualizer.Services;

public static class StaticAlgorithmData
{
    public static List<Algorithm> GetAlgorithms()
    {
        return new List<Algorithm>
        {
            new Algorithm
            {
                Id = 1,
                Name = "Bubble Sort",
                Description = "A simple sorting algorithm that repeatedly steps through the list, compares adjacent elements and swaps them if they are in the wrong order.",
                Category = "Sorting",
                ExampleInputs = new string[]
                {
                    "64,34,25,12,22,11,90",
                    "5,2,8,1,9,3",
                    "100,50,25,75,125"
                }
            },
            new Algorithm
            {
                Id = 2,
                Name = "Selection Sort",
                Description = "A sorting algorithm that divides the input list into two parts: a sorted sublist and an unsorted sublist.",
                Category = "Sorting",
                ExampleInputs = new string[]
                {
                    "64,34,25,12,22,11,90",
                    "5,2,8,1,9,3",
                    "100,50,25,75,125"
                }
            },
            new Algorithm
            {
                Id = 3,
                Name = "Binary Search",
                Description = "A search algorithm that finds the position of a target value within a sorted array.",
                Category = "Searching",
                ExampleInputs = new string[]
                {
                    "1,3,5,7,9,11,13,15",
                    "2,4,6,8,10,12,14,16",
                    "10,20,30,40,50,60,70,80"
                }
            }
        };
    }

    public static Algorithm? GetAlgorithmById(int id)
    {
        return GetAlgorithms().FirstOrDefault(a => a.Id == id);
    }
} 