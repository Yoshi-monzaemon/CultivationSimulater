using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class GrowPlant : MonoBehaviour
{
    [SerializeField] private FieldControl fieldControl;
    [SerializeField] private CheckPushedButton checkPushedButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        fieldControl.FeedSeed.Subscribe(clickedFieldInstance =>
        {
            GrowingPlant(clickedFieldInstance);
        });

        checkPushedButton.PushedButton.Subscribe(buttonNumber => Debug.Log(buttonNumber));
    }

    //植物を成長させる
    public void GrowingPlant(GameObject seedField)
    {
        Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                sproutingPlant();
            })
            .AddTo(gameObject);
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
}
