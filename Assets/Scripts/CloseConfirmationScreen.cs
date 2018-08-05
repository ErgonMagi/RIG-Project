using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseConfirmationScreen : MonoBehaviour {


    public ConfirmationPanel confirmationPanel;

    public void CloseScreen()
    {
        confirmationPanel.HideConfirmationScreen();
    }
}
