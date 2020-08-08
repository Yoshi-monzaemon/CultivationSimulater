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
    private Subject<int> growingSubject = new Subject<int>();

    public IObservable<int> FeedSeed
    {
        get { return growingSubject; }
    }

    // Use this for initialization
    void Start()
    {
    }

    private void Awake()
    {
        var eventTrigger = this.gameObject.AddComponent<ObservableEventTrigger>();

        //PointerDown
        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(pointerEventData =>
            {
                UnityEngine.Debug.Log(pointerEventData.position);
                growingSubject.OnNext(0);
            })
            .AddTo(gameObject);
    }

}