using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Audition slots are the gameobjects that hold potential auditions for the player.

public class AuditionSlot : MonoBehaviour {

    public Image movieImageRenderer;
    public Image actorImageRenderer;
    public CancelActorAssignmentButton cancelButton;
    private AuditionScreen.Audition audition;
    public float rotationAngle;
    public Transform zeroPosHolder;

    public float zeroPos;
    public float xPos;
    private float zeroZPos;
    private Transform myTransform;
    public float spacing;
    public float anglePercent;
    public float angle;

    private void Start()
    {
        myTransform = transform;
        zeroZPos = myTransform.position.z;
        spacing = 10;
        zeroPos = zeroPosHolder.position.x;
    }

    private void Update()
    {
        xPos = myTransform.position.x;
        anglePercent = (zeroPos - myTransform.position.x) / spacing;

        angle = anglePercent * rotationAngle;

        if(Mathf.Abs(zeroPos - myTransform.position.x) < 6 * spacing)
        {
            if (Mathf.Abs(angle) < 30)
            {
                myTransform.localPosition = new Vector3(myTransform.localPosition.x, myTransform.localPosition.y, Mathf.Pow(anglePercent, 2) * rotationAngle/18f);
                myTransform.rotation = Quaternion.Euler(new Vector3(myTransform.rotation.x, angle, myTransform.rotation.z));
            }
        }     
    }

    public void setZeroAndSpacing(float zeroPos, float spacing)
    {
        this.zeroPos = zeroPos;
        this.spacing = spacing;
    }

    public void setAudition(AuditionScreen.Audition a)
    {
        audition = a;
        movieImageRenderer.sprite = a.movie.getPicture();
        if(audition.actor != null)
        {
            actorImageRenderer.enabled = true;
            actorImageRenderer.sprite = audition.actor.getPicture();
            cancelButton.showButton();

        }
        else
        {
            actorImageRenderer.enabled = false;
            cancelButton.hideButton();
        }
    }

    public void setArrayNum(int i)
    {
        cancelButton.setArrayNum(i);
    }


    public bool hasMovie()
    {
        return audition.movie != null;
    }
    
    public void Empty()
    {
        audition = new AuditionScreen.Audition();
        cancelButton.hideButton();
    }

    [ContextMenu("Print pos")]
    public void printpos()
    {
        Debug.Log(this.transform.position.ToString("F4"));
    }

}
