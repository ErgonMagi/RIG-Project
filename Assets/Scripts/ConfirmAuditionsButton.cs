using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAuditionsButton : MonoBehaviour {

    public AuditionScreen auditionScreen;

    public void onClick()
    {
        auditionScreen.submitActors();
    }
}
