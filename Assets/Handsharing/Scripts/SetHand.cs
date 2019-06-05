using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHand : MonoBehaviour {

    public GameObject leftHand_dst;
    public GameObject leftHand_Palm;

    public GameObject rightHand_dst;
    public GameObject rightHand_Palm;

    public GameObject markerPosition;
    public GameObject imageTarget;

    public float gestureYOffset = 0.0f;

    // Use this for initialization
    void Start () {

        //leftHand_dst.SetActive(true);
        //rightHand_dst.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        
        //Left Hand///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (SocketClient.Hand.L_Active == 1) leftHand_dst.SetActive(true);
        else leftHand_dst.SetActive(false);

        ////left index
        ///
        Quaternion L_Palm_R_World = (imageTarget.transform.rotation * Quaternion.Euler(0.0f, gestureYOffset, 0.0f)) * (SocketClient.Hand.L_Palm_R * Quaternion.Euler(0.0f, 0.0f, 0.0f));
        leftHand_Palm.transform.localRotation = L_Palm_R_World;
        leftHand_Palm.transform.localPosition = SocketClient.Hand.L_Palm_P;


        leftHand_Palm.transform.Find("L_index_meta").localPosition = SocketClient.Hand.L_index_meta_P;
        leftHand_Palm.transform.Find("L_index_meta").localRotation = SocketClient.Hand.L_index_meta_R;

        leftHand_Palm.transform.Find("L_index_meta").GetChild(0).localPosition = SocketClient.Hand.L_index_a_P;
        leftHand_Palm.transform.Find("L_index_meta").GetChild(0).localRotation = SocketClient.Hand.L_index_a_R;

        leftHand_Palm.transform.Find("L_index_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_index_b_P;
        leftHand_Palm.transform.Find("L_index_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_index_b_R;

        leftHand_Palm.transform.Find("L_index_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_index_c_P;
        leftHand_Palm.transform.Find("L_index_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_index_c_R;

        //left middle
        leftHand_Palm.transform.Find("L_middle_meta").localPosition = SocketClient.Hand.L_middle_meta_P;
        leftHand_Palm.transform.Find("L_middle_meta").localRotation = SocketClient.Hand.L_middle_meta_R;

        leftHand_Palm.transform.Find("L_middle_meta").GetChild(0).localPosition = SocketClient.Hand.L_middle_a_P;
        leftHand_Palm.transform.Find("L_middle_meta").GetChild(0).localRotation = SocketClient.Hand.L_middle_a_R;

        leftHand_Palm.transform.Find("L_middle_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_middle_b_P;
        leftHand_Palm.transform.Find("L_middle_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_middle_b_R;

        leftHand_Palm.transform.Find("L_middle_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_middle_c_P;
        leftHand_Palm.transform.Find("L_middle_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_middle_c_R;

        //left pinky
        leftHand_Palm.transform.Find("L_pinky_meta").localPosition = SocketClient.Hand.L_pinky_meta_P;
        leftHand_Palm.transform.Find("L_pinky_meta").localRotation = SocketClient.Hand.L_pinky_meta_R;

        leftHand_Palm.transform.Find("L_pinky_meta").GetChild(0).localPosition = SocketClient.Hand.L_pinky_a_P;
        leftHand_Palm.transform.Find("L_pinky_meta").GetChild(0).localRotation = SocketClient.Hand.L_pinky_a_R;

        leftHand_Palm.transform.Find("L_pinky_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_pinky_b_P;
        leftHand_Palm.transform.Find("L_pinky_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_pinky_b_R;

        leftHand_Palm.transform.Find("L_pinky_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_pinky_c_P;
        leftHand_Palm.transform.Find("L_pinky_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_pinky_c_R;

        //left ring
        leftHand_Palm.transform.Find("L_ring_meta").localPosition = SocketClient.Hand.L_ring_meta_P;
        leftHand_Palm.transform.Find("L_ring_meta").localRotation = SocketClient.Hand.L_ring_meta_R;

        leftHand_Palm.transform.Find("L_ring_meta").GetChild(0).localPosition = SocketClient.Hand.L_ring_a_P;
        leftHand_Palm.transform.Find("L_ring_meta").GetChild(0).localRotation = SocketClient.Hand.L_ring_a_R;

        leftHand_Palm.transform.Find("L_ring_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_ring_b_P;
        leftHand_Palm.transform.Find("L_ring_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_ring_b_R;

        leftHand_Palm.transform.Find("L_ring_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_ring_c_P;
        leftHand_Palm.transform.Find("L_ring_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_ring_c_R;

        //left thumb
        leftHand_Palm.transform.Find("L_thumb_meta").localPosition = SocketClient.Hand.L_thumb_meta_P;
        leftHand_Palm.transform.Find("L_thumb_meta").localRotation = SocketClient.Hand.L_thumb_meta_R;

        leftHand_Palm.transform.Find("L_thumb_meta").GetChild(0).localPosition = SocketClient.Hand.L_thumb_a_P;
        leftHand_Palm.transform.Find("L_thumb_meta").GetChild(0).localRotation = SocketClient.Hand.L_thumb_a_R;

        leftHand_Palm.transform.Find("L_thumb_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_thumb_b_P;
        leftHand_Palm.transform.Find("L_thumb_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_thumb_b_R;

        leftHand_Palm.transform.Find("L_thumb_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.L_thumb_c_P;
        leftHand_Palm.transform.Find("L_thumb_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.L_thumb_c_R;

        //Right Hand///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (SocketClient.Hand.R_Active == 1) rightHand_dst.SetActive(true);
        else rightHand_dst.SetActive(false);        

        Quaternion R_Palm_R_World = (imageTarget.transform.rotation * Quaternion.Euler(0.0f, gestureYOffset, 0.0f)) * (SocketClient.Hand.R_Palm_R * Quaternion.Euler(0.0f, 0.0f, 0.0f));
        rightHand_Palm.transform.localRotation = R_Palm_R_World;
        ////left index
        rightHand_Palm.transform.localPosition = SocketClient.Hand.R_Palm_P;



        rightHand_Palm.transform.Find("R_index_meta").localPosition = SocketClient.Hand.R_index_meta_P;
        rightHand_Palm.transform.Find("R_index_meta").localRotation = SocketClient.Hand.R_index_meta_R;

        rightHand_Palm.transform.Find("R_index_meta").GetChild(0).localPosition = SocketClient.Hand.R_index_a_P;
        rightHand_Palm.transform.Find("R_index_meta").GetChild(0).localRotation = SocketClient.Hand.R_index_a_R;

        rightHand_Palm.transform.Find("R_index_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_index_b_P;
        rightHand_Palm.transform.Find("R_index_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_index_b_R;

        rightHand_Palm.transform.Find("R_index_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_index_c_P;
        rightHand_Palm.transform.Find("R_index_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_index_c_R;

        //left middle
        rightHand_Palm.transform.Find("R_middle_meta").localPosition = SocketClient.Hand.R_middle_meta_P;
        rightHand_Palm.transform.Find("R_middle_meta").localRotation = SocketClient.Hand.R_middle_meta_R;

        rightHand_Palm.transform.Find("R_middle_meta").GetChild(0).localPosition = SocketClient.Hand.R_middle_a_P;
        rightHand_Palm.transform.Find("R_middle_meta").GetChild(0).localRotation = SocketClient.Hand.R_middle_a_R;

        rightHand_Palm.transform.Find("R_middle_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_middle_b_P;
        rightHand_Palm.transform.Find("R_middle_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_middle_b_R;

        rightHand_Palm.transform.Find("R_middle_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_middle_c_P;
        rightHand_Palm.transform.Find("R_middle_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_middle_c_R;

        //left pinky
        rightHand_Palm.transform.Find("R_pinky_meta").localPosition = SocketClient.Hand.R_pinky_meta_P;
        rightHand_Palm.transform.Find("R_pinky_meta").localRotation = SocketClient.Hand.R_pinky_meta_R;

        rightHand_Palm.transform.Find("R_pinky_meta").GetChild(0).localPosition = SocketClient.Hand.R_pinky_a_P;
        rightHand_Palm.transform.Find("R_pinky_meta").GetChild(0).localRotation = SocketClient.Hand.R_pinky_a_R;

        rightHand_Palm.transform.Find("R_pinky_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_pinky_b_P;
        rightHand_Palm.transform.Find("R_pinky_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_pinky_b_R;

        rightHand_Palm.transform.Find("R_pinky_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_pinky_c_P;
        rightHand_Palm.transform.Find("R_pinky_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_pinky_c_R;

        //left ring
        rightHand_Palm.transform.Find("R_ring_meta").localPosition = SocketClient.Hand.R_ring_meta_P;
        rightHand_Palm.transform.Find("R_ring_meta").localRotation = SocketClient.Hand.R_ring_meta_R;

        rightHand_Palm.transform.Find("R_ring_meta").GetChild(0).localPosition = SocketClient.Hand.R_ring_a_P;
        rightHand_Palm.transform.Find("R_ring_meta").GetChild(0).localRotation = SocketClient.Hand.R_ring_a_R;

        rightHand_Palm.transform.Find("R_ring_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_ring_b_P;
        rightHand_Palm.transform.Find("R_ring_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_ring_b_R;

        rightHand_Palm.transform.Find("R_ring_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_ring_c_P;
        rightHand_Palm.transform.Find("R_ring_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_ring_c_R;

        //left thumb
        rightHand_Palm.transform.Find("R_thumb_meta").localPosition = SocketClient.Hand.R_thumb_meta_P;
        rightHand_Palm.transform.Find("R_thumb_meta").localRotation = SocketClient.Hand.R_thumb_meta_R;

        rightHand_Palm.transform.Find("R_thumb_meta").GetChild(0).localPosition = SocketClient.Hand.R_thumb_a_P;
        rightHand_Palm.transform.Find("R_thumb_meta").GetChild(0).localRotation = SocketClient.Hand.R_thumb_a_R;

        rightHand_Palm.transform.Find("R_thumb_meta").GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_thumb_b_P;
        rightHand_Palm.transform.Find("R_thumb_meta").GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_thumb_b_R;

        rightHand_Palm.transform.Find("R_thumb_meta").GetChild(0).GetChild(0).GetChild(0).localPosition = SocketClient.Hand.R_thumb_c_P;
        rightHand_Palm.transform.Find("R_thumb_meta").GetChild(0).GetChild(0).GetChild(0).localRotation = SocketClient.Hand.R_thumb_c_R;
    }
}
