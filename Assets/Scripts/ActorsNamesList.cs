using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorsNamesList {

    private string[] actorNames =
    {
        "Alicia Vikander",
        "Scarlett Johansson",
        "Henry Cavill",
        "Daniel Craig",
        "John Goodman",
        "Rowan Atkinson",
        "Tom Hiddleston",
        "Benedict Cumberbatch",
        "Hugh Jackman",
        "Tom Holland",
        "Leonardo DiCaprio",
        "Jonah Hill",
        "Andy Samberg",
        "Eddie Murphy",
        "Meryl Streep",
        "Pierce Brosnan",
        "Terry Crews",
        "Zac Efron",
        "Chris Hemsworth",
        "Ryan Reynolds",
        "Jack Nicholson",
        "Eddie Redmayne"
    };

	public string getName()
    {
        int actorNum = (int)Random.Range(0, actorNames.Length);
        return actorNames[actorNum];
    }
}
