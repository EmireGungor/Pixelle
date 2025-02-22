using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject finishScreen; // Bitiþ ekraný UI paneli

    private void Start()
    {
        finishScreen.SetActive(false); // Oyun baþýnda kapalý olacak
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu çarpýnca
        {
            ShowFinishScreen();
        }
    }

    void ShowFinishScreen()
    {
        finishScreen.SetActive(true); // Bitiþ ekranýný aç
        Time.timeScale = 0f; // Oyunu durdur
    }
}
