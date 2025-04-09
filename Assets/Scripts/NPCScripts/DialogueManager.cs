// DialogueManager.cs

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;      // Panel de diálogo
    public TextMeshProUGUI dialogueText;    // Componente de texto para mostrar el diálogo
    private List<string> dialogueLines;     // Líneas de diálogo a mostrar
    private int currentLine = 0;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Inicia el diálogo recibiendo la lista de líneas
    public void StartDialogue(List<string> lines)
    {
        dialogueLines = lines;
        currentLine = 0;
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];
    }

    // Avanza a la siguiente línea o finaliza el diálogo
    public void NextLine()
    {
        currentLine++;
        if (currentLine < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    // Finaliza el diálogo
    public void EndDialogue()
    {
        PlayerController.dialogue = false;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // Avanza el diálogo con la tecla Espacio
        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }
}
