using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IpadControl : MonoBehaviour, IPointerClickHandler {

	private Animator anim;
    private GameController gameController;
    private Collider col;
    public Camera rtCam;
    public Shader shader;

	// Use this for initialization
	void Start () 
	{
        gameController = GameController.Instance;
        col = this.GetComponent<Collider>();
        rtCam.enabled = true;
        //rtCam.aspect = 1.78f;
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 1000);
        rt.Create();
        rtCam.targetTexture = rt;
        GetComponent<Renderer>().material.mainTexture = rt;
        GetComponent<Renderer>().material.shader = shader;
    }

    public void OnPointerClick(PointerEventData pointer)
    {
        gameController.toIpad();
    }
}
