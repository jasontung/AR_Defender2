using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Events;
public class BattleFieldController : MonoBehaviour, ITrackableEventHandler {
    public UnityEvent onBattleFieldReady;
    public UnityEvent onBattleFieldLost;
    public float maxLostTime = 1f;
    private float lostTime;
    // Use this for initialization
    void Start()
    {
        TrackableBehaviour trackableBehaivor = GetComponent<TrackableBehaviour>();
        trackableBehaivor.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.DETECTED || 
            newStatus == TrackableBehaviour.Status.TRACKED || 
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("BattleField Ready");
            onBattleFieldReady.Invoke();
            enabled = false;
        }
        else
        {
            enabled = true;
            lostTime = 0;
        }
    }

    private void Update()
    {
        lostTime += Time.unscaledDeltaTime;
        if(lostTime >= maxLostTime)
        {
            Debug.Log("BattleField Lost");
            onBattleFieldLost.Invoke();
            enabled = false;
        }
    }
}
