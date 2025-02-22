using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionText; // "E tu�una bas�n" yaz�s�

    private void Start()
    {
        interactionText.SetActive(false); // Ba�lang��ta gizli olmal�
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu NPC'ye yakla��nca
        {
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu uzakla��nca
        {
            interactionText.SetActive(false);
        }
    }
}

