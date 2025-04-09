// NPCSystem.cs

using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    public DialogueManager dialogueManager;  // Referencia al DialogueManager
    public List<string> dialogueLines;         // Líneas de diálogo propias de este NPC
    public GameObject dialoguePrompt;          // Indicador de interacción
    public GameObject dialoguePrompt2;          // Indicador de interacción


    private bool playerInRange = false;

    void Update()
    {
        // Inicia el diálogo al pulsar F cuando el jugador está cerca
        if (playerInRange && Input.GetMouseButtonDown(0) && !PlayerController.dialogue)
        {
            PlayerController.dialogue = true;
            dialoguePrompt.SetActive(false);
            dialoguePrompt2.SetActive(false);
            dialogueManager.StartDialogue(dialogueLines);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.name == "Player")
        {
            playerInRange = true;
            dialoguePrompt.SetActive(true);
            dialoguePrompt2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
            playerInRange = false;
            dialoguePrompt.SetActive(false);
            dialoguePrompt2.SetActive(false);
        
    }
}
