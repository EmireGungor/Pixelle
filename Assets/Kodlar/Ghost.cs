using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveSpeed = 2f; // Hareket hýzý
    public float moveDistance = 5f; // Gidip geleceði mesafe
    public float damageAmount = 15f; // Iþýðý azaltma miktarý

    private Vector3 startPosition; // Baþlangýç konumu
    private bool movingRight = true; // Ýlk yönü saða doðru

    private void Start()
    {
        startPosition = transform.position; // Ýlk konumu kaydet
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

        // Yeni pozisyona taþý
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f); // Y ekseninde 180 derece çevirerek yön deðiþtir
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncuya çarpýnca ýþýðý azalt
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
}
