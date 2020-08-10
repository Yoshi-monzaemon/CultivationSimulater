using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Globalization;

public class CounterControl : MonoBehaviour
{
    [SerializeField] private Text harvestAmountView;
    [SerializeField] private Text fertilizerAmountView;
    [SerializeField] private Text balanceView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        harvestAmountView.text = "0";
        fertilizerAmountView.text = "100";
        balanceView.text = "0";
    }

    public void IncreaseHarvestAmount(int harvestYield)
    {
        harvestAmountView.text = (int.Parse(harvestAmountView.text) + harvestYield).ToString();
    }

    public void ConsumeFertilizer(int consumptionOfFertilizer)
    {
        if (fertilizerAmountView.text != "0")
        {
            fertilizerAmountView.text = (int.Parse(fertilizerAmountView.text) - consumptionOfFertilizer).ToString();
        }
    }

    public void Balance(int i)
    {

    }
}
