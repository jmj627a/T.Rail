using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class UI_ChoiceButton : MonoBehaviourPunCallbacks
{

    // 이건 오브젝트 풀링으로 바꾸자
    // 일단은 active(false)해놓은거 하겠지만ㄴ
    public GameObject Sofa;
    public GameObject Box;

    public GameObject Hit_Object;
    public Transform Train_Object;
    
    Vector3 Sofa_Rotation = new Vector3(-90, -80, 0);

    // 그리고 이 choicebutton도 ui script로 옮기기
    //버튼을 누르면 이 함수를 호출해서, 이 안에서 각 이벤트 함수를 또 호출
    public void onChoiceButton(int kind)
    {
        if (kind == 0)
        {
            photonView.RPC("Sofa_Add", RpcTarget.AllBuffered, Hit_Object.GetPhotonView().ViewID);
        }
        else if (kind == 1)
        {
            photonView.RPC("Box_Add", RpcTarget.AllBuffered, Hit_Object.GetPhotonView().ViewID);
        }
    }

    [PunRPC]
    void Sofa_Add(int hitObjectViewID)
    {
        Hit_Object = PhotonView.Find(hitObjectViewID).gameObject;

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = Sofa_Rotation;
    
        //포톤함수로 수정
        GameObject sofa = PhotonNetwork.InstantiateSceneObject(Sofa.name, Hit_Object.transform.position, rot);
        //sofa.transform.parent = Train_Object;
        Hit_Object.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false); //ui버튼 들어있던 것 부모로 한번 더 감싸서 getchild추가
    }

    [PunRPC]
    void Box_Add(int hitObjectViewID)
    {
        Hit_Object = PhotonView.Find(hitObjectViewID).gameObject;
        GameObject box = PhotonNetwork.InstantiateSceneObject(Box.name, Hit_Object.transform.position, Quaternion.identity);
        //box.transform.parent = Train_Object;
        Hit_Object.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false); //ui버튼 들어있던 것 부모로 한번 더 감싸서 getchild추가
    }

    public void GetHitObject(GameObject hit_object)
    {

        Hit_Object = hit_object;
        Hit_Object.transform.SetParent(Hit_Object.transform.parent);
    }

}
