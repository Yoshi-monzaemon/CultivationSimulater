using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using System.Security.Permissions;

public class PlantControl : MonoBehaviour
{
    GameObject eventSystemObject;
    private Subject<GameObject> touchPlantSubject = new Subject<GameObject>();

    public IObservable<GameObject> TouchPlant
    {
        get { return touchPlantSubject; }
    }

    // Use this for initialization
    void Start()
    {
    }


    private void Awake()
    {
        this.gameObject.AddComponent<ObservableEventTrigger>()
            .OnPointerDownAsObservable()
            .Subscribe(pointerEventData => {
                
                //イベントシステムオブジェクト取得
                eventSystemObject = GameObject.FindWithTag("EventSystem");
                GrowPlant growPlant = eventSystemObject.GetComponent<GrowPlant>();

                //肥料を与える
                if (this.gameObject.tag != "FruitPlant" && growPlant.fertilizerSelected)
                {
                    Debug.Log("肥料いけます");
                    growPlant.HastenGrowthOfPlant(this.gameObject);
                }
                //収穫する
                if (this.gameObject.tag == "FruitPlant" && growPlant.harvestSelected)
                {

                    //収穫処理実行
                    growPlant.HarvestPlant(this.gameObject);
                }
            })
            .AddTo(gameObject);
    }

}
