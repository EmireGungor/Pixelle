using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject deathScreen; // Ölüm ekraný paneli

    private void Start()
    {
        deathScreen.SetActive(false); // Oyun baþýnda kapalý olmalý
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu düþtüðünde
        {
            ShowDeathScreen();
        }
    }

    void ShowDeathScreen()
    {
        deathScreen.SetActive(true); // Ölüm ekranýný aç
        Time.timeScale = 0f; // Oyunu durdur
    }
}
