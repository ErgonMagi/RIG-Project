using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ActorProfile : MonoBehaviour, ClickableObject {

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

    private bool faceUp;
    private bool rotating;

    private Vector3 faceUpRot;
    private Vector3 faceDownRot;

    private float timeRotating;
    public float rotateTime;

    private float rotateStep;

    int stepcount;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        actor = null;

        faceUp = true;
        rotating = false;

        faceUpRot = new Vector3(-90.0f, 15.0f, -90.0f);
        faceDownRot = new Vector3(90.0f, 15.0f, -90.0f);

       
        stepcount = 0;
    }

    private void Update()
    {
        if(actor == null)
        {
            getActorFromPlayer();
        }

        if(rotating)
        {
            timeRotating += Time.deltaTime;
            //Check if rotating to be faceup
            if(faceUp)
            {
                if(timeRotating >= rotateTime)
                {
                    rotating = false;
                    this.transform.localRotation = Quaternion.Euler(faceDownRot);
                }
                else
                {
                    rotateStep = 180.0f / (rotateTime / Time.deltaTime);
                    this.transform.RotateAround(this.transform.position, this.transform.up, rotateStep);
                }
            }
            else
            {
                if (timeRotating >= rotateTime)
                {
                    rotating = false;
                    this.transform.localRotation = Quaternion.Euler(faceUpRot);
                }
                else
                {
                    stepcount++;
                    rotateStep = 180.0f / (rotateTime / Time.deltaTime);
                    this.transform.RotateAround(this.transform.position, this.transform.up, -rotateStep);
                }
            }
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

    public void onClick()
    {
        timeRotating = 0;
        rotating = true;
        faceUp = !faceUp;
    }
}
