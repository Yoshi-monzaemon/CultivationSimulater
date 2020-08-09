using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

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
            ButtonPushedCheck(i);

        }
    }

    void ButtonPushedCheck(int i)
    {
        buttons[i].onClick.AsObservable()
            .Subscribe(count => sendPushedButtonSubject.OnNext(i))
            .AddTo(gameObject);
    }
}
