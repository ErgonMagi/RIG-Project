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

    public void levelUp(float newMaxRep)
    {
        reputation = 0;
        maxRep = newMaxRep;
        Player.Instance.SetLevel(Player.Instance.getLevel() + 1);
    }

    public void UpdateReputation()
    {
        reputation = Player.Instance.GetReputation();
        repBarFill.DOFillAmount(reputation / maxRep, 1.0f);
    }
}
