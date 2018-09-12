using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/************************
 * The computer screen 
 * interactable object
 * *********************/

public class ComputerScreen : MonoBehaviour, IPointerClickHandler {

    GameController gameController;
    public Camera rtCam;
    public Shader shader;

    private bool unlocked = false;

	// Use this for initialization
	void Start () {
        rtCam.enabled = true;
        //rtCam.aspect = 1.78f;
        gameController = GameController.Instance;
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 1000);
        rt.Create();
        rtCam.targetTexture = rt;
        GetComponent<Renderer>().material.mainTexture = rt;
        GetComponent<Renderer>().material.shader = shader;

    }
	
    public void OnPointerClick(PointerEventData pointer)
    {
        if(gameController.isAtDesk() && unlocked)
        {
            gameController.toComputer();
        }
    }

    public void unlock()
    {
        unlocked = true;
    }
}
