using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour {

    private bool isLoading = false;

	public void startGame()
    {
        if (!isLoading)
        {
            isLoading = true;
            FindObjectOfType<LoadingBar>().startLoading();
            this.GetComponentInChildren<Text>().text = "Loading...";
        }
    }

}
