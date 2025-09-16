# SilkRay Examples

This folder contains example projects demonstrating how to use the SilkRay class library - a Raylib-compatible API built on Silk.NET with NativeAOT support.

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022, Visual Studio Code, or JetBrains Rider (recommended)
- Windows, macOS, or Linux

### Open in Visual Studio
1. Open `SilkRayExamples.sln` in your IDE
2. Set any example project as the startup project
3. Build and run (F5 or Ctrl+F5)

## Example Projects

The solution contains 4 example projects plus the SilkRay library:

### 1. BasicShapes
Demonstrates basic 2D shape drawing capabilities:
- Rectangles (filled and outlined)
- Circles (filled and outlined) 
- Lines and triangles
- Animated shapes with rotation
- Color usage and FPS display

### 2. Camera2D
Shows 2D camera system usage:
- Camera target following
- Zoom in/out with mouse wheel
- Camera rotation
- World-to-screen coordinate conversion

### 3. MouseInput
Comprehensive mouse input demonstration:
- Mouse button detection (left, right, middle)
- Mouse position and delta tracking
- Mouse wheel input for cursor changes
- Interactive mouse trail visualization
- Hover detection areas
- Real-time mouse state display

**Controls:**
- Left Click: Move ball and change color
- Right Click: Toggle mouse trail
- Mouse Wheel: Cycle through cursor types

### 4. GamepadInput
Gamepad input handling with keyboard fallback:
- Analog stick movement and rotation
- Button press detection
- Gamepad connection status
- Real-time axis and button state display
- Keyboard controls when no gamepad is connected

**Gamepad Controls:**
- Left Stick: Move player
- Right Stick: Rotate player
- A Button: Change color
- B Button: Reset position

**Keyboard Fallback:**
- WASD/Arrow Keys: Move
- Q/E: Rotate
- Space: Change color
- R: Reset position

## Building and Running

### Using Visual Studio Solution
```bash
# Build entire solution
dotnet build SilkRayExamples.sln

# Run specific example
dotnet run --project BasicShapes
dotnet run --project Camera2D
dotnet run --project MouseInput
dotnet run --project GamepadInput
```

### NativeAOT Publishing
All examples support NativeAOT compilation for optimal performance:

```bash
# Publish any example with NativeAOT
dotnet publish BasicShapes -c Release -r win-x64
dotnet publish Camera2D -c Release -r win-x64
dotnet publish MouseInput -c Release -r win-x64
dotnet publish GamepadInput -c Release -r win-x64
```

**Supported Runtime Identifiers:**
- Windows: `win-x64`, `win-arm64`
- macOS: `osx-x64`, `osx-arm64`
- Linux: `linux-x64`, `linux-arm64`

## SilkRay Library Features

These examples demonstrate the following SilkRay capabilities:

### Core Functions
- Window management (`InitWindow`, `CloseWindow`, `WindowShouldClose`)
- Frame timing (`SetTargetFPS`, `GetFrameTime`, `GetTime`)
- Drawing lifecycle (`BeginDrawing`, `EndDrawing`, `ClearBackground`)

### Shape Drawing
- Basic shapes (rectangles, circles, lines, triangles)
- Outlined and filled variants
- Advanced shapes with rotation and transformations

### Input System
- **Keyboard:** Key state detection, press/release events
- **Mouse:** Button states, position tracking, wheel input, cursor management
- **Gamepad:** Analog sticks, buttons, connection detection

### Text Rendering
- Font rendering with FontStashSharp integration
- FPS display and debug text

### Utilities
- Collision detection
- Random number generation
- Color manipulation

## Project Structure

Each example project:
- References the SilkRay class library via `ProjectReference`
- Targets .NET 8.0 with NativeAOT support enabled
- Uses static imports for clean Raylib-style API usage
- Includes proper error handling and resource management

## API Usage Pattern

```csharp
using SilkRay;
using static SilkRay.RaylibCore;
using static SilkRay.RaylibShapes;
using static SilkRay.RaylibText;

// Initialize
InitWindow(800, 600, "My Game");
SetTargetFPS(60);

// Game loop
while (!WindowShouldClose())
{
    // Update game logic here
    
    BeginDrawing();
    ClearBackground(Color.RayWhite);
    
    // Draw everything here
    DrawText("Hello SilkRay!", 10, 10, 20, Color.Black);
    
    EndDrawing();
}

CloseWindow();
```

## Performance Notes

- All examples are configured for NativeAOT compilation
- SilkRay uses OpenGL ES 3.0 for cross-platform compatibility
- Input handling is optimized with frame-based state tracking
- Memory allocations are minimized in hot paths

## Troubleshooting

**Build Errors:**
- Ensure .NET 8.0 SDK is installed
- Verify SilkRay project builds successfully first
- Check that project references are correct

**Runtime Issues:**
- Ensure graphics drivers support OpenGL ES 3.0
- For NativeAOT builds, ensure all dependencies are properly trimmed
- Check console output for detailed error messages

## Contributing

These examples serve as both demonstrations and tests for the SilkRay library. When adding new features to SilkRay, consider adding corresponding examples here.
