using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class DrawLineManager : MonoBehaviour {


    private int numClicks = 0;
    public  LineRenderer currLine;
    public ControllerConnectionHandler _controllerConnectionHandler;

    public GameObject marker;

    // Use this for initialization
    void Start () {
        
        MLInput.OnTriggerUp += HandleTriggerUp;
        GameObject go = new GameObject();
    }
	
	// Update is called once per frame
	void Update () {

        //MLInputController controller = _controllerConnectionHandler.ConnectedController;
        //if (controller.TriggerValue > 0f)
        //{
        //    currLine.positionCount = numClicks + 1;
        //    currLine.SetPosition(numClicks, controller.Position);
        //    numClicks++;
        //    Debug.Log(numClicks);
        //}
   
        if (SocketClient.isDrawLine == 1)
        {
            currLine.positionCount = numClicks + 1;
            currLine.SetPosition(numClicks, marker.transform.TransformPoint(SocketClient.drawPointPosition));
            numClicks++;
        }
        else
            numClicks = 0;
    }

    private void HandleTriggerDown(byte controllerId, float triggerValue)
    {
        MLInputController controller = _controllerConnectionHandler.ConnectedController;
        currLine.positionCount = numClicks + 1;
        currLine.SetPosition(numClicks, controller.Position);
        numClicks++;
        Debug.Log(numClicks);
    }

    private void HandleTriggerUp(byte controllerId, float triggerValue)
    {
        numClicks = 0;
    }

}
