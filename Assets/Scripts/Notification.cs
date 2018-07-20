using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification {

    private string text;

    public Notification(string t)
    {
        text = t;
    }

    public string getText()
    {
        return text;
    }

}
