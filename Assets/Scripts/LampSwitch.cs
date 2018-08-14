using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LampSwitch : MonoBehaviour, IPointerClickHandler{

    public Light lampLight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData pointer)
    {
        lampLight.enabled = !lampLight.enabled;
    }
}
