using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ActorProfile : MonoBehaviour {

    public GameObject comedyBar, actionBar, romanceBar, horrorBar, scifiBar, otherBar;
    public Text comedyVal, romanceVal, actionVal, horrorVal, scifiVal, otherVal;
    public Text actorNameText;
    public SpriteRenderer actorPictureSprite;
    public int actorNum;

    public float comedy, action, romance, horror, scifi, other;
    private string actorName;
    private Sprite actorPicture;
    private Actor actor;
    private Player player;
    private List<string> actorNames;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        actor = null;

        string filePath = Path.GetFullPath("Assets/ActorNames.txt");
        actorNames = new List<string>();

        StreamReader reader = new StreamReader(filePath);

        while (!reader.EndOfStream)
        {
            actorNames.Add(reader.ReadLine());
        }

        reader.Close();

        actorNameText.text = actorNames[0];
    }

    private void Update()
    {
        if(actor == null)
        {
            getActorFromPlayer();
        }
    }

    public Actor getActor()
    {
        return actor;
    }

    public void getActorFromPlayer()
    {
        Actor temp = player.getActor(actorNum);
        if(temp != null)
            setActor(temp);
    }

    public void setActor(Actor actor)
    {
        comedy = actor.getComedy();
        romance = actor.getRomance();
        action = actor.getAction();
        horror = actor.getHorror();
        scifi = actor.getScifi();
        other = actor.getOther();
        actorName = actor.getName();
        actorPicture = actor.getPicture();
        this.actor = actor;

        updateProfile();
    }

    private void updateProfile()
    {
        comedyBar.transform.localScale = new Vector3((int)comedy / 30f, comedyBar.transform.localScale.y,  comedyBar.transform.localScale.z);
        actionBar.transform.localScale = new Vector3((int)action / 30f, actionBar.transform.localScale.y,  actionBar.transform.localScale.z);
        romanceBar.transform.localScale = new Vector3((int)romance / 30f, romanceBar.transform.localScale.y,  romanceBar.transform.localScale.z);
        horrorBar.transform.localScale = new Vector3((int)horror / 30f, horrorBar.transform.localScale.y,  horrorBar.transform.localScale.z);
        scifiBar.transform.localScale = new Vector3((int)scifi / 30f, scifiBar.transform.localScale.y,  scifiBar.transform.localScale.z);
        otherBar.transform.localScale = new Vector3((int)other / 30f, otherBar.transform.localScale.y,  otherBar.transform.localScale.z);
        actorNameText.text = actorName;
        comedyVal.text = ((int)comedy).ToString();
        romanceVal.text = ((int)romance).ToString();
        horrorVal.text = ((int)horror).ToString();
        scifiVal.text = ((int)scifi).ToString();
        otherVal.text = ((int)other).ToString();
        actionVal.text = ((int)action).ToString();
        actorPictureSprite.sprite = actorPicture;
    }
}
