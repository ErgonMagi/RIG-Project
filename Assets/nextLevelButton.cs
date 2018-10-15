using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevelButton : MonoBehaviour {

	public void Clicked()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
