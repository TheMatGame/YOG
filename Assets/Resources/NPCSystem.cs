using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSystem : MonoBehaviour
{
    bool player_detection = false; 

    public GameObject d_template;

    public GameObject canva;


    // Update is called once per frame
    void Update()
    {
        
        if(player_detection && Input.GetKeyDown(KeyCode.F) && !PlayerController.dialogue)
        {
            canva.SetActive(true);
            print("Dialogue Started!");
            PlayerController.dialogue = true;
            NewDialogue("Welcome to our game!");
            NewDialogue("Vota al PSOE");
            NewDialogue("Illojuan masón");
            canva.transform.GetChild(1).gameObject.SetActive(true);     
        }
    }

    void NewDialogue(string text)
    {
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            player_detection = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
       
            player_detection = false;
        

    }

}
