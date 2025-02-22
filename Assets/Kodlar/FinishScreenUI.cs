using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScreenUI : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // Zamaný normale döndür
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Mevcut sahneyi yeniden yükle
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Zamaný normale döndür
        SceneManager.LoadScene("OpeningScene"); // Ana Menü sahnesini yükle (Sahne adýný ayarla)
    }
}
