using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContactButton : MonoBehaviour {

    TextMeshPro tmpro;
    Collider2D col;

    bool selected = false;

    public void Start()
    {
        tmpro = this.GetComponent<TextMeshPro>();
        col = this.GetComponent<Collider2D>();
    }

    public void Update()
    {
        if(GameController.Instance.isAtTicket())
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }

    public void OnMouseOver()
    {
        tmpro.fontStyle = TMPro.FontStyles.Bold;
    }

    public void OnMouseExit()
    {
        tmpro.fontStyle = TMPro.FontStyles.Normal;
        selected = false;
    }

    public void OnMouseDown()
    {
        selected = true;
    }

    public void OnMouseUp()
    {
        if(selected)
        {
            GameController.Instance.fromComputer();
            Actor purchasedActor = this.GetComponentInParent<PurchaseActorTicket>().getActor();
            Player.Instance.addActor(purchasedActor);
            
        }
    }
}
