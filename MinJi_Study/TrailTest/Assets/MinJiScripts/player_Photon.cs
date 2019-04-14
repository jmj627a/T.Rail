using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class player_Photon : MonoBehaviourPunCallbacks{

    private PhotonView photonView;
    private new Rigidbody rigidbody;
    private new Collider collider;
    private new Renderer renderer;

    public Material[] materials;

    private bool controllable = true;

    public void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            renderer.material = materials[0];
        }
        else //if(photonView.ViewID == 2001)
        {
           // this.GetComponent<Renderer>().material = materials[1];
        }
    }

    // Update is called once per frame
    void Update () {
        if (!photonView.IsMine || !controllable)
        {
            return;
        }
    }


    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    // 로컬 플레이어의 위치 정보 송신
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    else // 원격 플레이어의 위치 정보 수신
    //    {
    //        this.transform.position = (Vector3)stream.ReceiveNext();
    //        this.transform.rotation = (Quaternion)stream.ReceiveNext();
    //    }
    //}
}
