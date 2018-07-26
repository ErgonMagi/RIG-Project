using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseActorTicket : MonoBehaviour, ClickableObject {

    private Actor actor = null;
    private Vector3 rotation;
    public GameObject actorSprite;
    public GameObject actorName;
    public GameObject actorPrice;
    public GameObject CameraPos;

    private ActorPurchaseBoard actorPurchaseBoard;

    public void Start()
    {
        UpdateData();
        actorPurchaseBoard = FindObjectOfType<ActorPurchaseBoard>();
    }

    public void onClick()
    {
        if (GameController.Instance.isAtBoard() && this.GetComponent<Renderer>().enabled)
        {
            GameController.Instance.toActorTicket(CameraPos);
        }
    }

    public void setActor(Actor a)
    {
        actor = a;
        float randomAngle = Random.Range(-15, 0);
        rotation = new Vector3(0, 0, randomAngle);

        UpdateData();
    }

    public Actor getActor()
    {
        Actor temp = actor;
        actor = null;
        UpdateData();
        actorPurchaseBoard.FillBoard();
        return temp;
        
    }

    private void UpdateData()
    {
        if(actor != null)
        {
            SetVisbiilty(true);
            this.transform.rotation = Quaternion.Euler(rotation);
            actorSprite.GetComponent<SpriteRenderer>().sprite = actor.getPicture();
            actorName.GetComponent<TextMeshPro>().text = actor.getName();
            actorPrice.GetComponent<TextMeshPro>().text = "$100";
        }
        else
        {
            SetVisbiilty(false);
        }

    }

    public bool hasActor()
    {
        return actor != null;
    }

    private void SetVisbiilty(bool visbility)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            r.enabled = visbility;
        }
    }

    

}
