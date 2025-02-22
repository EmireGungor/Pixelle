using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Inspector'dan s�r�kleyerek ba�la

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sadece oyuncu tetikleyince �al��s�n
        {
            if (!audioSource.isPlaying) // E�er zaten �alm�yorsa
            {
                audioSource.Play();
            }
        }
    }
}
