using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackground : MonoBehaviour {

    public float scrollSpeed;

    private RectTransform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        myTransform.position -= new Vector3(scrollSpeed, 0) * Time.deltaTime;
        if(myTransform.position.x < -934f)
        {
            myTransform.position += new Vector3(2 * myTransform.rect.width, 0);
        }
	}
}
