using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseConfirmationScreen : MonoBehaviour {


    public AuditionScreen auditionScreen;

    public void CloseScreen()
    {
        auditionScreen.HideConfirmationScreen();
    }
}
    