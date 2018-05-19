using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileControl : MonoBehaviour, ClickableObject {

	Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}

    public void onClick()
    {
        anim.SetTrigger("File_Clicked");
    }
}
