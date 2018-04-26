using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    public Text nameText, successText, movieText;
    public SpriteRenderer actorPicture;
    public SpriteRenderer moviePicture;

    private Vector3 startPos, hidePos;
    private float t = 0;
    private GameController gc;

    private List<Tuple<Actor, Movie, bool>> auditionsList;

	// Use this for initialization
	void Start () {
        auditionsList = new List<Tuple<Actor, Movie, bool>>();
        startPos = this.transform.position;
        hidePos = startPos + new Vector3(0, 100, 0);
        this.transform.position = hidePos;
        gc = FindObjectOfType<GameController>();
	}


    public void auditionPassed(Tuple<Actor, Movie> pair)
    {
        auditionsList.Add(new Tuple<Actor, Movie, bool>(pair.Item1, pair.Item2, true));
    }

    public void auditionFailed(Tuple<Actor, Movie> pair)
    {
        auditionsList.Add(new Tuple<Actor, Movie, bool>(pair.Item1, pair.Item2, false));
    }

    public void display()
    {
        StartCoroutine(scoreboardLoop());
    }

    IEnumerator scoreboardLoop()
    {
        for(int i = 0; i < auditionsList.Count; i++)
        {
            this.transform.position = startPos;
            actorPicture.sprite = auditionsList[i].Item1.getPicture();
            moviePicture.sprite = auditionsList[i].Item2.getPicture();
            if(auditionsList[i].Item1.getPicture() == null)
            {
                Debug.Log("null");
            }
            nameText.text = auditionsList[i].Item1.getName() + " was ";
            if (auditionsList[i].Item3)
            {
                successText.color = Color.green;
                successText.text = "succesful";
            }
            else
            {
                successText.color = Color.red;
                successText.text = "unsuccesful";
            }
            movieText.text = "in their audition for a role in " + auditionsList[i].Item2.getTitle();
            yield return new WaitForSeconds(2.5f);
        }
        this.transform.position = hidePos;
        auditionsList = new List<Tuple<Actor, Movie, bool>>();
        gc.newWeek();

    }
}
