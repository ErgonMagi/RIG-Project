using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStatsMenu : MonoBehaviour {

    [SerializeField]
    public GameObject[] actorPages;

    public float swipeSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void scroll(float scrollLength)
    {
        for(int i = 0; i < actorPages.Length; i++)
        {
            actorPages[i].transform.localPosition -= new Vector3(scrollLength*swipeSpeed, 0, 0);
            if(actorPages[i].transform.localPosition.x >= 2.1)
            {
                actorPages[i].transform.localPosition -= new Vector3(5f, 0, 0);
            }
            else if(actorPages[i].transform.localPosition.x <= -2.1)
            {
                actorPages[i].transform.localPosition += new Vector3(5f, 0, 0);
            }
        }
    }
}
