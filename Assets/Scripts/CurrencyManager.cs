using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CurrencyManager : Singleton<CurrencyManager> {

    private int currency;
    private int newCurrency;

    public TextMeshProUGUI currencyOwned;
    public TextMeshProUGUI currencyChange;

    public void OnEnable()
    {
        SceneManager.sceneLoaded += newScene;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= newScene;
    }

    public void setStartCurrency(int curr)
    {
        currency = curr;
        newCurrency = curr;
        currencyOwned.text = currency.ToString();
    }

    public void newScene(Scene scene, LoadSceneMode mode)
    {
        UpdateCurrency();
    }

    public void UpdateCurrency()
    {
        newCurrency = Player.Instance.getMoney();
        int change = newCurrency - currency;

        currencyChange.text = change.ToString();
        if (change > 0)
        {
            currencyChange.color = Color.green;
            Color endColor = Color.green;
            endColor.a = 0;
            currencyChange.transform.DOLocalMoveY(-80f, 1.0f);
            currencyChange.DOColor(endColor, 1.0f);
            currencyChange.transform.DOLocalMoveY(-40f, 0.0f).SetDelay<Tween>(1.0f);
        }
        else
        {
            currencyChange.color = Color.red;
            Color endColor = Color.red;
            endColor.a = 0;
            currencyChange.transform.DOLocalMoveY(-80f, 1.0f);
            currencyChange.DOColor(endColor, 1.0f);
            currencyChange.transform.DOLocalMoveY(-40f, 0.0f).SetDelay<Tween>(1.0f);
        }

        StartCoroutine(currencyTicking(2f));
    }

    public IEnumerator currencyTicking(float timeTaken)
    {
        float timeTicking = 0;
        while(currency != newCurrency)
        {

            currency = (int)((timeTicking / timeTaken) * (newCurrency - currency) + currency);
            currencyOwned.text = currency.ToString();
            timeTicking += 0.01f;
            yield return new WaitForSeconds(0.01f);
            if(timeTicking > timeTaken)
            {
                timeTicking = timeTaken;
                currency = newCurrency;
            }
        }
    }
}
