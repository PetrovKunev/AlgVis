using AlgorithmVisualizer.Models;

namespace AlgorithmVisualizer.Services;

public class DataStructureService
{
    public DataStructure CreateArray()
    {
        return new DataStructure
        {
            Name = "Масив",
            Description = "Линейна структура от данни с елементи от същия тип",
            Type = "array",
            Operations = new[] { "get", "set", "insert", "delete", "search" }
        };
    }

    public DataStructure CreateStack()
    {
        return new DataStructure
        {
            Name = "Стек",
            Description = "LIFO (Last In, First Out) структура от данни",
            Type = "stack",
            Operations = new[] { "push", "pop", "peek", "isEmpty" }
        };
    }

    public DataStructure CreateQueue()
    {
        return new DataStructure
        {
            Name = "Опашка",
            Description = "FIFO (First In, First Out) структура от данни",
            Type = "queue",
            Operations = new[] { "enqueue", "dequeue", "front", "isEmpty" }
        };
    }

    public DataStructure SimulateArrayOperations(List<string> operations)
    {
        var dataStructure = CreateArray();
        var steps = new List<DataStructureStep>();
        var array = new List<object>();
        int stepId = 0;

        foreach (var operation in operations)
        {
            var parts = operation.Split(' ');
            var op = parts[0].ToLower();

            switch (op)
            {
                case "set":
                    if (parts.Length == 3 && int.TryParse(parts[1], out int index) && int.TryParse(parts[2], out int value))
                    {
                        // Ensure array is large enough
                        while (array.Count <= index)
                        {
                            array.Add(0);
                        }
                        
                        array[index] = value;
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "set",
                            Description = $"Задаваме стойност {value} на индекс {index}",
                            Data = array.ToArray(),
                            HighlightIndex = index,
                            Status = "success"
                        });
                    }
                    break;

                case "get":
                    if (parts.Length == 2 && int.TryParse(parts[1], out int getIndex))
                    {
                        if (getIndex < array.Count)
                        {
                            var val = array[getIndex];
                            steps.Add(new DataStructureStep
                            {
                                Id = stepId++,
                                Operation = "get",
                                Description = $"Четем стойност {val} от индекс {getIndex}",
                                Data = array.ToArray(),
                                HighlightIndex = getIndex,
                                Status = "info",
                                Message = $"Стойност: {val}"
                            });
                        }
                        else
                        {
                            steps.Add(new DataStructureStep
                            {
                                Id = stepId++,
                                Operation = "get",
                                Description = $"Опит за четене от невалиден индекс {getIndex}",
                                Data = array.ToArray(),
                                Status = "error",
                                Message = "Индексът е извън границите на масива"
                            });
                        }
                    }
                    break;

                case "search":
                    if (parts.Length == 2 && int.TryParse(parts[1], out int searchValue))
                    {
                        var foundIndex = array.IndexOf(searchValue);
                        if (foundIndex >= 0)
                        {
                            steps.Add(new DataStructureStep
                            {
                                Id = stepId++,
                                Operation = "search",
                                Description = $"Намираме стойност {searchValue} на индекс {foundIndex}",
                                Data = array.ToArray(),
                                HighlightIndex = foundIndex,
                                Status = "success",
                                Message = $"Намерено на индекс {foundIndex}"
                            });
                        }
                        else
                        {
                            steps.Add(new DataStructureStep
                            {
                                Id = stepId++,
                                Operation = "search",
                                Description = $"Търсим стойност {searchValue} - не е намерена",
                                Data = array.ToArray(),
                                Status = "warning",
                                Message = "Стойността не е намерена в масива"
                            });
                        }
                    }
                    break;
            }
        }

        dataStructure.Steps = steps;
        return dataStructure;
    }

    public DataStructure SimulateStackOperations(List<string> operations)
    {
        var dataStructure = CreateStack();
        var steps = new List<DataStructureStep>();
        var stack = new List<object>();
        int stepId = 0;

        foreach (var operation in operations)
        {
            var parts = operation.Split(' ');
            var op = parts[0].ToLower();

            switch (op)
            {
                case "push":
                    if (parts.Length == 2 && int.TryParse(parts[1], out int value))
                    {
                        stack.Add(value);
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "push",
                            Description = $"Добавяме {value} на върха на стека",
                            Data = stack.ToArray(),
                            HighlightIndex = stack.Count - 1,
                            Status = "success"
                        });
                    }
                    break;

                case "pop":
                    if (stack.Count > 0)
                    {
                        var poppedValue = stack.Last();
                        stack.RemoveAt(stack.Count - 1);
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "pop",
                            Description = $"Премахваме {poppedValue} от върха на стека",
                            Data = stack.ToArray(),
                            Status = "info",
                            Message = $"Премахната стойност: {poppedValue}"
                        });
                    }
                    else
                    {
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "pop",
                            Description = "Опит за премахване от празен стек",
                            Data = stack.ToArray(),
                            Status = "error",
                            Message = "Стекът е празен"
                        });
                    }
                    break;

                case "peek":
                    if (stack.Count > 0)
                    {
                        var topValue = stack.Last();
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "peek",
                            Description = $"Гледаме върха на стека: {topValue}",
                            Data = stack.ToArray(),
                            HighlightIndex = stack.Count - 1,
                            Status = "info",
                            Message = $"Връх на стека: {topValue}"
                        });
                    }
                    else
                    {
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "peek",
                            Description = "Опит за гледане на връх от празен стек",
                            Data = stack.ToArray(),
                            Status = "error",
                            Message = "Стекът е празен"
                        });
                    }
                    break;

                case "isempty":
                    var isEmpty = stack.Count == 0;
                    steps.Add(new DataStructureStep
                    {
                        Id = stepId++,
                        Operation = "isEmpty",
                        Description = $"Проверяваме дали стекът е празен: {(isEmpty ? "Да" : "Не")}",
                        Data = stack.ToArray(),
                        Status = "info",
                        Message = isEmpty ? "Стекът е празен" : "Стекът не е празен"
                    });
                    break;
            }
        }

        dataStructure.Steps = steps;
        return dataStructure;
    }

    public DataStructure SimulateQueueOperations(List<string> operations)
    {
        var dataStructure = CreateQueue();
        var steps = new List<DataStructureStep>();
        var queue = new List<object>();
        int stepId = 0;

        foreach (var operation in operations)
        {
            var parts = operation.Split(' ');
            var op = parts[0].ToLower();

            switch (op)
            {
                case "enqueue":
                    if (parts.Length == 2 && int.TryParse(parts[1], out int value))
                    {
                        queue.Add(value);
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "enqueue",
                            Description = $"Добавяме {value} в края на опашката",
                            Data = queue.ToArray(),
                            HighlightIndex = queue.Count - 1,
                            Status = "success"
                        });
                    }
                    break;

                case "dequeue":
                    if (queue.Count > 0)
                    {
                        var dequeuedValue = queue.First();
                        queue.RemoveAt(0);
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "dequeue",
                            Description = $"Премахваме {dequeuedValue} от началото на опашката",
                            Data = queue.ToArray(),
                            Status = "info",
                            Message = $"Премахната стойност: {dequeuedValue}"
                        });
                    }
                    else
                    {
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "dequeue",
                            Description = "Опит за премахване от празна опашка",
                            Data = queue.ToArray(),
                            Status = "error",
                            Message = "Опашката е празна"
                        });
                    }
                    break;

                case "front":
                    if (queue.Count > 0)
                    {
                        var frontValue = queue.First();
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "front",
                            Description = $"Гледаме първия елемент: {frontValue}",
                            Data = queue.ToArray(),
                            HighlightIndex = 0,
                            Status = "info",
                            Message = $"Първи елемент: {frontValue}"
                        });
                    }
                    else
                    {
                        steps.Add(new DataStructureStep
                        {
                            Id = stepId++,
                            Operation = "front",
                            Description = "Опит за гледане на първи елемент от празна опашка",
                            Data = queue.ToArray(),
                            Status = "error",
                            Message = "Опашката е празна"
                        });
                    }
                    break;

                case "isempty":
                    var isEmpty = queue.Count == 0;
                    steps.Add(new DataStructureStep
                    {
                        Id = stepId++,
                        Operation = "isEmpty",
                        Description = $"Проверяваме дали опашката е празна: {(isEmpty ? "Да" : "Не")}",
                        Data = queue.ToArray(),
                        Status = "info",
                        Message = isEmpty ? "Опашката е празна" : "Опашката не е празна"
                    });
                    break;
            }
        }

        dataStructure.Steps = steps;
        return dataStructure;
    }
}