using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void IniciarJogo()
    {
        GameController.Init();
        SceneManager.LoadScene(0);
    }

    public void menu()
    {
        SceneManager.LoadScene(1);
    }
}
