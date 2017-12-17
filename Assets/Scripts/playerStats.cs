using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour {

    public int Money;
    public int StartMoney;
    public int MoneyGenerationAmount;
    public Text moneyPerText;
    public Text moneyText;

    // Use this for initialization
    void Start () {
        Money = StartMoney;
        MoneyGenerationAmount = 5;
	}

    void Awake()
    {
        StartCoroutine(moneyBuffer());
    }

    // Update is called once per frame
    void Update ()
    {
        informationUpdate();
	}

    public void MoneyGenerationIncrease (int moneyUpgrade)
    {
        MoneyGenerationAmount = MoneyGenerationAmount + moneyUpgrade;
    }

    void informationUpdate()
    {
        moneyText.text = "$" + Money.ToString(); // constantly update the money text with the current money from palyerstats
        moneyPerText.text = "$" + MoneyGenerationAmount.ToString(); // constantly update the money text with the current money from palyerstats
    }

    IEnumerator moneyBuffer()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Money = MoneyGenerationAmount + Money;
        }
    }
}
