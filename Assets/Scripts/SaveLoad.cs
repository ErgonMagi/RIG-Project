using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : Singleton<SaveLoad> {

    private Actor[] actors;
    private int level;
    private int money;
    private DateTime prevTime;

	// Use this for initialization
	protected override void Awake () {

        base.Awake();

        level = 1;
        actors = new Actor[100];
        prevTime = DateTime.Now;
        money = 1000;
        Load();
	}

    //Converts all relevants data into a serializable form and saves it to playerInfo.dat
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData(Player.Instance.getActorsList().ToArray(), LevelManager.Instance.getLevel(), IncomeManager.Instance.getPrevIncomeTime(), Player.Instance.getMoney());

        bf.Serialize(file, data);
        file.Close();
    }

    private List<Actor> convertArrayToList(Actor[] actors)
    {
        List<Actor> Temp = new List<Actor>();

        for(int i = 0; i < actors.Length; i++)
        {
            if (actors[i] != null)
            {
                Temp.Add(actors[i]);
            }
        }
        return Temp;
    }

    //Loads all data from playerInfo.dat and stores it in the SaveLoad object
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            actors = data.getActors();
            level = data.getLevel();
            money = data.getMoney();
            prevTime = data.getPrevIncomeTime();
            file.Close();
        }
    }

    //Returns the loaded level
    public int getLevel()
    {
        return level;
    }
    
    //Returns the loaded actors
    public List<Actor> getActors()
    {
        return convertArrayToList(actors);
    }

    public DateTime getPrevIncomeTime()
    {
        return prevTime;
    }

    public int getMoney()
    {
        return money;
    }
}

//A serializable object to contain all the playerdata for saving/loading
[Serializable]
class PlayerData
{
    private SerializableActor[] actors = new SerializableActor[100];
    private int level;
    private System.DateTime prevTime;
    private int money;

    //Constructor
    public PlayerData(Actor [] actors, int level, System.DateTime prevIncTime, int mon)
    {
        for(int i = 0; i < actors.Length; i++)
        {
            if(actors[i] != null)
            {
                this.actors[i] = new SerializableActor(actors[i]);
            }
        }
        this.level = level;
        this.prevTime = prevIncTime;
        money = mon;
    }

    //Returns the actors from the playerData (Used in loading)
    public Actor[] getActors()
    {
        Actor[] actors = new Actor[100];

        for (int i = 0; i < actors.Length; i++)
        {
            if (this.actors[i] != null)
            {
                actors[i] = this.actors[i].getActor();
            }
        }
        return actors;
    }

    public System.DateTime getPrevIncomeTime()
    {
        return prevTime;
    }
    
    //Returns the level from the playerData (Used in loading)
    public int getLevel()
    {
        return level;
    }

    public int getMoney()
    {
        return money;
    }
}

//A serializable form of the Actor class where the sprite is left out.
[Serializable]
class SerializableActor
{
    private float comedy, action, romance, horror, scifi, other;
    private float baseComedy, baseAction, baseRomance, baseHorror, baseScifi, baseOther;
    private string actorName;
    private float experience;
    private Actor.ActorState actorState;
    private string[] moviesStarredIn;
    private Actor.ActorState state;
    private int incomeValue;

    //Constructor to convert an actor to a serializable actor
    public SerializableActor(Actor actor)
    {
        comedy = actor.getComedy();
        action = actor.getAction();
        romance = actor.getRomance();
        horror = actor.getRomance();
        scifi = actor.getScifi();
        other = actor.getOther();
        baseComedy = actor.getBaseComedy();
        baseAction = actor.getBaseAction();
        baseRomance = actor.getBaseRomance();
        baseHorror = actor.getBaseHorror();
        baseScifi = actor.getBaseScifi();
        baseOther = actor.getBaseOther();
        state = actor.getState();
        incomeValue = actor.getIncomeValue();

        actorName = actor.getName();
        experience = actor.getExperience();
        actorState = actor.getState();
        moviesStarredIn = actor.getMoviesStarredIn();
    }

    //Returns the serializable actor as an actor.
    public Actor getActor()
    {
        Byte[] imgData = File.ReadAllBytes(Application.persistentDataPath + "/actorImages/" + actorName + ".png");
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imgData);
        Sprite spriteSearch = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        if (spriteSearch == null)
        {
            spriteSearch = Resources.Load<Sprite>("Actor images/Alicia Vikander");
        }

        Actor.Init init;
        init.com = comedy;
        init.rom = romance;
        init.act = action;
        init.hor = horror;
        init.sci = scifi;
        init.other = other;
        init.name = actorName;
        init.pic = spriteSearch;
        init.moviesActorIn = moviesStarredIn;
        init.baseCom = baseComedy;
        init.baseRom = baseRomance;
        init.baseHor = baseHorror;
        init.baseAct = baseAction;
        init.baseSci = baseScifi;
        init.baseOth = baseOther;
        init.exp = experience;
        init.state = actorState;
        init.incomeVal = incomeValue;

        return new Actor(init);
    }
}
