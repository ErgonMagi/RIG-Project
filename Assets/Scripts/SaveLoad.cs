using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour {

    private Actor[] actors;
    private int level;

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(gameObject);

        level = 1;
        actors = new Actor[100];

        Load();
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData(FindObjectOfType<Player>().getActorsList(), FindObjectOfType<LevelManager>().getLevel());

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            actors = data.getActors();
            level = data.getLevel();
            file.Close();
        }
    }

    public int getLevel()
    {
        return level;
    }

    public Actor[] getActors()
    {
        return actors;
    }
}

[Serializable]
class PlayerData
{
    private SerializableActor[] actors = new SerializableActor[100];
    private int level;

    public PlayerData(Actor [] actors, int level)
    {
        for(int i = 0; i < actors.Length; i++)
        {
            if(actors[i] != null)
            {
                this.actors[i] = new SerializableActor(actors[i]);
            }
        }
        this.level = level;
    }

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

    public int getLevel()
    {
        return level;
    }
}

[Serializable]
class SerializableActor
{
    private float comedy, action, romance, horror, scifi, other;
    private float baseComedy, baseAction, baseRomance, baseHorror, baseScifi, baseOther;
    private string actorName;
    private float experience;
    private Actor.ActorState actorState;

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

        actorName = actor.getName();
        experience = actor.getExperience();
        actorState = actor.getState();
    }

    public Actor getActor()
    {
        Sprite spriteSearch = Resources.Load<Sprite>("Actor images/" + actorName);

        if (spriteSearch == null)
        {
            spriteSearch = Resources.Load<Sprite>("Actor images/Alicia Vikander");
        }

        return new Actor(comedy, romance, action, horror, scifi, other, actorName, spriteSearch, baseComedy, baseRomance, baseAction, baseHorror, baseScifi, baseOther);
    }
}
