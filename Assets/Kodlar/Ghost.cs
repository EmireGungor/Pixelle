using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveSpeed = 2f; // Hareket hýzý
    public float moveDistance = 5f; // Gidip geleceði mesafe
    public float damageAmount = 15f; // Iþýðý azaltma miktarý
    public float detectionRange = 5f; // Oyuncuyu algýlama mesafesi
    public AudioClip alertSound; // Düþman algýladýðýnda çalacak ses

    private Vector3 startPosition;
    private bool movingRight = true;
    private Transform player;
    private AudioSource audioSource;
    private bool isPlayingSound = false;

    private void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (alertSound != null)
        {
            audioSource.clip = alertSound;
            audioSource.loop = true; // Ses döngüde çalsýn (isteðe baðlý)
        }
    }

    private void Update()
    {
        // Hareketi hesapla
        float movement = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        float newX = startPosition.x + movement;

        // Yön deðiþti mi kontrol et
        if ((newX > transform.position.x && !movingRight) || (newX < transform.position.x && movingRight))
        {
            movingRight = !movingRight;
            Flip();
        }

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Oyuncu algýlandý mý?
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange && !isPlayingSound)
            {
                PlayAlertSound();
            }
            else if (distanceToPlayer > detectionRange && isPlayingSound)
            {
                StopAlertSound();
            }
        }
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LightManager lm = FindObjectOfType<LightManager>();
            if (lm != null)
            {
                lm.TakeDamage(damageAmount);
                Debug.Log("Ghost oyuncunun ýþýðýný azalttý!");

                Destroy(gameObject); // Ghost kendisini yok eder
            }
        }
    }

    void PlayAlertSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            isPlayingSound = true;
        }
    }

    void StopAlertSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            isPlayingSound = false;
        }
    }

    private void OnDestroy()
    {
        StopAlertSound(); // Obje yok edildiðinde sesi kes
    }
}
