# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

AlgVis is an Algorithm & Data Structure Visualizer - an interactive single-page application (SPA) for visualizing classic algorithms with step-by-step animations. Built as a Blazor Server project focused on algorithm education without user management complexity.

## Tech Stack
- **Application**: Blazor Server with API Controllers
- **Database**: Entity Framework Core with SQL Server (optional - for saved examples only)
- **Styling**: TailwindCSS
- **Deployment**: Single project deployment to smarterasp.net

## Project Structure
```
/AlgorithmVisualizer
├── /Controllers (API endpoints for algorithm traces)
├── /Components (Blazor components)
├── /Data (EF Core context & models - optional)
├── /Services (Algorithm engines)
├── /wwwroot (Static files, CSS, JS)
└── /Models (Simple data models)
```

## Development Commands

### Initial Project Setup
```bash
# Create new Blazor Server project (WITHOUT Identity)
dotnet new blazorserver -n AlgorithmVisualizer

# Navigate to project directory
cd AlgorithmVisualizer

# Add only essential NuGet packages
dotnet add package System.Text.Json
```

### TailwindCSS Setup
```bash
# Navigate to wwwroot
cd wwwroot

# Initialize npm and install TailwindCSS
npm init -y
npm install -D tailwindcss @tailwindcss/forms
npx tailwindcss init

# Create input CSS file
echo "@tailwind base; @tailwind components; @tailwind utilities;" > css/input.css

# Build CSS (add to package.json scripts)
npx tailwindcss -i css/input.css -o css/app.css --watch
```

### Development Commands
```bash
# Run the application
dotnet run

# Use dotnet watch for hot reload
dotnet watch run

# Build CSS for production
npm run build-css
```

## Core Models

### Algorithm.cs
```csharp
public class Algorithm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string[] ExampleInputs { get; set; } = Array.Empty<string>();
}
```

### TraceStep.cs
```csharp
public class TraceStep
{
    public int StepNumber { get; set; }
    public string Action { get; set; }
    public object State { get; set; }
    public string Description { get; set; }
}
```

## Algorithm Implementation Pattern

Each algorithm follows this pattern:
1. **ITraceGenerator interface** - Standardized trace generation
2. **Step-by-step state capture** - Each operation creates a TraceStep
3. **JSON serializable state** - For API responses
4. **Educational descriptions** - Clear explanations for each step

Example trace generator structure:
```csharp
public class BubbleSortTraceGenerator : ITraceGenerator
{
    public string AlgorithmName => "Bubble Sort";
    
    public List<TraceStep> GenerateTrace(object input)
    {
        // Generate steps with:
        // - StepNumber (sequential)
        // - Action (compare, swap, complete)
        // - State (current array, highlighted indices)
        // - Description (educational explanation)
    }
}
```

## Component Architecture

### Key Blazor Components
- **AlgorithmSelector.razor** - Choose algorithm and input
- **AlgorithmVisualization.razor** - Main visualization display
- **PlaybackControls.razor** - Play, pause, step controls
- **ArrayVisualizer.razor** - Visual array representation

### Page Routes
- `/` - Home page with algorithm selection
- `/visualize/{algorithmId}` - Algorithm visualization page
- `/about` - Educational information

## TailwindCSS Classes

### Algorithm-specific utility classes:
```css
.array-element - Base array element styling
.array-element-active - Currently active element
.array-element-comparing - Elements being compared
.array-element-sorted - Sorted elements
.control-button - Playback control styling
```

## API Endpoints

### AlgorithmsController
- `GET /api/algorithms` - List all algorithms
- `GET /api/algorithms/{id}` - Get specific algorithm
- `POST /api/algorithms/{id}/trace` - Generate algorithm trace

## Development Priorities

### Phase 1: Core Implementation
1. Basic Blazor Server project setup
2. Bubble Sort with visualization
3. Playback controls
4. Array input functionality

### Phase 2: Algorithm Expansion
1. Selection Sort and Binary Search
2. Algorithm comparison view
3. Speed controls
4. Example datasets

### Phase 3: Educational Enhancement
1. Big O complexity information
2. Step-by-step explanations
3. Educational tooltips
4. Algorithm comparison metrics

## Educational Focus

- **No Authentication** - Direct access for students
- **Visual Learning** - Step-by-step animations
- **Interactive Controls** - Self-paced learning
- **Mobile Friendly** - Tablet and phone support
- **Clear Explanations** - Each step explained

## Performance Requirements

- Fast loading for classroom use
- Smooth animations on older devices
- Offline capability after initial load
- Color-blind friendly palette

## Static Data Structure

Uses `StaticAlgorithmData.cs` for algorithm definitions without database complexity. Supports immediate deployment without migration concerns.

## No Database Initially

Project designed to work without Entity Framework initially:
- Static algorithm data
- File-based configuration
- Simple JSON serialization
- Optional database addition later