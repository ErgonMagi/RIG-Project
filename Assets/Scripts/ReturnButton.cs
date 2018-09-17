using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReturnButton : MonoBehaviour, IPointerClickHandler {

    GameController gameController;
    Vector3 startScale;

	// Use this for initialization
	void Start () {
        gameController = GameController.Instance;
        startScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.isAtDesk())
        {
            SetVisbility(false);
        }
        else
        {
            SetVisbility(true);
        }
	}

    public void OnPointerClick(PointerEventData pointer)
    {
        gameController.fromComputer();
    }

    public void SetVisbility(bool vis)
    {
        if(vis)
        {
            this.transform.localScale = startScale;
        }
        else
        {
            this.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
