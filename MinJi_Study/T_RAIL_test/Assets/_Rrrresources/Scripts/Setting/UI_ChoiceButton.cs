using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UI_ChoiceButton : MonoBehaviourPunCallbacks {



    // 이건 오브젝트 풀링으로 바꾸자
    // 일단은 active(false)해놓은거 하겠지만ㄴ
    public GameObject Sofa;
    public GameObject Box;

    public GameObject Hit_Object =null;
    public Transform Train_Object =null;

    Vector3 Sofa_Rotation = new Vector3(-90, -80, 0);
    // 그리고 이 choicebutton도 ui script로 옮기기

    PhotonView photonView;

    //넘겨줄때 기차칸이랑 몇번째 박스인지 알려줘야할듯

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void onChoiceButton(int kind)
    {
        if(kind == 0)
        {
            //Debug.Log("소파클릭" + Hit_Object.name + "    " + Train_Object.name);
            photonView.RPC("Sofa_Add", RpcTarget.AllBuffered, Hit_Object.GetPhotonView().ViewID);// Train_Object.gameObject.GetPhotonView().ViewID);
        }
        else if(kind == 1)
        {
            //Debug.Log("박스클릭");
            photonView.RPC("Box_Add", RpcTarget.AllBuffered, Hit_Object.GetPhotonView().ViewID);//, Train_Object.gameObject.GetPhotonView().ViewID);
        }
    }

    //train object는 기차 안에 있는거. 그리고 hit object는 4개의 행동영역중에 뭔지 
    [PunRPC]
    public void Sofa_Add(int hitObjectViewID)//, int TrainObjectViewID)
    {
        Hit_Object = PhotonView.Find(hitObjectViewID).gameObject;
        Debug.LogError("소파rpc" + "    " + Hit_Object);

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = Sofa_Rotation;
        
        GameObject sofa = PhotonNetwork.InstantiateSceneObject(Sofa.name, Hit_Object.transform.position, rot);
        sofa.transform.parent = Hit_Object.transform.parent;
        Debug.Log(Hit_Object);
        Hit_Object.SetActive(false);
        gameObject.SetActive(false);
    }
    [PunRPC]
    public void Box_Add(int hitObjectViewID)//, int TrainObjectViewID)
    {
        Hit_Object = PhotonView.Find(hitObjectViewID).gameObject;
        Debug.LogError("박스rpc");// + Hit_Object.name);
        GameObject box = PhotonNetwork.InstantiateSceneObject(Box.name, Hit_Object.transform.position, Quaternion.identity);
        box.transform.parent = Hit_Object.transform.parent;
        Hit_Object.SetActive(false);
        gameObject.SetActive(false);
    }
    

    public void GetHitObject(GameObject _hit_object)
    {

        Hit_Object = _hit_object;
        Hit_Object.transform.SetParent(Hit_Object.transform.parent);
    }

}
//object[] obj = new object[2];
//for(int i=0;i< TrainGameManager.instance.trainHP.Count; ++i)
//{
//    if(TrainGameManager.instance.trainHP[i] == Train_Object)
//    {
//        obj[0] = Train_Object;
//        obj[1] = Hit_Object;
//    }
//}