using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class SocketClient : MonoBehaviour
{

    public string serverIP;
    public int serverPort;

    private Socket client;

    private bool IsConnected = false;
    private bool enableSendingMsg = true;
    private string TAG = "SocketClient: ";
    private byte[] msg = new byte[10000000];

    public GameObject imageTarget,mainCamera;

    [SerializeField, Tooltip("The headpose canvas for example status text.")]
    private Text _statusLabel,_networkTransform, _CameraPosition, _cameraRotation;

    public Transform HeadPose, eyeGaze;
    private Transform transformToUse;

    public bool useHeadPose = false;

    public static int isDrawLine = 0 , isMarkerPlaced = 0;
    public static Vector3 drawPointPosition, pinPointPosition;

    //private EmpaLinkClass empalink;

    public enum messageID
    {
        msgString = 0,
        msgInt = 1,
        msgFloat = 2,
        msgEmpatica = 3,
        msgHeadPose = 4,
        msgCameraPose = 5,
        msgHeadCamera = 6,
        msgHandInfo = 7,
        EndThread = 99
    }

    public struct HandInfo
    {
        //Left Hand
        public int L_Active;
        public Vector3 L_Palm_P;
        public Quaternion L_Palm_R;

        public Vector3 L_index_meta_P;
        public Quaternion L_index_meta_R;
        public Vector3 L_index_a_P;
        public Quaternion L_index_a_R;
        public Vector3 L_index_b_P;
        public Quaternion L_index_b_R;
        public Vector3 L_index_c_P;
        public Quaternion L_index_c_R;

        public Vector3 L_middle_meta_P;
        public Quaternion L_middle_meta_R;
        public Vector3 L_middle_a_P;
        public Quaternion L_middle_a_R;
        public Vector3 L_middle_b_P;
        public Quaternion L_middle_b_R;
        public Vector3 L_middle_c_P;
        public Quaternion L_middle_c_R;

        public Vector3 L_pinky_meta_P;
        public Quaternion L_pinky_meta_R;
        public Vector3 L_pinky_a_P;
        public Quaternion L_pinky_a_R;
        public Vector3 L_pinky_b_P;
        public Quaternion L_pinky_b_R;
        public Vector3 L_pinky_c_P;
        public Quaternion L_pinky_c_R;

        public Vector3 L_ring_meta_P;
        public Quaternion L_ring_meta_R;
        public Vector3 L_ring_a_P;
        public Quaternion L_ring_a_R;
        public Vector3 L_ring_b_P;
        public Quaternion L_ring_b_R;
        public Vector3 L_ring_c_P;
        public Quaternion L_ring_c_R;

        public Vector3 L_thumb_meta_P;
        public Quaternion L_thumb_meta_R;
        public Vector3 L_thumb_a_P;
        public Quaternion L_thumb_a_R;
        public Vector3 L_thumb_b_P;
        public Quaternion L_thumb_b_R;
        public Vector3 L_thumb_c_P;
        public Quaternion L_thumb_c_R;

        //right Hand
        public int R_Active;
        public Vector3 R_Palm_P;
        public Quaternion R_Palm_R;

        public Vector3 R_index_meta_P;
        public Quaternion R_index_meta_R;
        public Vector3 R_index_a_P;
        public Quaternion R_index_a_R;
        public Vector3 R_index_b_P;
        public Quaternion R_index_b_R;
        public Vector3 R_index_c_P;
        public Quaternion R_index_c_R;

        public Vector3 R_middle_meta_P;
        public Quaternion R_middle_meta_R;
        public Vector3 R_middle_a_P;
        public Quaternion R_middle_a_R;
        public Vector3 R_middle_b_P;
        public Quaternion R_middle_b_R;
        public Vector3 R_middle_c_P;
        public Quaternion R_middle_c_R;

        public Vector3 R_pinky_meta_P;
        public Quaternion R_pinky_meta_R;
        public Vector3 R_pinky_a_P;
        public Quaternion R_pinky_a_R;
        public Vector3 R_pinky_b_P;
        public Quaternion R_pinky_b_R;
        public Vector3 R_pinky_c_P;
        public Quaternion R_pinky_c_R;

        public Vector3 R_ring_meta_P;
        public Quaternion R_ring_meta_R;
        public Vector3 R_ring_a_P;
        public Quaternion R_ring_a_R;
        public Vector3 R_ring_b_P;
        public Quaternion R_ring_b_R;
        public Vector3 R_ring_c_P;
        public Quaternion R_ring_c_R;

        public Vector3 R_thumb_meta_P;
        public Quaternion R_thumb_meta_R;
        public Vector3 R_thumb_a_P;
        public Quaternion R_thumb_a_R;
        public Vector3 R_thumb_b_P;
        public Quaternion R_thumb_b_R;
        public Vector3 R_thumb_c_P;
        public Quaternion R_thumb_c_R;
    };
    public static HandInfo Hand;


    void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;
        //ConnectServer(serverIP, serverPort);
    }

    private void OnButtonDown(byte controllerId, MLInputControllerButton button)
    {
        if (button == MLInputControllerButton.Bumper)
        {
            ConnectServer(serverIP, serverPort);
        }else if (button == MLInputControllerButton.Bumper)
        {
            //NOTHING HERE :D
        }
    }


    void Update()
    {
        if (useHeadPose)
        {
            transformToUse = HeadPose;
        }
        else
        {
            transformToUse = eyeGaze;
        }
        if (enableSendingMsg)
        {
            if (IsConnected)
            {
                try { sendPoseAndCamera((int)messageID.msgHeadCamera); }
                catch(Exception e)
                {
                    OnApplicationQuit();
                    Debug.Log(e.ToString());
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        
        IsConnected = false;
        client.Close();
        Debug.Log(TAG + "Client closed");
        _statusLabel.text = "Disconnected";
    }

    void ConnectServer(string ip, int port)
    {
        if (IsConnected)
        {
            OnApplicationQuit();
            return;
        }

        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPAddress mIp = IPAddress.Parse(ip);
        IPEndPoint ip_end_point = new IPEndPoint(mIp, port);
        
        try
        {
            client.Connect(ip_end_point);
            IsConnected = true;
            Debug.Log(TAG + "Succeed to connect server");
            _statusLabel.text = "Follow instructions from remote expert.";
        }
        catch(Exception e)
        {
            IsConnected = false;
            Debug.Log(TAG + "Failed to connect server"+e.ToString());
            _statusLabel.text = "failed to connect server"+e.ToString();
            return;
        }

        Thread t = new Thread(new ThreadStart(ReceiveMsg));
        t.Start();
    }

    void sendMsgString(int id, string msg)
    {
        ByteBuffer buffer = new ByteBuffer();
        
        buffer.WriteInt(id);
        buffer.WriteString(msg);
        client.Send(buffer.ToBytes());
    }

    void sendMsgInt(int id, int msg)
    {
        ByteBuffer buffer = new ByteBuffer();
        
        buffer.WriteInt(id);
        buffer.WriteInt(msg);
        client.Send(buffer.ToBytes());
    }

    void sendMsg(int id)
    {
        ByteBuffer buffer = new ByteBuffer();

        buffer.WriteInt(id);
        buffer.WriteFloat(21.4f);
        buffer.WriteFloat(18.6f);
        buffer.WriteFloat(0.43387f);

        client.Send(buffer.ToBytes());
    }

    void sendPoseAndCamera(int id)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInt(id);

        //Pose Position
        //buffer.WriteFloat(transformToUse.position.x - imageTarget.transform.position.x);
        //buffer.WriteFloat(transformToUse.position.y - imageTarget.transform.position.y);
        //buffer.WriteFloat(transformToUse.position.z - imageTarget.transform.position.z);
        //_networkTransform.text = "PosX:" + (transformToUse.position.x - imageTarget.transform.position.x) + "PosY:" + (transformToUse.position.y - imageTarget.transform.position.y) + "PosZ:" + (transformToUse.position.z - imageTarget.transform.position.z);
        //Debug.Log("GazePosX:" + (transformToUse.position.x - imageTarget.transform.position.x) + "PosY:" + (transformToUse.position.y - imageTarget.transform.position.y) + "PosZ:" + (transformToUse.position.z - imageTarget.transform.position.z));

        Vector3 gaze = imageTarget.transform.InverseTransformPoint(transformToUse.transform.position);
        buffer.WriteFloat(gaze.x);
        buffer.WriteFloat(gaze.y);
        buffer.WriteFloat(gaze.z);
        //rotation
        buffer.WriteFloat(transformToUse.rotation.x);
        buffer.WriteFloat(transformToUse.rotation.y);
        buffer.WriteFloat(transformToUse.rotation.z);
        buffer.WriteFloat(transformToUse.rotation.w);

        //Camera
        Vector3 camera_P = imageTarget.transform.InverseTransformPoint(mainCamera.transform.position);
        buffer.WriteFloat(camera_P.x);
        buffer.WriteFloat(camera_P.y);
        buffer.WriteFloat(camera_P.z);
        //Debug.Log("Edit camera position: " + camera_P.ToString("F6"));

        Quaternion camera_R = Quaternion.Inverse(imageTarget.transform.rotation) * mainCamera.transform.rotation;
        buffer.WriteFloat(camera_R.x);
        buffer.WriteFloat(camera_R.y);
        buffer.WriteFloat(camera_R.z);
        buffer.WriteFloat(camera_R.w);

        client.Send(buffer.ToBytes());
    }

    void sendMsgFloat(int id, float msg)
    {
        ByteBuffer buffer = new ByteBuffer();
        
        buffer.WriteInt(id);
        buffer.WriteFloat(msg);
        client.Send(buffer.ToBytes());
    }

    void ReceiveMsg()
    {
        while (IsConnected)
        {
            try
            {
                int length = client.Receive(msg);
                Debug.Log(TAG + "Server id: " + client.RemoteEndPoint.ToString() + " Length: " + length);
            }
            catch (Exception e)
            {
                Debug.Log(TAG + e.Message);
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                break;
            }

            ByteBuffer buff = new ByteBuffer(msg);

            int id = buff.ReadInt();
            if (id == (int)messageID.msgString)
            {
                string mssage = buff.ReadString();
                Debug.Log(TAG + "Server id: " + client.RemoteEndPoint.ToString() + " Test Message: " + mssage);
                _statusLabel.text = "Received int " + mssage;
            }
            else if (id == (int)messageID.msgInt)
            {
                int mssage = buff.ReadInt();
                Debug.Log(TAG + "Server id: " + client.RemoteEndPoint.ToString() + " Test Message: " + mssage);
            }
            else if (id == (int)messageID.msgFloat)
            {
                float mssage = buff.ReadFloat();
                Debug.Log(TAG + "Server id: " + client.RemoteEndPoint.ToString() + " Test Message: " + mssage);
            }
            else if (id == (int)messageID.msgHandInfo)
            {
                //Left hand//////////////////////////////////////////////////////////////////////////////////////////////////////
                Hand.L_Active = buff.ReadInt();
                Hand.L_Palm_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_Palm_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.L_index_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_index_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_index_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_index_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_index_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_index_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_index_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_index_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.L_middle_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_middle_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_middle_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_middle_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_middle_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_middle_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_middle_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_middle_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.L_pinky_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_pinky_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_pinky_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_pinky_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_pinky_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_pinky_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_pinky_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_pinky_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.L_ring_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_ring_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_ring_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_ring_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_ring_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_ring_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_ring_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_ring_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.L_thumb_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_thumb_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_thumb_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_thumb_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_thumb_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_thumb_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_thumb_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.L_thumb_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                //Right hand//////////////////////////////////////////////////////////////////////////////////////////////////////
                Hand.R_Active = buff.ReadInt();
                Hand.R_Palm_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_Palm_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.R_index_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_index_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_index_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_index_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_index_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_index_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_index_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_index_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.R_middle_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_middle_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_middle_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_middle_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_middle_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_middle_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_middle_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_middle_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.R_pinky_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_pinky_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_pinky_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_pinky_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_pinky_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_pinky_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_pinky_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_pinky_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.R_ring_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_ring_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_ring_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_ring_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_ring_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_ring_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_ring_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_ring_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                Hand.R_thumb_meta_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_thumb_meta_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_thumb_a_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_thumb_a_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_thumb_b_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_thumb_b_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_thumb_c_P = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                Hand.R_thumb_c_R = new Quaternion(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
                
                //Debug.Log("R:" + Hand.R_Palm_P.ToString("F6") + "L:" + Hand.L_Palm_P.ToString("F6"));       
                //draw Line
                isDrawLine = buff.ReadInt();
                drawPointPosition = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());

                //isMarkerPlaced = buff.ReadInt();
                //pinPointPosition = new Vector3(buff.ReadFloat(), buff.ReadFloat(), buff.ReadFloat());
            }
            else
                continue;
        }
    }

    
}
