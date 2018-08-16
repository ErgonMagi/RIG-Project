using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsArrow : MonoBehaviour {

    public bool isRightArrow;
    public NotificationMenu notificationMenu;

	public void Clicked()
    {
        notificationMenu.ChangeMenu(isRightArrow);
    }
}
