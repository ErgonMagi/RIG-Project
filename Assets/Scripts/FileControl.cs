using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FileControl : MonoBehaviour, IPointerClickHandler {

	private Animator anim;
    private GameController gameController;
    private Collider col;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
        col = this.GetComponent<Collider>();
	}

    private void Update()
    {
        if (!gameController.isAtFile() && col.enabled == false)
        {
            anim.SetTrigger("File_Clicked");
            col.enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData pointer)
    {
        if(!gameController.isAtFile())
        {
            anim.SetTrigger("File_Clicked");
            gameController.toFile();
            col.enabled = false;
        }
    }
}
