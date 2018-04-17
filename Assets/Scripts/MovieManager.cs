using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.General;
using System.IO;

public class MovieManager : MonoBehaviour {

    TMDbClient client;
    WWW www;

	// Use this for initialization
	void Start () {
        client = new TMDbClient("e2ffb845e5d5fca810eaf5054914f41b");

        ImagesWithId img = client.GetMovieImagesAsync(100).Result;

        client.GetConfig();

        string path = client.GetImageUrl("w92", img.Posters[0].FilePath).ToString();

        www = new WWW(path);

        StreamWriter sw = new StreamWriter("url.txt");

        sw.Write(path);

        sw.Close();
    }
	
	// Update is called once per frame
	void Update () {
		

        if(www.isDone)
        {
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));

            this.GetComponent<SpriteRenderer>().sprite = sprite;

            /*while(this.GetComponent<SpriteRenderer>().sprite.bounds.size.y > 1)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x*0.9f, this.transform.localScale.y * 0.9f, 1.0f);
            }*/



        }
	}
}
