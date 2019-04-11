using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class temp
{
    public int a;
    public string b;
}

public class playerController : MonoBehaviourPunCallbacks, IPunObservable
{

    public static GameObject LocalPlayerInstance;
    public Material[] materials;

    bool isPush = false;
    #region private 

    Transform thisObject;
    PhotonView photonView;
    #endregion


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //우리는 이 플레이어를 소유중- 다른사람에게 우리의 데이터를 send

            if (isPush == true)
                stream.SendNext("hello");  //총알을 발사 했는지


        }
        else
        {
            //네트워크 플레이어, receive data
            string str = (string)stream.ReceiveNext();
            Debug.Log(str);
        }

    }
    


    [PunRPC]
    void LogTest(object[] str)
    {        
        Debug.Log(str[0]);
        Debug.Log(str[1]);
        Debug.Log(str[2]);
    }


    private void Awake()
    {
        thisObject = this.GetComponent<Transform>();
        photonView = this.GetComponent<PhotonView>();

        //used in GameManager.cs : 레벨이 동기화 될 때 인스턴스화를 방지하기 위해 localPlayer 인스턴스를 추적함.?
        if (photonView.IsMine)
        {
            playerController.LocalPlayerInstance = this.gameObject;
        }
        // 인스턴스가 레벨 동기화를 견뎌 낼 수 있도록 로드시 파괴하지 않도록 플래그를 지정하여 레벨 로드시 원활히 제공.?
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {

        if (photonView.IsMine)
        {
            this.GetComponent<Renderer>().material = materials[0];
        }
        else //if(photonView.ViewID == 2001)
        {
            this.GetComponent<Renderer>().material = materials[1];
        }

    }

    void Update () {
        if (photonView.IsMine)
            playerKeyInput();
    }

    void playerKeyInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            thisObject.position += new Vector3(0, 0, 5) * Time.deltaTime;
            isPush = true;

            object[] newTemp = new object[3];
            newTemp[0] = 3;
            newTemp[1] = "logTest";
            newTemp[2] = 5;
            photonView.RPC("LogTest", RpcTarget.All, newTemp);
        } 
        else if (Input.GetKey(KeyCode.A))
        {
            thisObject.position += new Vector3(-5, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            thisObject.position += new Vector3(0, 0, -5) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            thisObject.position += new Vector3(5, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey (KeyCode.Space))
        {
            thisObject.position += new Vector3(0, 5, 0) * Time.deltaTime;
        }
    }
}
