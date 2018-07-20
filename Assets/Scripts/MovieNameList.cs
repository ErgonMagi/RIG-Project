using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieNameList {

    private string[] movieNames =
    {
        "The Titanic",
        "The Village",
        "The Avengers",
        "Rocky",
        "Harry Potter and the Goblet of Fire",
        "Zootopia",
        "A Quiet Place",
        "The Shining",
        "Mamma Mia",
        "Hercules"
    };

    public string getName()
    {
        int movieNum = (int)Random.Range(0, movieNames.Length);
        return movieNames[movieNum];
    }

}
