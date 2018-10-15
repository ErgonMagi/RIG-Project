using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class updateManagers : MonoBehaviour {

    public Camera main, comp, file;
    public NotificationBanner notiBanner;
    public NotificationMenu notiMenu;
    public ReturnButton retButton;
    public Image quickNoti;
    public TextMeshProUGUI quicknotitext;
    public AuditionResultNotifications audResNots;
    public MovieResultNotifications movResNot;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        GameController.Instance.UpdateCameras(main, comp, file);
        NotificationManager.Instance.UpdateReferences(notiBanner, notiMenu, retButton, quickNoti, quicknotitext, audResNots, movResNot);
        CameraManager.Instance.UpdateCamera(main);
    }
}
