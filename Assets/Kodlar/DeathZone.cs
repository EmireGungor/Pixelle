using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject deathScreen; // �l�m ekran� paneli

    private void Start()
    {
        deathScreen.SetActive(false); // Oyun ba��nda kapal� olmal�
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu d��t���nde
        {
            ShowDeathScreen();
        }
    }

    void ShowDeathScreen()
    {
        deathScreen.SetActive(true); // �l�m ekran�n� a�
        Time.timeScale = 0f; // Oyunu durdur
    }
}
