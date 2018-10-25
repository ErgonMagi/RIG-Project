using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ReputationManager : Singleton<ReputationManager> {

    public float reputation;
    private float newRep;
    public float maxRep;

    public Image repBarFill;

    public void OnEnable()
    {
        maxRep = 1000f;
        SceneManager.sceneLoaded += newScene;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= newScene;
    }

    public void newScene(Scene scene, LoadSceneMode mode)
    {
        UpdateReputation();
    }

    public void setStartReputation(float rep, float maxRep)
    {
        reputation = rep;
        newRep = rep;
        this.maxRep = maxRep;
        repBarFill.fillAmount = reputation / maxRep;
    }

    public IEnumerator levelUp(float newMaxRep)
    {
        reputation -= maxRep;
        repBarFill.DOFillAmount(1, 1.0f);
        yield return new WaitForSeconds(1.0f);
        //Add level up effect here
        Player.Instance.SetLevel(Player.Instance.getLevel() + 1);
        reputation = 0;
        maxRep = newMaxRep;
        repBarFill.fillAmount = 0 / maxRep;
        repBarFill.DOFillAmount(reputation / maxRep, 1.0f);
    }

    public void UpdateReputation()
    {
        
        reputation = Player.Instance.GetReputation();
        if (reputation > maxRep)
        {
            StartCoroutine(levelUp(maxRep * 2));
        }
        else
        {
            float fill = (float)reputation / maxRep;
            repBarFill.DOFillAmount(fill, 1.0f);
        }
    }
}
