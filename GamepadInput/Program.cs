using static SilkRay.Core;
using static SilkRay.Shapes;
using static SilkRay.Text;
using System.Numerics;

namespace GamepadInput;

class Program
{
    static void Main()
    {
        // Initialize window
        const int screenWidth = 800;
        const int screenHeight = 600;
        
        InitWindow(screenWidth, screenHeight, "SilkRay Example - Gamepad Input");
        SetTargetFPS(60);

        // Game state
        Vector2 playerPosition = new Vector2(400, 300);
        Vector2 playerVelocity = new Vector2(0, 0);
        Color playerColor = BLUE;
        float playerSpeed = 200.0f;
        float rotation = 0.0f;
        
        // Gamepad state
        int gamepadId = 0;
        bool gamepadConnected = false;

        while (!WindowShouldClose())
        {
            float deltaTime = GetFrameTime();
            
            // Update gamepad connection status
            gamepadConnected = IsGamepadAvailable(gamepadId);
            
            // Update player movement with gamepad or keyboard
            Vector2 movement = new Vector2(0, 0);
            
            if (gamepadConnected)
            {
                // Gamepad input
                float leftX = GetGamepadAxisMovement(gamepadId, GAMEPAD_AXIS_LEFT_X);
                float leftY = GetGamepadAxisMovement(gamepadId, GAMEPAD_AXIS_LEFT_Y);
                
                movement.X = leftX;
                movement.Y = leftY;
                
                // Right stick for rotation
                float rightX = GetGamepadAxisMovement(gamepadId, GAMEPAD_AXIS_RIGHT_X);
                if (MathF.Abs(rightX) > 0.1f)
                {
                    rotation += rightX * 180.0f * deltaTime;
                }
                
                // Button actions
                if (IsGamepadButtonPressed(gamepadId, GAMEPAD_BUTTON_RIGHT_FACE_DOWN)) // A button
                {
                    playerColor = new Color(
                        (byte)GetRandomValue(0, 255),
                        (byte)GetRandomValue(0, 255),
                        (byte)GetRandomValue(0, 255),
                        (byte)255
                    );
                }
                
                if (IsGamepadButtonPressed(gamepadId, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT)) // B button
                {
                    playerPosition = new Vector2(400, 300); // Reset position
                    rotation = 0.0f;
                }
            }
            else
            {
                // Keyboard fallback
                if (IsKeyDown(KEY_RIGHT) || IsKeyDown(KEY_D)) movement.X = 1.0f;
                if (IsKeyDown(KEY_LEFT) || IsKeyDown(KEY_A)) movement.X = -1.0f;
                if (IsKeyDown(KEY_DOWN) || IsKeyDown(KEY_S)) movement.Y = 1.0f;
                if (IsKeyDown(KEY_UP) || IsKeyDown(KEY_W)) movement.Y = -1.0f;
                
                if (IsKeyDown(KEY_Q)) rotation -= 180.0f * deltaTime;
                if (IsKeyDown(KEY_E)) rotation += 180.0f * deltaTime;
                
                if (IsKeyPressed(KEY_SPACE))
                {
                    playerColor = new Color(
                        (byte)GetRandomValue(0, 255),
                        (byte)GetRandomValue(0, 255),
                        (byte)GetRandomValue(0, 255),
                        (byte)255
                    );
                }
                
                if (IsKeyPressed(KEY_R))
                {
                    playerPosition = new Vector2(400, 300);
                    rotation = 0.0f;
                }
            }
            
            // Apply movement with deadzone
            if (MathF.Sqrt(movement.X * movement.X + movement.Y * movement.Y) > 0.1f)
            {
                playerVelocity.X = movement.X * playerSpeed;
                playerVelocity.Y = movement.Y * playerSpeed;
            }
            else
            {
                playerVelocity.X = 0;
                playerVelocity.Y = 0;
            }
            
            // Update position
            playerPosition.X += playerVelocity.X * deltaTime;
            playerPosition.Y += playerVelocity.Y * deltaTime;
            
            // Keep player on screen
            if (playerPosition.X < 25) playerPosition.X = 25;
            if (playerPosition.X > screenWidth - 25) playerPosition.X = screenWidth - 25;
            if (playerPosition.Y < 25) playerPosition.Y = 25;
            if (playerPosition.Y > screenHeight - 25) playerPosition.Y = screenHeight - 25;
            
            // Draw
            BeginDrawing();
            ClearBackground(RAYWHITE);
            
            // Draw title and instructions
            DrawText("Gamepad Input Example", 10, 10, 20, DARKGRAY);
            
            if (gamepadConnected)
            {
                DrawText("Gamepad Connected!", 10, 40, 16, GREEN);
                DrawText("Left Stick: Move", 10, 60, 14, GRAY);
                DrawText("Right Stick: Rotate", 10, 80, 14, GRAY);
                DrawText("A Button: Change Color", 10, 100, 14, GRAY);
                DrawText("B Button: Reset Position", 10, 120, 14, GRAY);
                
                // Draw gamepad info
                DrawText($"Gamepad: Connected", 10, 150, 14, BLACK);
                
                // Draw axis values
                float leftX = GetGamepadAxisMovement(gamepadId, GAMEPAD_AXIS_LEFT_X);
                float leftY = GetGamepadAxisMovement(gamepadId, GAMEPAD_AXIS_LEFT_Y);
                float rightX = GetGamepadAxisMovement(gamepadId, GAMEPAD_AXIS_RIGHT_X);
                float rightY = GetGamepadAxisMovement(gamepadId, GAMEPAD_AXIS_RIGHT_Y);
                
                DrawText($"Left Stick: ({leftX:F2}, {leftY:F2})", 10, 180, 14, BLACK);
                DrawText($"Right Stick: ({rightX:F2}, {rightY:F2})", 10, 200, 14, BLACK);
                
                // Draw button states
                DrawText("Buttons:", 10, 230, 14, BLACK);
                DrawText($"A: {(IsGamepadButtonDown(gamepadId, GAMEPAD_BUTTON_RIGHT_FACE_DOWN) ? "DOWN" : "UP")}", 10, 250, 12, RED);
                DrawText($"B: {(IsGamepadButtonDown(gamepadId, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT) ? "DOWN" : "UP")}", 10, 270, 12, GREEN);
                DrawText($"X: {(IsGamepadButtonDown(gamepadId, GAMEPAD_BUTTON_RIGHT_FACE_LEFT) ? "DOWN" : "UP")}", 10, 290, 12, BLUE);
                DrawText($"Y: {(IsGamepadButtonDown(gamepadId, GAMEPAD_BUTTON_RIGHT_FACE_UP) ? "DOWN" : "UP")}", 10, 310, 12, YELLOW);
            }
            else
            {
                DrawText("No Gamepad Connected", 10, 40, 16, RED);
                DrawText("Using Keyboard Controls:", 10, 70, 14, GRAY);
                DrawText("WASD/Arrow Keys: Move", 10, 90, 14, GRAY);
                DrawText("Q/E: Rotate", 10, 110, 14, GRAY);
                DrawText("Space: Change Color", 10, 130, 14, GRAY);
                DrawText("R: Reset Position", 10, 150, 14, GRAY);
            }
            
            // Draw player as rotated rectangle
            DrawRectanglePro(
                new Rectangle(playerPosition.X, playerPosition.Y, 50, 30),
                new Vector2(25, 15),
                rotation,
                playerColor
            );
            
            // Draw player direction indicator
            float dirX = MathF.Cos(rotation * MathF.PI / 180.0f) * 30;
            float dirY = MathF.Sin(rotation * MathF.PI / 180.0f) * 30;
            DrawLine(
                (int)playerPosition.X, 
                (int)playerPosition.Y,
                (int)(playerPosition.X + dirX),
                (int)(playerPosition.Y + dirY),
                WHITE
            );
            
            // Draw velocity indicator
            if (playerVelocity.X != 0 || playerVelocity.Y != 0)
            {
                Vector2 velEnd = new Vector2(
                    playerPosition.X + playerVelocity.X * 0.1f,
                    playerPosition.Y + playerVelocity.Y * 0.1f
                );
                DrawLineV(playerPosition, velEnd, RED);
            }
            
            // Draw boundaries
            DrawRectangleLines(0, 0, screenWidth, screenHeight, BLACK);
            
            // Draw FPS
            DrawFPS(10, screenHeight - 30);
            
            EndDrawing();
        }

        CloseWindow();
    }
}
