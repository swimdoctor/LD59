using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Button restartButton;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        PlayerController.OnPlayerDeath += ShowRestartButton;
    }

    void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= ShowRestartButton;
    }

    void ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}