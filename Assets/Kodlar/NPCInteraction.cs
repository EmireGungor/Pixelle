using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionText; // "E tuþuna basýn" yazýsý

    private void Start()
    {
        interactionText.SetActive(false); // Baþlangýçta gizli olmalý
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu NPC'ye yaklaþýnca
        {
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu uzaklaþýnca
        {
            interactionText.SetActive(false);
        }
    }
}

