# SilkRay Examples

This repository contains example projects demonstrating how to use the SilkRay class library - a Raylib-compatible 2D graphics API built on Silk.NET with full NativeAOT support.

## About SilkRay

SilkRay is available as a NuGet package and focuses on 2D game development and graphics programming. It provides a familiar Raylib API while leveraging the performance benefits of Silk.NET and .NET's NativeAOT compilation.

**Install SilkRay:**
```bash
dotnet add package SilkRay
```

**NuGet Package:** https://www.nuget.org/packages/SilkRay/  
**Main Repository:** https://github.com/AliasBLACK/SilkRay

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

The solution contains 4 example projects showcasing SilkRay's 2D capabilities:

### 1. BasicShapes
Demonstrates core 2D shape drawing functionality:
- Rectangles (filled and outlined)
- Circles (filled and outlined) 
- Lines and triangles
- Animated shapes with rotation
- Color usage and FPS display

### 2. Camera2D
Shows 2D camera system for games and applications:
- Camera target following
- Zoom in/out with mouse wheel
- Camera rotation around target
- World-to-screen coordinate conversion
- Viewport transformations

### 3. MouseInput
Comprehensive 2D mouse input handling:
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
2D gamepad input with keyboard fallback:
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

## SilkRay 2D Library Features

These examples demonstrate SilkRay's comprehensive 2D graphics and input capabilities:

### 2D Graphics Core
- Window management (`InitWindow`, `CloseWindow`, `WindowShouldClose`)
- Frame timing and synchronization (`SetTargetFPS`, `GetFrameTime`, `GetTime`)
- 2D drawing lifecycle (`BeginDrawing`, `EndDrawing`, `ClearBackground`)

### 2D Shape Drawing
- Basic 2D shapes (rectangles, circles, lines, triangles)
- Outlined and filled variants
- 2D transformations with rotation and scaling
- Color blending and transparency

### 2D Camera System
- Camera2D with target following
- Zoom, rotation, and offset controls
- World-to-screen coordinate conversion
- Viewport transformations for 2D scenes

### Input System
- **Keyboard:** Key state detection, press/release events
- **Mouse:** Button states, position tracking, wheel input, cursor management
- **Gamepad:** Analog sticks, buttons, connection detection with keyboard fallback

### 2D Text Rendering
- Font rendering with FontStashSharp integration
- FPS display and debug text overlay
- Text positioning and alignment

### 2D Utilities
- 2D collision detection
- Random number generation
- Color manipulation and palettes
- Vector2 math operations

## Project Structure

Each example project:
- References SilkRay via NuGet package (`<PackageReference Include="SilkRay" />`)
- Targets .NET 8.0 with NativeAOT support enabled
- Uses static imports for clean Raylib-style API usage
- Includes proper error handling and resource management

## API Usage Pattern

```csharp
using static SilkRay.Core;
using static SilkRay.Shapes;
using static SilkRay.Text;

// Initialize 2D window
InitWindow(800, 600, "My 2D Game");
SetTargetFPS(60);

// Game loop
while (!WindowShouldClose())
{
    // Update 2D game logic here
    
    BeginDrawing();
    ClearBackground(RAYWHITE);
    
    // Draw 2D graphics
    DrawRectangle(100, 100, 200, 150, RED);
    DrawCircle(400, 300, 50, BLUE);
    DrawText("Hello SilkRay 2D!", 10, 10, 20, BLACK);
    
    EndDrawing();
}

CloseWindow();
```

## Performance Notes

- All examples are configured for NativeAOT compilation for optimal 2D performance
- SilkRay uses OpenGL for cross-platform 2D graphics compatibility
- 2D input handling is optimized with frame-based state tracking
- Memory allocations are minimized in 2D rendering hot paths
- Efficient 2D batch rendering for shapes and sprites

## Troubleshooting

**Build Errors:**
- Ensure .NET 8.0 SDK is installed
- Verify SilkRay NuGet package is available: `dotnet add package SilkRay`
- Clear NuGet cache if package resolution fails: `dotnet nuget locals all --clear`

**Runtime Issues:**
- Ensure graphics drivers support OpenGL for 2D rendering
- For NativeAOT builds, ensure all dependencies are properly trimmed
- Check console output for detailed error messages

## Contributing

These examples serve as both demonstrations and tests for SilkRay's 2D capabilities. When adding new 2D features to SilkRay, consider adding corresponding examples here.

**Repository:** https://github.com/AliasBLACK/SilkRayExamples
