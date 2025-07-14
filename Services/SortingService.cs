using AlgorithmVisualizer.Models;

namespace AlgorithmVisualizer.Services;

public class SortingService
{
    public SortingAlgorithm BubbleSort(int[] array)
    {
        var algorithm = new SortingAlgorithm
        {
            Name = "Bubble Sort",
            Description = "Сравнява съседни елементи и ги разменя ако са в грешен ред",
            TimeComplexity = "O(n²)",
            SpaceComplexity = "O(1)",
            IsStable = true,
            IsInPlace = true
        };

        var steps = new List<AlgorithmStep>();
        var arr = (int[])array.Clone();
        int n = arr.Length;
        int stepId = 0;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                steps.Add(new AlgorithmStep
                {
                    Id = stepId++,
                    Action = "compare",
                    Description = $"Сравняваме {arr[j]} и {arr[j + 1]}",
                    Array = (int[])arr.Clone(),
                    Indices = new[] { j, j + 1 },
                    Highlight = "compare"
                });

                if (arr[j] > arr[j + 1])
                {
                    steps.Add(new AlgorithmStep
                    {
                        Id = stepId++,
                        Action = "swap",
                        Description = $"Разменяме {arr[j]} и {arr[j + 1]}",
                        Array = (int[])arr.Clone(),
                        Indices = new[] { j, j + 1 },
                        Highlight = "swap"
                    });

                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }

        steps.Add(new AlgorithmStep
        {
            Id = stepId++,
            Action = "complete",
            Description = "Масивът е сортиран!",
            Array = (int[])arr.Clone(),
            Highlight = "complete",
            IsCompleted = true
        });

        algorithm.Steps = steps;
        return algorithm;
    }

    public SortingAlgorithm QuickSort(int[] array)
    {
        var algorithm = new SortingAlgorithm
        {
            Name = "Quick Sort",
            Description = "Избира pivot елемент и разделя масива на по-малки и по-големи части",
            TimeComplexity = "O(n log n)",
            SpaceComplexity = "O(log n)",
            IsStable = false,
            IsInPlace = true
        };

        var steps = new List<AlgorithmStep>();
        var arr = (int[])array.Clone();
        int stepId = 0;

        QuickSortRecursive(arr, 0, arr.Length - 1, steps, ref stepId);

        steps.Add(new AlgorithmStep
        {
            Id = stepId++,
            Action = "complete",
            Description = "Масивът е сортиран!",
            Array = (int[])arr.Clone(),
            Highlight = "complete",
            IsCompleted = true
        });

        algorithm.Steps = steps;
        return algorithm;
    }

    private void QuickSortRecursive(int[] arr, int low, int high, List<AlgorithmStep> steps, ref int stepId)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high, steps, ref stepId);
            QuickSortRecursive(arr, low, pi - 1, steps, ref stepId);
            QuickSortRecursive(arr, pi + 1, high, steps, ref stepId);
        }
    }

    private int Partition(int[] arr, int low, int high, List<AlgorithmStep> steps, ref int stepId)
    {
        int pivot = arr[high];
        int i = low - 1;

        steps.Add(new AlgorithmStep
        {
            Id = stepId++,
            Action = "pivot",
            Description = $"Избираме pivot: {pivot}",
            Array = (int[])arr.Clone(),
            Indices = new[] { high },
            Highlight = "pivot"
        });

        for (int j = low; j < high; j++)
        {
            steps.Add(new AlgorithmStep
            {
                Id = stepId++,
                Action = "compare",
                Description = $"Сравняваме {arr[j]} с pivot {pivot}",
                Array = (int[])arr.Clone(),
                Indices = new[] { j, high },
                Highlight = "compare"
            });

            if (arr[j] < pivot)
            {
                i++;
                if (i != j)
                {
                    steps.Add(new AlgorithmStep
                    {
                        Id = stepId++,
                        Action = "swap",
                        Description = $"Разменяме {arr[i]} и {arr[j]}",
                        Array = (int[])arr.Clone(),
                        Indices = new[] { i, j },
                        Highlight = "swap"
                    });

                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
        }

        steps.Add(new AlgorithmStep
        {
            Id = stepId++,
            Action = "swap",
            Description = $"Поставяме pivot на правилната позиция",
            Array = (int[])arr.Clone(),
            Indices = new[] { i + 1, high },
            Highlight = "swap"
        });

        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        return i + 1;
    }

    public SortingAlgorithm SelectionSort(int[] array)
    {
        var algorithm = new SortingAlgorithm
        {
            Name = "Selection Sort",
            Description = "Намира най-малкия елемент и го поставя в началото",
            TimeComplexity = "O(n²)",
            SpaceComplexity = "O(1)",
            IsStable = false,
            IsInPlace = true
        };

        var steps = new List<AlgorithmStep>();
        var arr = (int[])array.Clone();
        int n = arr.Length;
        int stepId = 0;

        for (int i = 0; i < n - 1; i++)
        {
            int minIdx = i;
            
            steps.Add(new AlgorithmStep
            {
                Id = stepId++,
                Action = "find_min",
                Description = $"Търсим най-малкия елемент от позиция {i}",
                Array = (int[])arr.Clone(),
                Indices = new[] { i },
                Highlight = "current"
            });

            for (int j = i + 1; j < n; j++)
            {
                steps.Add(new AlgorithmStep
                {
                    Id = stepId++,
                    Action = "compare",
                    Description = $"Сравняваме {arr[j]} с текущия минимум {arr[minIdx]}",
                    Array = (int[])arr.Clone(),
                    Indices = new[] { j, minIdx },
                    Highlight = "compare"
                });

                if (arr[j] < arr[minIdx])
                {
                    minIdx = j;
                }
            }

            if (minIdx != i)
            {
                steps.Add(new AlgorithmStep
                {
                    Id = stepId++,
                    Action = "swap",
                    Description = $"Разменяме {arr[i]} и {arr[minIdx]}",
                    Array = (int[])arr.Clone(),
                    Indices = new[] { i, minIdx },
                    Highlight = "swap"
                });

                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
            }
        }

        steps.Add(new AlgorithmStep
        {
            Id = stepId++,
            Action = "complete",
            Description = "Масивът е сортиран!",
            Array = (int[])arr.Clone(),
            Highlight = "complete",
            IsCompleted = true
        });

        algorithm.Steps = steps;
        return algorithm;
    }
}