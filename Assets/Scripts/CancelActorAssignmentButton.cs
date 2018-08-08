using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelActorAssignmentButton : MonoBehaviour {

    bool selected = false;
    private Image imageRenderer;
    private Button button;

    public AuditionScreen auditionScreen;

    private int arrayNum;

    public void Start()
    { 
        imageRenderer = GetComponent<Image>();
        button = GetComponent<Button>();

        hideButton();
    }

    public void showButton()
    {
        imageRenderer.enabled = true;
        button.enabled = true;
    }

    public void hideButton()
    {
        imageRenderer.enabled = false;
        button.enabled = false;
    }

    public void setArrayNum(int i)
    {
        arrayNum = i;
    }

    public void Clicked()
    {
        auditionScreen.UnassignActor(arrayNum);
    }
}


