﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScreen : MonoBehaviour, ClickableObject {

    GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
	}
	
    public void onClick()
    {
        gameController.toComputer();
    }
}