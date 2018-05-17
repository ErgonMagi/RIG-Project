using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitch : MonoBehaviour, ClickableObject{

    public Light lampLight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        lampLight.enabled = !lampLight.enabled;
    }
}
