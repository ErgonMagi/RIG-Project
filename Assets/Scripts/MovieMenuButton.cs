using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieMenuButton : MonoBehaviour, ClickableObject {

    GameController gameController;
    private bool isMenuOpen;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        isMenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        if (isMenuOpen)
        {
            gameController.closeMenu();
            isMenuOpen = false;
        }
        else
        {
            gameController.openMovieMenu();
            isMenuOpen = true;
        }

    }
}
