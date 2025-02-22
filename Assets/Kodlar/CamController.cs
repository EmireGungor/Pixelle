using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothTime = 0.2f; // Kameranýn yumuþak hareket süresi
    public Vector2 minLimits; // Kamera sýnýrlarý (Sol Alt)
    public Vector2 maxLimits; // Kamera sýnýrlarý (Sað Üst)

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (playerTransform == null)
            return;

        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        // Sýnýrlar içinde kalmasýný saðla
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimits.x, maxLimits.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimits.y, maxLimits.y);

        // Kamerayý yumuþak þekilde hedef pozisyona taþý
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
