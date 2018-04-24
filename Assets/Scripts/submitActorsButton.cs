using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submitActorsButton : MonoBehaviour, ClickableObject {

	public void onClick()
    {
        Debug.Log("clicked");

        MovieMenu mm = FindObjectOfType<MovieMenu>();
        mm.submitActors();

        ScoreManager.calculateScores();
    }
}
