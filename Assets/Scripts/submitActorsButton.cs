using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submitActorsButton : MonoBehaviour, ClickableObject {

    //Submits all actors to their auditions on click (Should be changed to show confirm screen).
    public void onClick()
    {
        MovieMenu mm = FindObjectOfType<MovieMenu>();
        mm.submitActors();
    }
}
