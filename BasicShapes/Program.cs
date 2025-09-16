using static SilkRay.Core;
using static SilkRay.Shapes;
using static SilkRay.Text;
using System.Numerics;

namespace BasicShapes;

class Program
{
    static void Main()
    {
        // Initialize window
        const int screenWidth = 800;
        const int screenHeight = 600;
        
        InitWindow(screenWidth, screenHeight, "SilkRay Example - Basic Shapes");
        SetTargetFPS(60);

        // Animation variables
        float rotation = 0.0f;
        Vector2 ballPosition = new Vector2(screenWidth / 2.0f, screenHeight / 2.0f);
        
        while (!WindowShouldClose())
        {
            // Update
            rotation += 1.0f;
            if (rotation > 360.0f) rotation = 0.0f;
            
            // Draw
            BeginDrawing();
            ClearBackground(RAYWHITE);
            
            // Draw title
            DrawText("Basic Shapes Example", 10, 10, 20, DARKGRAY);
            
            // Draw various shapes
            DrawRectangle(100, 100, 200, 150, RED);
            DrawRectangleLines(320, 100, 200, 150, BLUE);
            
            DrawCircle(150, 350, 50, GREEN);
            DrawCircleLines(350, 350, 50, PURPLE);
            
            // Draw rotating rectangle
            DrawRectanglePro(
                new Rectangle(500, 200, 100, 60),
                new Vector2(50, 30),
                rotation,
                ORANGE
            );
            
            // Draw animated bouncing ball
            ballPosition.X += MathF.Sin((float)GetTime() * 2.0f) * 2.0f;
            DrawCircleV(ballPosition, 20, MAROON);
            
            // Draw some lines
            DrawLine(50, 500, 750, 500, BLACK);
            DrawLineV(new Vector2(50, 520), new Vector2(750, 520), GRAY);
            
            // Draw triangle
            DrawTriangle(
                new Vector2(600, 400),
                new Vector2(650, 500),
                new Vector2(550, 500),
                VIOLET
            );
            
            // Draw FPS
            DrawFPS(10, screenHeight - 30);
            
            EndDrawing();
        }

        CloseWindow();
    }
}
