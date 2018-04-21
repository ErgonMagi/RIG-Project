using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieMenu : MonoBehaviour {

    [SerializeField]
    public GameObject[] movieProfiles;

    public float swipeSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void scroll(float scrollLength)
    {
        for (int i = 0; i < movieProfiles.Length; i++)
        {
            movieProfiles[i].transform.localPosition -= new Vector3(scrollLength * swipeSpeed, 0, 0);
            if (movieProfiles[i].transform.localPosition.x >= 11)
            {
                movieProfiles[i].transform.localPosition -= new Vector3(31.2f, 0, 0);
            }
            else if (movieProfiles[i].transform.localPosition.x <= -15)
            {
                movieProfiles[i].transform.localPosition += new Vector3(31.2f, 0, 0);
            }
        }
    }
}
