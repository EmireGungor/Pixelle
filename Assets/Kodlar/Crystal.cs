using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float lightAmount = 20f; // Kristalin artıracağı ışık miktarı
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (animator == null)
        {
            Debug.LogWarning("Animator bileşeni atanmadı!");
        }
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource bileşeni atanmadı!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LightManager lm = FindObjectOfType<LightManager>();
            if (lm != null)
            {
                lm.CollectCrystal(lightAmount);
            }
            else
            {
                Debug.LogWarning("LightManager bulunamadı!");
            }

            if (animator != null)
            {
                animator.SetTrigger("Collect");
            }

            if (audioSource != null && audioSource.clip != null)
            {
                // Yeni bir boş nesne oluştur ve sesi burada çal
                GameObject tempAudio = new GameObject("CrystalSound");
                AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
                tempSource.clip = audioSource.clip;
                tempSource.volume = audioSource.volume;
                tempSource.pitch = audioSource.pitch;
                tempSource.Play();
                Destroy(tempAudio, tempSource.clip.length); // Ses çalınca nesneyi yok et
            }

            Destroy(gameObject); // Obje hemen yok olsun
        }
    }
}
