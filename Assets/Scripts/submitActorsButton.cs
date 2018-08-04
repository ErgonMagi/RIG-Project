using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submitActorsButton : MonoBehaviour {

    //Submits all actors to their auditions on click (Should be changed to show confirm screen).
    public void onClick()
    {
        AuditionScreen auditionScreen = FindObjectOfType<AuditionScreen>();
        auditionScreen.submitActors();
    }
}
