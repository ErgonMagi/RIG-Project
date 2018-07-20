using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {

    private int baseNum = -1;
    private ActorManager am;
    private bool notLoading;

    private void Start()
    {
        this.transform.localScale = new Vector3(0, 1, 1);
        am = FindObjectOfType<ActorManager>();
        notLoading = true;
    }

    public void Update()
    {
        float percentage = ((float)am.getNumActors() - baseNum) / (5.0f - (float)baseNum);
        if(percentage > 1)
        {
            percentage = 1;
        }
        Debug.Log(percentage);
        if(baseNum != -1)
        {
            this.transform.localScale = new Vector3(percentage, 1, 1);
            this.GetComponentInChildren<Text>().text = (percentage*100).ToString() + "%";
        }
        if(am.getNumActors() >= 5 && notLoading)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(1);
            notLoading = false;
        }
    }

    public void startLoading()
    {
        baseNum = am.getNumActors();
    }


}
