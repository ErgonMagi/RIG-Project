using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelActorAssignmentButton : MonoBehaviour {

    bool selected = false;
    public AuditionSlot auditionSlot;
    private Image imageRenderer;
    private Button button;

    public void Start()
    { 
        imageRenderer = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    /*public void Update()
    {
        if (auditionSlot.GetComponent<AuditionSlot>().getActor() != null)
        {
            imageRenderer.enabled = true;
            button.enabled = true;
        }
        else
        {
            imageRenderer.enabled = false;
            button.enabled = false;
        }
    }

    public void Clicked()
    {
        GameObject temp = auditionSlot.getActor();
        auditionSlot.resetActor();
    }*/
}


