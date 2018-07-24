using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {

    private ActorManager am;
    private MovieManager mm;
    private bool loading;

    //Hides the loading bar on start
    private void Start()
    {
        this.transform.localScale = new Vector3(0, 1, 1);
        am = FindObjectOfType<ActorManager>();
        mm = FindObjectOfType<MovieManager>();
        loading = false;
    }

    //The loading bar grows as the actors and movies are finished loading
    public void Update()
    {
        int numActors = am.getNumActors();
        int numMovies = mm.getNumMovies();
        if(numActors >5)
        {
            numActors = 5;
        }
        if(numMovies >10)
        {
            numMovies = 10;
        }
        float percentage = ((float)numActors + (float)numMovies) / (15.0f);
        if(percentage > 1)
        {
            percentage = 1;
        }

        this.transform.localScale = new Vector3(percentage, 1, 1);
        this.GetComponentInChildren<Text>().text = (percentage*100).ToString() + "%";
        
        if(numActors >= 5 && numMovies >= 10 && loading)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(1);
            loading = false;
        }
    }

    //Called on click to start loading the game
    public void startLoading()
    {
        loading = true;
    }


}
