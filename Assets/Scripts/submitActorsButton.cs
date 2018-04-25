using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submitActorsButton : MonoBehaviour, ClickableObject {

    private ScoreManager scoreManager;

    public void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

    }

    public void onClick()
    {

        MovieMenu mm = FindObjectOfType<MovieMenu>();
        mm.submitActors();

        scoreManager.calculateScores();
    }
}
