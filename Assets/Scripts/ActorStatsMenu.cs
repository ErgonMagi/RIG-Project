using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActorStatsMenu : MonoBehaviour {

    public ActorStatsPage actorStatsPage;
    public ActorIconIpad[] actorIcons;
    public float swipeDist;

    public int centreActorNum;
    private bool swiping = false;
    private List<GameObject> iconGOArray;
    Vector3 mousePosDown, mousePosUp;

	// Use this for initialization
	void Start () {
        centreActorNum = 0;
        iconGOArray = new List<GameObject>();
        foreach(ActorIconIpad icon in actorIcons)
        {
            iconGOArray.Add(icon.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void UpdateMenu()
    {
        List<Actor> actorList = Player.Instance.getActorsList();

        for(int i = 0; i < actorList.Count; i++)
        {
            iconGOArray[i].SetActive(true);
            actorIcons[i].SetActor(actorList[i]);
        }
        for(int i = actorList.Count; i < actorIcons.Length; i++)
        {
            iconGOArray[i].SetActive(false);
        }
    }

    public void SelectActor(ActorIconIpad selectedActoricon)
    {
        actorStatsPage.setActor(selectedActoricon.GetActor());
        foreach(ActorIconIpad icon in actorIcons)
        {
            icon.ToggleBorder(false);
        }
        selectedActoricon.ToggleBorder(true);
    }


}
