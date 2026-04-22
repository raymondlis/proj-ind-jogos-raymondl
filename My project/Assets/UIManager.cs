using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI finalTimeText;
    [Header("Audio Settings")]
    public AudioSource musicSource;
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip gameOverSFX;

    void Start()
    {
        GameController.Init();
        
        bool isMenu = SceneManager.GetActiveScene().name.ToLower().Contains("menu");
        
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.clip = isMenu ? menuMusic : gameplayMusic;
            if (musicSource.clip != null) musicSource.Play();
        }
    }

    void Update()
    {
        if (!GameController.isGameOver)
        {
            GameController.UpdateTimer(Time.deltaTime);
            UpdateHUD();
        }
        else
        {
            ShowEndScreen();
        }
    }

    void UpdateHUD()
    {
        if (scoreText != null) scoreText.text = "Vidas: " + GameController.lives;
        if (timerText != null) timerText.text = "Tempo: " + GameController.elapsedTime.ToString("F1") + "s";
    }

    void ShowEndScreen()
    {
        if (endGamePanel != null && !endGamePanel.activeSelf)
        {
            endGamePanel.SetActive(true);
            if (resultText != null) resultText.text = "FIM DE JOGO!";
            if (finalTimeText != null) finalTimeText.text = "Sobreviveu: " + GameController.elapsedTime.ToString("F1") + "s";
            
            // Play Game Over Sound
            if (musicSource != null)
            {
                musicSource.Stop();
                if (gameOverSFX != null) musicSource.PlayOneShot(gameOverSFX);
            }
        }
    }

    public void IrParaMenu()
    {
        GameController.Init(); // Reset state before leaving
        SceneManager.LoadScene(0); // MainMenu is now index 0
    }

    public void ReiniciarCena()
    {
        GameController.Init(); // Reset state before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
