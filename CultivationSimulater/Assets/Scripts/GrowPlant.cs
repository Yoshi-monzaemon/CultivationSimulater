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

    public bool fertilizerSelected = false;
    public bool harvestSelected = false;

    Color planeColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
    Color selectedColor = new Color(165f / 255f, 220f / 255f, 192f / 255f, 255f / 255f);

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //フィールドがクリックされたとき
        fieldControl.FeedSeed
            .Subscribe(clickedFieldInstance =>
            {
                //選択された農地を取得
                Transform fieldObject = clickedFieldInstance.transform;
            
                //肥料も収穫も選択されていないとき
                if (!fertilizerSelected && !harvestSelected)
                {
                    SproutingPlant(fieldObject);
                }
            })
            .AddTo(gameObject);

        //肥料か収穫のボタンが押されたとき
        checkPushedButton.PushedButton
            .Subscribe(buttonNumber =>
            {
                ChangeStatusPushedButton(buttonNumber);
            })
            .AddTo(gameObject);
    }

    //植物を植える
    public void SproutingPlant(Transform fieldObject)
    {
        GameObject obj = (GameObject)Instantiate(sproutPlant, fieldObject);
        obj.transform.parent = fieldObject;
    }

    //収穫、肥料のいずれかのボタンが押下された際のフラグ立て
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
                    harvestSelected = false;
                    buttons[1].image.color = planeColor;
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
                    fertilizerSelected = false;
                    buttons[0].image.color = planeColor;
                }
                break;
        }
    }
}
