using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class DeletePlantInstance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(gameObject);
    }
}