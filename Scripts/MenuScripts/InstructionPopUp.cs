using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPopUp : MonoBehaviour
{
    public GameObject instructionMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instructionPanelPopUp()
    {
        instructionMenu.SetActive(true);
    }

    public void instructionPanelClose()
    {
        instructionMenu.SetActive(false);
    }
}
