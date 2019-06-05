using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placePinManager : MonoBehaviour {

    public GameObject locationMarker;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SocketClient.isMarkerPlaced == 1)
        {
            locationMarker.SetActive(true);
        }
        locationMarker.transform.SetPositionAndRotation(Vector3.zero, new Quaternion());//Change to Marker position
    }
}
