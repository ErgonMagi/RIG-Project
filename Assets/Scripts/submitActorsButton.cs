using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submitActorsButton : MonoBehaviour {

    public ConfirmationPanel confirmationPanel;
    public AuditionScreen auditionScreen;

    //Submits all actors to their auditions on click (Should be changed to show confirm screen).
    public void onClick()
    {
        Debug.Log("button clicked");
        if (confirmationPanel.IsVisble())
        {
            auditionScreen.submitActors();
        }
        else
        {
            confirmationPanel.ShowConfirmationScreen();
        }
    }
}
