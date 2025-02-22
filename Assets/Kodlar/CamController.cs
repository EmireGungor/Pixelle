using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothTime = 0.2f; // Kameran�n yumu�ak hareket s�resi
    public Vector2 minLimits; // Kamera s�n�rlar� (Sol Alt)
    public Vector2 maxLimits; // Kamera s�n�rlar� (Sa� �st)

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (playerTransform == null)
            return;

        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        // S�n�rlar i�inde kalmas�n� sa�la
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimits.x, maxLimits.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimits.y, maxLimits.y);

        // Kameray� yumu�ak �ekilde hedef pozisyona ta��
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
