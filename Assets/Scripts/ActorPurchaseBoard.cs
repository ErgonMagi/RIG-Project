using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActorPurchaseBoard : MonoBehaviour, IPointerClickHandler {

    public GameObject[] actorPictures;

    public int numPurchasableActors;

	// Use this for initialization
	void Start () {
        numPurchasableActors = 5;
        FillBoard();
    }
	
    public void FillBoard()
    {
        int count = 0;
        for (int i = 0; i < actorPictures.Length; i++)
        {
            if (actorPictures[i].activeSelf)
            {
                if (actorPictures[i].GetComponent<PurchaseActorTicket>().hasActor())
                {
                    count++;
                }
            }
        }
        while(count < numPurchasableActors)
        {
            int randInt = Random.Range(0, actorPictures.Length - 1);
            if (!actorPictures[randInt].GetComponent<PurchaseActorTicket>().hasActor())
            {
                actorPictures[randInt].GetComponent<PurchaseActorTicket>().setActor(ActorManager.Instance.getActor());
            
                count++;
            }
        }
    }

    public void OnPointerClick(PointerEventData pointer)
    {
         GameController.Instance.toActorBoard();   
    }
}
