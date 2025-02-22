using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Inspector'dan sürükleyerek baðla

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sadece oyuncu tetikleyince çalýþsýn
        {
            if (!audioSource.isPlaying) // Eðer zaten çalmýyorsa
            {
                audioSource.Play();
            }
        }
    }
}
