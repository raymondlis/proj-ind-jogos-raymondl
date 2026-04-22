using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAction : MonoBehaviour
{
    public void IniciarJogo()
    {
        GameController.Init();
        SceneManager.LoadScene(1); // SampleScene is now index 1
    }

    public void menu()
    {
        SceneManager.LoadScene(0); // MainMenu is now index 0
    }
}
