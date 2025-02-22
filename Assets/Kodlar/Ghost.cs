using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveSpeed = 2f; // Hareket h�z�
    public float moveDistance = 5f; // Gidip gelece�i mesafe
    public float damageAmount = 15f; // I���� azaltma miktar�

    private Vector3 startPosition; // Ba�lang�� konumu
    private bool movingRight = true; // �lk y�n� sa�a do�ru

    private void Start()
    {
        startPosition = transform.position; // �lk konumu kaydet
    }

    private void Update()
    {
        // Hareketi hesapla
        float movement = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        float newX = startPosition.x + movement;

        // Y�n de�i�ti mi kontrol et
        if ((newX > transform.position.x && !movingRight) || (newX < transform.position.x && movingRight))
        {
            movingRight = !movingRight;
            Flip();
        }

        // Yeni pozisyona ta��
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f); // Y ekseninde 180 derece �evirerek y�n de�i�tir
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncuya �arp�nca ����� azalt
        {
            LightManager lm = FindObjectOfType<LightManager>();
            if (lm != null)
            {
                lm.TakeDamage(damageAmount);
                Debug.Log("Ghost oyuncunun �����n� azaltt�!");

                Destroy(gameObject); // Ghost kendisini yok eder
            }
        }
    }
}
