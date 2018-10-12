using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReputationManager : Singleton<ReputationManager> {

    private float reputation;
    private float newRep;
    private float maxRep;

    public Image repBarFill;

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
        if(reputation > maxRep)
        {
            StartCoroutine(levelUp(maxRep * 2));
        }
        else
        {
            repBarFill.DOFillAmount(reputation / maxRep, 1.0f);
        }
    }
}
