using static SilkRay.Core;
using static SilkRay.Shapes;
using static SilkRay.Text;
using System.Numerics;

namespace Camera2DExample;

class Program
{
    const int MAX_BUILDINGS = 100;

    static void Main()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "SilkRay Example - 2D Camera");

        Rectangle player = new Rectangle(400, 280, 40, 40);
        Rectangle[] buildings = new Rectangle[MAX_BUILDINGS];
        Color[] buildColors = new Color[MAX_BUILDINGS];

        int spacing = 0;

        for (int i = 0; i < MAX_BUILDINGS; i++)
        {
            buildings[i].Width = GetRandomValue(50, 200);
            buildings[i].Height = GetRandomValue(100, 800);
            buildings[i].Y = screenHeight - 130.0f - buildings[i].Height;
            buildings[i].X = -6000.0f + spacing;

            spacing += (int)buildings[i].Width;

            buildColors[i] = new Color(
                GetRandomValue(200, 240),
                GetRandomValue(200, 240),
                GetRandomValue(200, 250),
                255
            );
        }

        Camera2D camera = new Camera2D();
        camera.Target = new Vector2(player.X + 20.0f, player.Y + 20.0f);
        camera.Offset = new Vector2(screenWidth / 2.0f, screenHeight / 2.0f);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            //----------------------------------------------------------------------------------
            
            // Player movement
            if (IsKeyDown(KEY_RIGHT)) player.X += 2;
            else if (IsKeyDown(KEY_LEFT)) player.X -= 2;

            // Camera target follows player
            camera.Target = new Vector2(player.X + 20, player.Y + 20);

            // Camera rotation controls
            if (IsKeyDown(KEY_A)) camera.Rotation--;
            else if (IsKeyDown(KEY_S)) camera.Rotation++;

            // Limit camera rotation to 80 degrees (-40 to 40)
            if (camera.Rotation > 40) camera.Rotation = 40;
            else if (camera.Rotation < -40) camera.Rotation = -40;

            // Camera zoom controls
            camera.Zoom += GetMouseWheelMove() * 0.05f;

            if (camera.Zoom > 3.0f) camera.Zoom = 3.0f;
            else if (camera.Zoom < 0.1f) camera.Zoom = 0.1f;

            // Camera reset (zoom and rotation)
            if (IsKeyPressed(KEY_R))
            {
                camera.Zoom = 1.0f;
                camera.Rotation = 0.0f;
            }

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                BeginMode2D(camera);

                    DrawRectangle(-6000, 320, 13000, 8000, DARKGRAY);

                    for (int i = 0; i < MAX_BUILDINGS; i++) 
                        DrawRectangleRec(buildings[i], buildColors[i]);

                    DrawRectangleRec(player, RED);

                    DrawLine((int)camera.Target.X, -screenHeight * 10, (int)camera.Target.X, screenHeight * 10, GREEN);
                    DrawLine(-screenWidth * 10, (int)camera.Target.Y, screenWidth * 10, (int)camera.Target.Y, GREEN);

                EndMode2D();

                DrawText("SCREEN AREA", 640, 10, 20, RED);

                DrawRectangle(0, 0, screenWidth, 5, RED);
                DrawRectangle(0, 5, 5, screenHeight - 10, RED);
                DrawRectangle(screenWidth - 5, 5, 5, screenHeight - 10, RED);
                DrawRectangle(0, screenHeight - 5, screenWidth, 5, RED);

                DrawRectangle(10, 10, 250, 113, new Color(135, 206, 235, 128));
                DrawRectangleLines(10, 10, 250, 113, BLUE);

                DrawText("Free 2d camera controls:", 20, 20, 10, BLACK);
                DrawText("- Right/Left to move Offset", 40, 40, 10, DARKGRAY);
                DrawText("- Mouse Wheel to Zoom in-out", 40, 60, 10, DARKGRAY);
                DrawText("- A / S to Rotate", 40, 80, 10, DARKGRAY);
                DrawText("- R to reset Zoom and Rotation", 40, 100, 10, DARKGRAY);

            EndDrawing();
        }

        // De-Initialization
        CloseWindow();
    }
}
