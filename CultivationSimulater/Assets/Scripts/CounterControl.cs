using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Globalization;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CounterControl : MonoBehaviour
{
    [SerializeField] private Text harvestAmountView;
    [SerializeField] private Text fertilizerAmountView;
    [SerializeField] private Text balanceView;
    [SerializeField] private CheckPushedButton checkPushedButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        harvestAmountView.text = "0";
        fertilizerAmountView.text = "100";
        balanceView.text = "0";

        checkPushedButton.PushedButton
            .Subscribe(buttonNumber =>
            {
                if (buttonNumber == 2)
                {
                    if (int.Parse(harvestAmountView.text)>=10) 
                    {
                        DecreaseHarvestAmount(10);
                        IncreaseBalance(100);
                    } 
                }
            })
            .AddTo(gameObject);

    }

    public void IncreaseHarvestAmount(int harvestYield)
    {
        harvestAmountView.text = (int.Parse(harvestAmountView.text) + harvestYield).ToString();
    }

    public void DecreaseHarvestAmount(int harvestYield)
    {
        harvestAmountView.text = (int.Parse(harvestAmountView.text) - harvestYield).ToString();
    }

    public void ConsumeFertilizer(int consumptionOfFertilizer)
    {
        if (fertilizerAmountView.text != "0")
        {
            fertilizerAmountView.text = (int.Parse(fertilizerAmountView.text) - consumptionOfFertilizer).ToString();
        }
    }

    public void IncreaseBalance(int income)
    {
        balanceView.text = (int.Parse(balanceView.text) + income).ToString();
    }
}
