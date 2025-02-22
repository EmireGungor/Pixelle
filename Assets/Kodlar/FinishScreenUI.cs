using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScreenUI : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // Zaman� normale d�nd�r
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Mevcut sahneyi yeniden y�kle
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Zaman� normale d�nd�r
        SceneManager.LoadScene("OpeningScene"); // Ana Men� sahnesini y�kle (Sahne ad�n� ayarla)
    }
}
