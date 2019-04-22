using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UI_ChoiceButton : MonoBehaviourPunCallbacks {



    // 이건 오브젝트 풀링으로 바꾸자
    // 일단은 active(false)해놓은거 하겠지만ㄴ
    public GameObject Sofa;
    public GameObject Box;

    GameObject Hit_Object;
    Transform Train_Object;

    Vector3 Sofa_Rotation = new Vector3(-90, -80, 0);
    // 그리고 이 choicebutton도 ui script로 옮기기



    public void onChoiceButton(int kind)
    {
        if(kind == 0)
        {
            Debug.Log("소파클릭");
            photonView.RPC("Sofa_Add", RpcTarget.All);
        }
        else if(kind == 1)
        {
            Debug.Log("박스클릭");
            photonView.RPC("Box_Add", RpcTarget.All);
        }
    }


    [PunRPC]
    public void Sofa_Add()
    {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = Sofa_Rotation;

        GameObject sofa = Instantiate(Sofa, Hit_Object.transform.position, rot);
        sofa.transform.parent = Train_Object;
        Hit_Object.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    [PunRPC]
    public void Box_Add()
    {
        GameObject box = Instantiate(Box, Hit_Object.transform.position, Quaternion.identity);
        box.transform.parent = Train_Object;
        Hit_Object.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void GetHitObject(GameObject hit_object)
    {

        Hit_Object = hit_object;
        Train_Object = Hit_Object.transform.parent;
    }

}
