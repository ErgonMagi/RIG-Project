using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actor {

    public enum ActorState
    {
        available,
        auditioning,
        onMovie,
        training
    }
    /*public Init
        {
        public float com, 

     }*/
    private float comedy, action, romance, horror, scifi, other;
    private float baseComedy, baseAction, baseRomance, baseHorror, baseScifi, baseOther;
    private string actorName;
    private Sprite actorPicture;
    private float experience;
    private ActorState actorState;

    /*public Actor(ActorInit init)
{
    actorState 
}*/
    public Actor(float com, float rom, float act, float hor, float sci, float oth, string name, Sprite picture)
    {
        actorState = ActorState.available;
        comedy = com;
        romance = rom;
        action = act;
        horror = hor;
        scifi = sci;
        other = oth;
        actorName = name;
        baseComedy = com;
        baseRomance = rom;
        baseAction = act;
        baseHorror = hor;
        baseScifi = sci;
        baseOther = oth;
        actorPicture = picture;
    }

    public Actor(float com, float rom, float act, float hor, float sci, float oth, string name, Sprite picture, float basecom, float baserom, float baseact, float basehor, float basesci, float baseoth, float experience, ActorState actorState)
    {
        actorState = ActorState.available;
        comedy = com;
        romance = rom;
        action = act;
        horror = hor;
        scifi = sci;
        other = oth;
        actorName = name;
        baseComedy = basecom;
        baseRomance = baserom;
        baseAction = baseact;
        baseHorror = basehor;
        baseScifi = basesci;
        baseOther = baseoth;
        actorPicture = picture;
        this.experience = experience;
        this.actorState = actorState;
    }

    public void toMovie()
    {
        actorState = ActorState.onMovie;
    }

    public void toTraining()
    {
        actorState = ActorState.training;
    }

    public void toAudition()
    {
        actorState = ActorState.auditioning;
    }

    public void returnhome()
    {
        actorState = ActorState.available;
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

    public float getBaseComedy()
    {
        return baseComedy;
    }

    public float getBaseRomance()
    {
        return baseRomance;
    }

    public float getBaseHorror()
    {
        return baseHorror;
    }

    public float getBaseScifi()
    {
        return baseScifi;
    }

    public float getBaseAction()
    {
        return baseAction;
    }

    public float getBaseOther()
    {
        return baseOther;
    }

    public Sprite getPicture()
    {
        return actorPicture;
    }

    public string getName()
    {
        return actorName;
    }

    public float getExperience()
    {
        return experience;
    }

    public void addExperience(float xp)
    {
        experience += xp;
    }

    public void levelUp()
    {
        comedy += 0.25f * baseComedy;
        romance += 0.25f * baseRomance;
        action += 0.25f * baseAction;
        scifi += 0.25f * baseScifi;
        horror += 0.25f * baseHorror;
        other += 0.25f * baseOther;
    }

    public ActorState getState()
    {
        return actorState;
    }
}

