using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMenuButton : MonoBehaviour, ClickableObject {

    GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        Debug.Log("clicked");
        gameController.openStatsMenu();
    }
}
