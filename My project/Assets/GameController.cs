using UnityEngine;

public static class GameController
{
    private static int score;
    public static bool isGameOver
    {
        get {return score <=0;}
    }

    public static void Init()
    {
        score =4;
    }

    public static void collect()
    {
        score--;
    }
}
