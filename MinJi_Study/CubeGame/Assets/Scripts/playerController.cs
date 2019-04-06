using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerController : MonoBehaviourPunCallbacks
{

    public static GameObject LocalPlayerInstance;


    #region private 

    Transform thisObject;

    #endregion

    private void Awake()
    {
        //used in GameManager.cs : 레벨이 동기화 될 때 인스턴스화를 방지하기 위해 localPlayer 인스턴스를 추적함.?
        if (photonView.IsMine)
        {
            playerController.LocalPlayerInstance = this.gameObject;
        }
        // 인스턴스가 레벨 동기화를 견뎌 낼 수 있도록 로드시 파괴하지 않도록 플래그를 지정하여 레벨 로드시 원활히 제공.?
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {

        thisObject = this.GetComponent<Transform>();

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
