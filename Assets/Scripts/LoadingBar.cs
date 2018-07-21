using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {

    private int baseNum = -1;
    private ActorManager am;
    private MovieManager mm;
    private bool loading;

    private void Start()
    {
        this.transform.localScale = new Vector3(0, 1, 1);
        am = FindObjectOfType<ActorManager>();
        mm = FindObjectOfType<MovieManager>();
        loading = false;
    }

    public void Update()
    {
        Debug.Log(mm.getNumMovies());
        float percentage = ((float)am.getNumActors() + (float)mm.getNumMovies()) / (15.0f);
        if(percentage > 1)
        {
            percentage = 1;
        }

        this.transform.localScale = new Vector3(percentage, 1, 1);
        this.GetComponentInChildren<Text>().text = (percentage*100).ToString() + "%";
        
        if(am.getNumActors() >= 5 && mm.getNumMovies() >= 10 && loading)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(1);
            loading = false;
        }
    }

    public void startLoading()
    {
        loading = true;
    }


}
