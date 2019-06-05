using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ConnectOnTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MLInput.OnTriggerUp += HandleTriggerDown;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleTriggerDown(byte controllerId, float triggerValue)
    {
        
    }
}
