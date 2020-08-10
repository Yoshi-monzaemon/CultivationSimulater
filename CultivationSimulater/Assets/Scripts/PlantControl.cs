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
    [SerializeField] private GameObject bloomPlant;
    [SerializeField] private GameObject fruitPlant;

    GameObject eventSystemObject;
    GameObject parentFieldObject;

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
        //イベントシステムオブジェクトにアタッチされたスクリプトを取得
        eventSystemObject = GameObject.FindWithTag("EventSystem");
        GrowPlant growPlant = eventSystemObject.GetComponent<GrowPlant>();
        CounterControl counterControl = eventSystemObject.GetComponent<CounterControl>();

        //親オブジェクト取得
        parentFieldObject = this.gameObject.transform.parent.gameObject;
        //栽培中としてステータス変更
        parentFieldObject.tag = "NowPlantingField";

        this.gameObject.AddComponent<ObservableEventTrigger>()
            .OnPointerDownAsObservable()
            .Subscribe(pointerEventData => {
                //肥料を与える
                if (this.gameObject.tag != "FruitPlant" && growPlant.fertilizerSelected)
                {
                    GameObject obj = (GameObject)Instantiate(fruitPlant, parentFieldObject.transform);
                    obj.transform.parent = parentFieldObject.transform;
                    Destroy(this.gameObject);
                    counterControl.ConsumeFertilizer(1);
                }
                //収穫する
                if (this.gameObject.tag == "FruitPlant" && growPlant.harvestSelected)
                {
                    //収穫処理実行
                    Destroy(this.gameObject);
                    parentFieldObject.tag = "plantfield";
                    counterControl.IncreaseHarvestAmount(1);
                }
            })
            .AddTo(gameObject);

        //植物を成長させる(芽→花）
        if (this.gameObject.tag == "SproutPlant") {
        Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                GameObject obj = (GameObject)Instantiate(bloomPlant, parentFieldObject.transform);
                obj.transform.parent = parentFieldObject.transform;
                Destroy(gameObject);
            })
            .AddTo(gameObject);
        }
        //植物を成長させる（花→果実）
        else if (this.gameObject.tag == "BloomPlant")
        {
            Observable.Timer(TimeSpan.FromSeconds(5))
                .Subscribe(_ =>
                {
                    GameObject obj = (GameObject)Instantiate(fruitPlant, parentFieldObject.transform);
                    obj.transform.parent = parentFieldObject.transform;
                    Destroy(gameObject);
                })
                .AddTo(gameObject);
        }
    }
}
