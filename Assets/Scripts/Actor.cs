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

    public struct Init
    {
        public float com, act, rom, hor, sci, other;
        public float baseCom, baseAct, baseRom, baseHor, baseSci, baseOth;
        public string name;
        public Sprite pic;
        public string[] moviesActorIn;
        public ActorState state;
        public float exp;
        public int level;
        public int incomeVal;
    };
     


    private float comedy, action, romance, horror, scifi, other;
    private float baseComedy, baseAction, baseRomance, baseHorror, baseScifi, baseOther;
    private string actorName;
    private Sprite actorPicture;
    private float experience;
    private float maxExperience;
    private ActorState actorState;
    private string[] moviesStarredIn;
    private int incomeValue;
    private int level;


    public Actor(Init i)
    {
        actorState = i.state;
        comedy = i.com;
        romance = i.rom;
        action = i.act;
        horror = i.hor;
        scifi = i.sci;
        other = i.other;
        actorName = i.name;
        if(i.baseCom == 0 && i.baseAct == 0 & i.baseHor == 0 & i.baseOth == 0 & i.baseRom == 0 & i.baseSci == 0)
        {
            baseComedy = comedy;
            baseRomance = romance;
            baseAction = action;
            baseHorror = horror;
            baseScifi = scifi;
            baseOther = other;
        }
        else
        {
            baseComedy = i.baseCom;
            baseRomance = i.baseRom;
            baseAction = i.baseAct;
            baseHorror = i.baseHor;
            baseScifi = i.baseSci;
            baseOther = i.baseOth;
        }
        
        actorPicture = i.pic;
        moviesStarredIn = i.moviesActorIn;
        incomeValue = i.incomeVal;
        experience = i.exp;
        maxExperience = 1000;
        this.level = i.level;
    }

    public float getMaxExperience()
    {
        return maxExperience;
    }

    public int getIncomeValue()
    {
        return incomeValue;
    }

    public int getLevel()
    {
        return level;
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
        if(experience > maxExperience)
        {
            level++;
            experience -= maxExperience;
            maxExperience *= 2;
        }
    }

    public string [] getMoviesStarredIn()
    {
        return moviesStarredIn;
    }

    public bool hasStarredIn(string movieName)
    {
        for(int i = 0; i < moviesStarredIn.Length; i++)
        {
            if(movieName == moviesStarredIn[i])
            {
                return true;
            }
        }
        return false;
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

