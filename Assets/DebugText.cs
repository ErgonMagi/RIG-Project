using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour {

	// Use this for initialization
	void Start () {


        this.GetComponent<TextMeshProUGUI>().text = MovieManager.Instance.getMovie().getTitle();
    }
	
	// Update is called once per frame
	void Update () {

        
	}
}
