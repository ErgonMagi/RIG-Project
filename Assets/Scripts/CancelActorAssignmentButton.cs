using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelActorAssignmentButton : MonoBehaviour {

    bool selected = false;
    public Image imageRenderer;
    public Button button;

    public AuditionScreen auditionScreen;

    private int arrayNum;

    public void Awake()
    { 

        hideButton();
    }

    public void showButton()
    {
        imageRenderer.enabled = true;
        button.enabled = true;
    }

    public void hideButton()
    {
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


