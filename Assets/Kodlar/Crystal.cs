using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float lightAmount = 20f; // Kristalin artıracağı ışık miktarı
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>(); // Animator bileşenini al
        if (animator == null)
        {
            Debug.LogWarning("Animator bileşeni atanmadı!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sadece "Player" tag'ine sahip objeler etkilenir
        {
            LightManager lm = FindObjectOfType<LightManager>(); // Sahnedeki LightManager’ı bul
            if (lm != null)
            {
                lm.CollectCrystal(lightAmount); // Işık seviyesini artır
            }
            else
            {
                Debug.LogWarning("LightManager bulunamadı!");
            }

            Destroy(gameObject); // Kristali yok et
        }
    }
}
