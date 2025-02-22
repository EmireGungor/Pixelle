using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject finishScreen; // Biti� ekran� UI paneli

    private void Start()
    {
        finishScreen.SetActive(false); // Oyun ba��nda kapal� olacak
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu �arp�nca
        {
            ShowFinishScreen();
        }
    }

    void ShowFinishScreen()
    {
        finishScreen.SetActive(true); // Biti� ekran�n� a�
        Time.timeScale = 0f; // Oyunu durdur
    }
}
