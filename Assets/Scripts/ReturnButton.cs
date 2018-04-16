using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour, ClickableObject {

    GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        gameController.fromComputer();
    }
}
