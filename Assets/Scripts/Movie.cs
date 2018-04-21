using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie
{

    private int comedy, action, romance, horror, scifi, other;
    private string movieName;
    private Sprite moviePicture;

    public Movie(int com, int rom, int act, int hor, int sci, int oth, string name, Sprite picture)
    {
        comedy = com;
        romance = rom;
        action = act;
        horror = hor;
        scifi = sci;
        other = oth;
        movieName = name;
        moviePicture = picture;
    }

    public void setComedy(int com)
    {
        comedy = com;
    }
    public void setRomance(int rom)
    {
        romance = rom;
    }
    public void setAction(int act)
    {
        action = act;
    }
    public void setHorror(int hor)
    {
        horror = hor;
    }

    public void setScifi(int sci)
    {
        scifi = sci;
    }
    public void setOther(int oth)
    {
        other = oth;
    }

    public void setName(string name)
    {
        movieName = name;
    }

    public void setPicture(Sprite picture)
    {
        moviePicture = picture;
    }

    public int getComedy()
    {
        return comedy;
    }

    public int getRomance()
    {
        return romance;
    }

    public int getHorror()
    {
        return horror;
    }

    public int getScifi()
    {
        return scifi;
    }

    public int getAction()
    {
        return action;
    }

    public int getOther()
    {
        return other;
    }

    public Sprite getPicture()
    {
        return moviePicture;
    }

    public string getName()
    {
        return movieName;
    }

}
