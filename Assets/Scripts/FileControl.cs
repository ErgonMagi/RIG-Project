using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileControl : MonoBehaviour, ClickableObject {

	private Animator anim;
    private GameController gameController;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
	}

    public void onClick()
    {
        anim.SetTrigger("File_Clicked");
        gameController.toFile();
    }
}
