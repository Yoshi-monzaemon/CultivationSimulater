using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class GrowPlant : MonoBehaviour
{

    [SerializeField] private FieldControl fieldControl;

    // Start is called before the first frame update
    void Start()
    {
        fieldControl.FeedSeed.Subscribe(_ =>
        {
            Debug.Log("seed");
        });
    }

    private void Awake()
    {
        
    }

    //発芽
    public void sproutingPlant()
    {

    }

    //開花
    public void bloomingPlant()
    {

    } 

    //結実
    public void fruitingPlant()
    {

    }

    public void GrowingPlant()
    {
        //Observable.Timer(TimeSpan.FromSeconds(5))
        //    .Subscribe(_ =>
        //    {
        //        Debug.Log("5secondsPassed");
        //    })
        //    .AddTo;
    }
}
