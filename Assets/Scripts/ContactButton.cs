using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContactButton : MonoBehaviour {

    TextMeshPro tmpro;

    bool selected = false;

    public void Start()
    {
        tmpro = this.GetComponent<TextMeshPro>();
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
