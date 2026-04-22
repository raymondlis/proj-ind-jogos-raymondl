using UnityEngine;

public static class GameController
{
    public static int lives { get; private set; }
    public static float elapsedTime { get; private set; }
    public static bool isGameOver { get; private set; }

    public static void Init()
    {
        lives = 3;
        elapsedTime = 0;
        isGameOver = false;
    }

    public static void UpdateTimer(float deltaTime)
    {
        if (isGameOver) return;
        elapsedTime += deltaTime;
    }

    public static void LoseLife()
    {
        if (isGameOver) return;
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            isGameOver = true;
        }
    }
}
