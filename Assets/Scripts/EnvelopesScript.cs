using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopesScript : MonoBehaviour, ClickableObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClick()
    {
        NotificationManager.Instance.EnvelopeClicked();
    }
}
