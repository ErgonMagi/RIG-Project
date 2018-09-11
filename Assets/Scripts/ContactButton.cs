using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ContactButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler {

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

    public void OnPointerEnter(PointerEventData pointer)
    {
        tmpro.fontStyle = TMPro.FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        tmpro.fontStyle = TMPro.FontStyles.Normal;
        selected = false;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        selected = true;
    }

    public void OnPointerUp(PointerEventData pointer)
    {
        if(selected)
        {
            PurchaseActorTicket pat = this.GetComponentInParent<PurchaseActorTicket>();
            Debug.Log("Price for actor is: $" + pat.getPrice());

            if(Player.Instance.spendMoney(pat.getPrice()))
            {
                Actor purchasedActor = pat.getActor();
                Player.Instance.addActor(purchasedActor);
                GameController.Instance.fromComputer();
                NotificationManager.Instance.QuickNotification("Actor purchased");
            }
        }
    }
}
