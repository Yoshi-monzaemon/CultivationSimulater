using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Runtime.Versioning;
using System.Security.Cryptography;

public class GrowPlant : MonoBehaviour
{
    [SerializeField] private FieldControl fieldControl;
    [SerializeField] private GameObject sproutPlant;
    [SerializeField] private GameObject bloomPlant;
    [SerializeField] private GameObject fruitPlant;

    [SerializeField] private CheckPushedButton checkPushedButton;
    [SerializeField] List<Button> buttons;

    bool fertilizerSelected = false;
    bool harvestSelected = false;

    Color planeColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
    Color selectedColor = new Color(165f / 255f, 220f / 255f, 192f / 255f, 255f / 255f);

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        fieldControl.FeedSeed.Subscribe(clickedFieldInstance =>
        {
            //選択された農地を取得
            Transform fieldObject = clickedFieldInstance.transform;
            Transform plantObject;
            bool plantExist;

            //既に作物を栽培中か判定
            if (fieldObject.childCount > 0)
            {
                plantExist = true;
                plantObject = fieldObject.GetChild(0).transform;
            }
            else
            {
                plantExist = false;
            }

            if (plantExist)
            {
                //肥料モードの時
                if (fertilizerSelected)
                {

                }
                //収穫モードの時
                else if (harvestSelected)
                {
                    //植物が収穫可能か確認
                    plantObject = fieldObject.GetChild(0).transform;
                    if (plantObject.tag == "FruitPlant")
                    {
                        Debug.Log("実っています");
                    }
                }
            }
            else
            {
                GrowingPlant(fieldObject);
            }
        });

        checkPushedButton.PushedButton
            .Subscribe(buttonNumber =>
            {
                ChangeStatusPushedButton(buttonNumber);
            });
    }

    //植物を成長させる
    public void GrowingPlant(Transform fieldObject)
    {
        Observable.Timer(TimeSpan.FromSeconds(0))
            .Subscribe(_ => {
                GameObject obj = (GameObject)Instantiate(sproutPlant, fieldObject);
                obj.transform.parent = fieldObject;
            })
            .AddTo(gameObject);
        Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                //Destroy(seedField);
                GameObject obj = (GameObject)Instantiate(bloomPlant, fieldObject);
                obj.transform.parent = fieldObject;
            })
            .AddTo(gameObject);
        Observable.Timer(TimeSpan.FromSeconds(10))
            .Subscribe(_ =>
            {
                GameObject obj = (GameObject)Instantiate(fruitPlant, fieldObject);
                obj.transform.parent = fieldObject;
            })
            .AddTo(gameObject);
    }

    //収穫、肥料のいずれかが選択されていた場合のフラグ立て
    void ChangeStatusPushedButton(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                if (fertilizerSelected)
                {
                    fertilizerSelected = false;
                    buttons[buttonNumber].image.color = planeColor;
                }
                else
                {
                    fertilizerSelected = true;
                    buttons[buttonNumber].image.color = selectedColor;
                }
                break;
            case 1:
                if (harvestSelected)
                {
                    harvestSelected = false;
                    buttons[buttonNumber].image.color = planeColor;
                }
                else
                {
                    harvestSelected = true;
                    buttons[buttonNumber].image.color = selectedColor;
                }
                break;
        }
    }
    //収穫


}
