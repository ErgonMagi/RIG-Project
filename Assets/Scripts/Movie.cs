using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie
{

    private int comedy, action, romance, horror, scifi, other;
    private string title;
    private Sprite moviePicture;
    private int movieCoinReward;
    private int auditionPrice;
    private int auditionRepReward;
    private int movieXPReward;

    public struct init
    {
        public int comedy;
        public int romance;
        public int action;
        public int horror;
        public int scifi;
        public int other;
        public string title;
        public Sprite picture;
        public int coinReward;
        public int price;
        public int repReward;
        public int xpReward;
    }


    public Movie(init i)
    {
        comedy = i.comedy;
        romance = i.romance;
        action = i.action;
        horror = i.horror;
        scifi = i.scifi;
        other = i.other;
        title = i.title;
        moviePicture = i.picture;

        movieXPReward = i.xpReward;
        movieCoinReward = i.coinReward;
        auditionRepReward = i.repReward;
        auditionPrice = i.price;
    }

    public int getMovieXPReward()
    {
        return movieXPReward;
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

    public void setTitle(string title)
    {
        this.title = title;
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

    public string getTitle()
    {
        return title;
    }

    public int getMovieCoinReward()
    {
        return movieCoinReward;
    }

    public int getAuditionPrice()
    {
        return auditionPrice;
    }

    public int getAuditionRepReward()
    {
        return auditionRepReward;
    }

}
