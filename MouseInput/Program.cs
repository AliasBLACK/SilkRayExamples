using static SilkRay.Core;
using static SilkRay.Shapes;
using static SilkRay.Text;
using System.Numerics;

namespace MouseInput;

class Program
{
    static void Main()
    {
        // Initialize window
        const int screenWidth = 800;
        const int screenHeight = 600;
        
        InitWindow(screenWidth, screenHeight, "SilkRay Example - Mouse Input");
        SetTargetFPS(60);

        // Game state
        Vector2 ballPosition = new Vector2(400, 300);
        Color ballColor = BLUE;
        List<Vector2> mouseTrail = new List<Vector2>();
        bool showTrail = false;
        int currentCursor = 0;
        
        // Mouse cursor types for cycling
        int[] cursorTypes = {
            MOUSE_CURSOR_DEFAULT,
            MOUSE_CURSOR_ARROW,
            MOUSE_CURSOR_IBEAM,
            MOUSE_CURSOR_CROSSHAIR,
            MOUSE_CURSOR_POINTING_HAND,
            MOUSE_CURSOR_RESIZE_EW,
            MOUSE_CURSOR_RESIZE_NS
        };
        
        string[] cursorNames = {
            "DEFAULT", "ARROW", "IBEAM", "CROSSHAIR", 
            "POINTING_HAND", "RESIZE_EW", "RESIZE_NS"
        };

        while (!WindowShouldClose())
        {
            // Update
            Vector2 mousePos = GetMousePosition();
            Vector2 mouseDelta = GetMouseDelta();
            
            // Left click: Move ball to mouse position
            if (IsMouseButtonPressed(MOUSE_BUTTON_LEFT))
            {
                ballPosition = mousePos;
                ballColor = new Color(
                    (byte)GetRandomValue(0, 255),
                    (byte)GetRandomValue(0, 255),
                    (byte)GetRandomValue(0, 255),
                    (byte)255
                );
            }
            
            // Right click: Toggle mouse trail
            if (IsMouseButtonPressed(MOUSE_BUTTON_RIGHT))
            {
                showTrail = !showTrail;
                if (!showTrail) mouseTrail.Clear();
            }
            
            // Mouse wheel: Change cursor
            float wheelMove = GetMouseWheelMove();
            if (wheelMove != 0)
            {
                currentCursor += wheelMove > 0 ? 1 : -1;
                if (currentCursor >= cursorTypes.Length) currentCursor = 0;
                if (currentCursor < 0) currentCursor = cursorTypes.Length - 1;
                SetMouseCursor(cursorTypes[currentCursor]);
            }
            
            // Add to mouse trail
            if (showTrail && (mouseTrail.Count == 0 || Vector2.Distance(mouseTrail.Last(), mousePos) > 5))
            {
                mouseTrail.Add(mousePos);
                if (mouseTrail.Count > 50) mouseTrail.RemoveAt(0);
            }
            
            // Draw
            BeginDrawing();
            ClearBackground(RAYWHITE);
            
            // Draw title and instructions
            DrawText("Mouse Input Example", 10, 10, 20, DARKGRAY);
            DrawText("Left Click: Move ball", 10, 40, 16, GRAY);
            DrawText("Right Click: Toggle trail", 10, 60, 16, GRAY);
            DrawText("Mouse Wheel: Change cursor", 10, 80, 16, GRAY);
            
            // Draw mouse information
            DrawText($"Mouse Position: ({mousePos.X:F0}, {mousePos.Y:F0})", 10, 120, 16, BLACK);
            DrawText($"Mouse Delta: ({mouseDelta.X:F1}, {mouseDelta.Y:F1})", 10, 140, 16, BLACK);
            DrawText($"Wheel Move: {GetMouseWheelMove():F1}", 10, 160, 16, BLACK);
            DrawText($"Current Cursor: {cursorNames[currentCursor]}", 10, 180, 16, BLACK);
            
            // Draw button states
            string leftState = IsMouseButtonDown(MOUSE_BUTTON_LEFT) ? "DOWN" : "UP";
            string rightState = IsMouseButtonDown(MOUSE_BUTTON_RIGHT) ? "DOWN" : "UP";
            string middleState = IsMouseButtonDown(MOUSE_BUTTON_MIDDLE) ? "DOWN" : "UP";
            
            DrawText($"Left: {leftState}", 10, 220, 16, RED);
            DrawText($"Right: {rightState}", 10, 240, 16, GREEN);
            DrawText($"Middle: {middleState}", 10, 260, 16, BLUE);
            
            // Draw test areas
            Rectangle testArea1 = new Rectangle(500, 100, 150, 100);
            Rectangle testArea2 = new Rectangle(500, 220, 150, 100);
            
            bool hover1 = CheckCollisionPointRec(mousePos, testArea1);
            bool hover2 = CheckCollisionPointRec(mousePos, testArea2);
            
            DrawRectangleRec(testArea1, hover1 ? LIGHTGRAY : GRAY);
            DrawRectangleRec(testArea2, hover2 ? LIGHTGRAY : GRAY);
            DrawText("Hover Area 1", 510, 140, 16, BLACK);
            DrawText("Hover Area 2", 510, 260, 16, BLACK);
            
            // Draw mouse trail
            if (showTrail)
            {
                for (int i = 0; i < mouseTrail.Count - 1; i++)
                {
                    float alpha = (float)i / mouseTrail.Count;
                    Color trailColor = new Color((byte)255, (byte)0, (byte)0, (byte)(alpha * 255));
                    DrawLineV(mouseTrail[i], mouseTrail[i + 1], trailColor);
                }
            }
            
            // Draw ball
            DrawCircleV(ballPosition, 25, ballColor);
            DrawCircleLines((int)ballPosition.X, (int)ballPosition.Y, 25, BLACK);
            
            // Draw crosshair at mouse position
            DrawLine((int)mousePos.X - 10, (int)mousePos.Y, (int)mousePos.X + 10, (int)mousePos.Y, RED);
            DrawLine((int)mousePos.X, (int)mousePos.Y - 10, (int)mousePos.X, (int)mousePos.Y + 10, RED);
            
            // Draw FPS
            DrawFPS(10, screenHeight - 30);
            
            EndDrawing();
        }

        CloseWindow();
    }
}
