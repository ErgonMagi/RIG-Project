using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor {

    private float comedy, action, romance, horror, scifi, other;
    private string actorName;
    private Sprite actorPicture;

    public Actor(float com, float rom, float act, float hor, float sci, float oth, string name, Sprite picture)
    {
        comedy = com;
        romance = rom;
        action = act;
        horror = hor;
        scifi = sci;
        other = oth;
        actorName = name;
        actorPicture = picture;
    }

    public void setComedy(float com)
    {
        comedy = com;
    }
    public void setRomance(float rom)
    {
        romance = rom;
    }
    public void setAction(float act)
    {
        action = act;
    }
    public void setHorror(float hor)
    {
        horror = hor;
    }

    public void setScifi(float sci)
    {
        scifi = sci;
    }
    public void setOther(float oth)
    {
        other = oth;
    }

    public void setName(string name)
    {
        actorName = name;
    }

    public void setPicture(Sprite picture)
    {
        actorPicture = picture;
    }

    public float getComedy()
    {
        return comedy;
    }

    public float getRomance()
    {
        return romance;
    }

    public float getHorror()
    {
        return horror;
    }

    public float getScifi()
    {
        return scifi;
    }

    public float getAction()
    {
        return action;
    }

    public float getOther()
    {
        return other;
    }

    public Sprite getPicture()
    {
        return actorPicture;
    }

    public string getName()
    {
        return actorName;
    }

}
