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

public class FieldControl : MonoBehaviour
{
    [SerializeField] List<GameObject> fieldInstants;

    private Subject<GameObject> growingSubject = new Subject<GameObject>();


    public IObservable<GameObject> FeedSeed
    {
        get { return growingSubject; }
    }

    // Use this for initialization
    void Start()
    {
    }

    private void Awake()
    {
        for(int i = 0; i < fieldInstants.Count; i++)
        {
            SetFieldInstance(i);
        }
    }

    //フィールドがクリック検知できるようにする
    void SetFieldInstance(int i)
    {
        fieldInstants[i].AddComponent<ObservableEventTrigger>()
            .OnPointerDownAsObservable()
            .Subscribe(pointerEventData =>
            {
                growingSubject.OnNext(pointerEventData.pointerEnter);
            })
            .AddTo(gameObject);
    }
}