using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PurchaseActorTicket : MonoBehaviour, IPointerClickHandler {

    private Actor actor = null;
    private Vector3 rotation;
    private int price;
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

    public void OnPointerClick(PointerEventData pointer)
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
        int price = Random.Range(50, 200);
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

    public int getPrice()
    {
        return price;
    }

    private void UpdateData()
    {
        if(actor != null)
        {
            SetVisbiilty(true);
            this.transform.rotation = Quaternion.Euler(rotation);
            actorSprite.GetComponent<SpriteRenderer>().sprite = actor.getPicture();
            actorName.GetComponent<TextMeshPro>().text = actor.getName();
            actorPrice.GetComponent<TextMeshPro>().text = "$" + price.ToString();
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
