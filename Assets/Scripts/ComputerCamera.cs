using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().enabled = true;
        GetComponent<Camera>().aspect = 1.78f;
        GetComponent<Camera>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
