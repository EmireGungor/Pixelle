using System.Collections;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    private LightManager lightManager; // LightManager referansý

    private void Start()
    {
        lightManager = FindObjectOfType<LightManager>(); // LightManager'ý bul
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                EndDialogue();
            }
            else
            {
                StartDialogue();
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(Typing());

        if (lightManager != null)
        {
            lightManager.StartTalking(); // Iþýk azalmayý durdur
        }
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

        if (lightManager != null)
        {
            lightManager.StopTalking(); // Iþýk azalmaya devam etsin
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            EndDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            EndDialogue();
        }
    }
}
