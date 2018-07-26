using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> {

    Actor[] startingActorChoices;
    ActorManager actorManager;
    GameController gameController;
    CameraManager cameraManager;
    Player player;

    public GameObject dialogueBox;
    public GameObject folder;
    public GameObject desktop;
    public GameObject backArrow;

    [SerializeField]
    public GameObject[] actorChoices; 

    private Text dBoxText;

    private bool paused;
    private bool textTyping;
    private int letterCount;
    private string currentText;
    private bool checkStats;

    private int currentLevel;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();

        actorManager = ActorManager.Instance;
        gameController = GameController.Instance;
        cameraManager = CameraManager.Instance;
        player = Player.Instance;
        dBoxText = dialogueBox.GetComponentInChildren<Text>();

        currentLevel = SaveLoad.Instance.getLevel();

        switch(currentLevel)
        {
            case 1:
                startingActorChoices = new Actor[5];
                for (int i = 0; i < 5; i++)
                {
                    startingActorChoices[i] = actorManager.getActor();
                }


                checkStats = false;

                StartCoroutine(levelOne());
                break;
            default:
                gameController.unlockCamera();
                gameController.unlockClicking();
                dialogueBox.SetActive(false);
                backArrow.SetActive(true);
                desktop.GetComponent<ComputerScreen>().unlock();
                break;

        }

        
	}

    public int getLevel()
    {
        return currentLevel;
    }

    public void setActor(Actor actor, int arrayNum)
    {
        startingActorChoices[arrayNum] = actor;
    }

	// Update is called once per frame
	void Update () {
		
        if(Input.GetMouseButtonUp(0))
        {
            if(textTyping)
            {
                textTyping = false;
                dBoxText.text = currentText;
            }
            else if(paused)
            {
                paused = false;
            }

        }
	}

    IEnumerator levelOne()
    {
        /*********LEVEL ONE TEXT************/
        string[] l1text =
        {
            "Hi there and welcome to All Star Talent Agency. I’m your secretary, Donna and I’ll be showing you how we do things around here.",
            "See that folder on your desk? That’s your actor folder. Go ahead and click on it.",
            "This is your actor’s folder. All the actor’s you manage can be accessed here.",
            "Since you’re new here, I’ll give you one of the actors the guy before you managed. Select an actor to add to your folder.",
            "Great! Now, tap on the actors picture to see their stats.",
            "These are your actor’s stats. They show what genre of movies your actors have acted in. Keep these stats in mind when sending your actors out to movies.",
            "When you acquire more actors, you can swipe left or right to view them all.",
            "Click the back arrow to return to your desk",
            "Now, we get to the important part. Click on the computer and let’s get down to business.",
            "Here, where you’ll be sending your actors for auditions. To send an actor, simply drag the actor to the square in the movie, then click the button in the bottom right. Go ahead and try it out ! ",
            "Great job! You did it! You’ll be climbing that managerial ladder in no time.",
            "Certain auditions take time to complete. This means the actor will be unavailable for other auditions until the current audition is completed.",
            "Here’s another thing to remember. Sending actors to movies that don’t fit them could result in failure. If the actor fails too many times, they will leave the agency. If too many actors leave, you and me will be looking for a new audition.",
            "This is your reputation bar. Everytime an actor is successful in a audition/movie you earn reputation points. The higher your reputation, the more actors will want to work with you.",
            "Alright, looks like you’re almost set. Get to level 3 to unlock your phone."
        };


        gameController.lockCamera();
        gameController.lockClicking();
        dBoxText.text = " ";
        dialogueBox.SetActive(true);
        paused = true;

        //Welcome
        StartCoroutine(typingText(l1text[0], dBoxText));
        while (paused)
        {         
            yield return null;
        }

        //look at the folder
        StartCoroutine(typingText(l1text[1], dBoxText));
        paused = true;
        cameraManager.getCam().transform.Rotate(0,45f,0);
        Vector3 camRot = cameraManager.getCam().transform.rotation.eulerAngles;
        cameraManager.getCam().transform.Rotate(0, -45f, 0);
        cameraManager.lerpToLoc(cameraManager.getCam().transform.position, camRot, 1f);
        Behaviour halo = (Behaviour)folder.GetComponent("Halo");
        halo.enabled = true;
        while (paused)
        {
            yield return null;
        }

        //Wait for player to click folder
        dialogueBox.SetActive(false);
        //gameController.unlockCamera();
        gameController.unlockClicking();

        while(!gameController.isAtFile())
        {
            yield return null;
        }
        halo.enabled = false;

        //Explain folder
        dialogueBox.SetActive(true);
        gameController.lockClicking();
        StopCoroutine(typingText(l1text[1], dBoxText));
        StartCoroutine(typingText(l1text[2], dBoxText));
        paused = true;
        while (paused)
        {
            yield return null;
        }


        //Offer actors text
        StopCoroutine(typingText(l1text[2], dBoxText));
        StartCoroutine(typingText(l1text[3], dBoxText));
        paused = true;
        while (paused)
        {
            yield return null;
        }

        //Wait for player to choose an actor
        for (int i = 0; i < 4; i ++)
        {
            actorChoices[i].SetActive(true);
            ActorSelection asel = actorChoices[i].GetComponent<ActorSelection>();
            asel.setActor(startingActorChoices[i]);         
        }
        while (player.getActor(0) == null)
        {
            yield return null;
        }
        for (int i = 0; i < 4; i++)
        {
            actorChoices[i].SetActive(false);
        }

        //Now tap stats
        gameController.unlockClicking();
        StopCoroutine(typingText(l1text[3], dBoxText));
        StartCoroutine(typingText(l1text[4], dBoxText));
        paused = true;
        while (paused)
        {
            yield return null;
        }

        //Wait for stats to be tapped
        checkStats = true;
        dialogueBox.SetActive(false);
        while (checkStats)
        {
            yield return null;
        }
        gameController.lockClicking();

        yield return new WaitForSeconds(1.0f);

        //Explain stats
        dialogueBox.SetActive(true);
        StopCoroutine(typingText(l1text[4], dBoxText));
        StartCoroutine(typingText(l1text[5], dBoxText));
        paused = true;
        while (paused)
        {
            yield return null;
        }

        //Explain swiping
        StopCoroutine(typingText(l1text[5], dBoxText));
        StartCoroutine(typingText(l1text[6], dBoxText));
        paused = true;
        while (paused)
        {
            yield return null;
        }

        //Click back arrow
        backArrow.SetActive(true);
        StopCoroutine(typingText(l1text[6], dBoxText));
        StartCoroutine(typingText(l1text[7], dBoxText));
        while (gameController.isAtFile())
        {
            yield return null;
        }

        //Click the computer
        gameController.unlockClicking();
        desktop.GetComponent<ComputerScreen>().unlock();
        StopCoroutine(typingText(l1text[7], dBoxText));
        StartCoroutine(typingText(l1text[8], dBoxText));
        paused = true;
        while (paused)
        {
            yield return null;
        }
        dialogueBox.SetActive(false);

        //Wait to click computer
        while (!gameController.isAtComputer())
        {
            yield return null;
        }

        //Explain sending actors
        dialogueBox.SetActive(true);
        StopCoroutine(typingText(l1text[8], dBoxText));
        StartCoroutine(typingText(l1text[9], dBoxText));
        paused = true;
        while (paused)
        {
            yield return null;
        }
        dialogueBox.SetActive(false);

        while (gameController.isAtComputer())
        {
            yield return null;
        }

        //Turn off when done

        gameController.unlockCamera();
        dialogueBox.SetActive(false);


        currentLevel = 2;
        
    }



    //Function used to type text one character at a time
    IEnumerator typingText(string text, Text textLoc)
    {
        currentText = text;
        textTyping = true;
        letterCount = 1;
        while(textTyping)
        {
            textLoc.text = text.Substring(0, letterCount);
            if(letterCount < text.Length)
            {
                letterCount++;
            }
            else
            {
                textTyping = false;
            }
            yield return new WaitForSeconds(0.04f);
        }
    }

    public void checkedStats()
    {
        checkStats = false;
    }
}
