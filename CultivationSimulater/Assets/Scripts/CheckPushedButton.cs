using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CheckPushedButton : MonoBehaviour
{
    [SerializeField] List<Button> buttons;

    private Subject<int> sendPushedButtonSubject = new Subject<int>();

    public IObservable<int> PushedButton
    {
        get { return sendPushedButtonSubject; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        for(int i=0; i<buttons.Count; i++)
        {
            SetPushedButtonCheckSubject(i);
        }
    }

    void SetPushedButtonCheckSubject(int i)
    {
        buttons[i].onClick.AsObservable()
            .Subscribe(_ =>
            {
                sendPushedButtonSubject.OnNext(i);
            })
            .AddTo(gameObject);
    }
}