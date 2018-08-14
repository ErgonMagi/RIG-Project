using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReturnButton : MonoBehaviour, IPointerClickHandler {

    GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = GameController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.isAtDesk())
        {
            setVisbility(false);
        }
        else
        {
            setVisbility(true);
        }
	}

    public void OnPointerClick(PointerEventData pointer)
    {
        gameController.fromComputer();
    }

    private void setVisbility(bool vis)
    {
        if(vis)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
