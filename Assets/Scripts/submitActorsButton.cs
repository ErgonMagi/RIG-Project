using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submitActorsButton : MonoBehaviour, ClickableObject {

    public void onClick()
    {
        MovieMenu mm = FindObjectOfType<MovieMenu>();
        mm.submitActors();
    }
}
