using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************
 * The computer screen 
 * interactable object
 * *********************/

public class ComputerScreen : MonoBehaviour, ClickableObject {

    GameController gameController;
    public Camera rtCam;
    public Shader shader;

	// Use this for initialization
	void Start () {
        rtCam.enabled = true;
        rtCam.aspect = 1.78f;
        gameController = FindObjectOfType<GameController>();
        RenderTexture rt = new RenderTexture(1024, 1024, 1000);
        rt.Create();
        rtCam.targetTexture = rt;
        GetComponent<Renderer>().material.mainTexture = rt;
        GetComponent<Renderer>().material.shader = shader;


    }
	
    public void onClick()
    {
        if(gameController.isAtDesk())
        {
            gameController.toComputer();
        }
    }
}
