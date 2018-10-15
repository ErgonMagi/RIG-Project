using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCameras : MonoBehaviour {

    public Camera main, comp, folder;

	// Use this for initialization
	void Start () {
        GameController.Instance.UpdateCameras(main, comp, folder);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
