using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChoiceButton : MonoBehaviour {



    // 이건 오브젝트 풀링으로 바꾸자
    // 일단은 active(false)해놓은거 하겠지만ㄴ
    public GameObject Sofa;
    public GameObject Box;

    GameObject Hit_Object;
    Transform Train_Object;

    Vector3 Sofa_Rotation = new Vector3(-90, -80, 0);
    // 그리고 이 choicebutton도 ui script로 옮기기
    public void Sofa_Add()
    {

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = Sofa_Rotation;

        GameObject sofa = Instantiate(Sofa, Hit_Object.transform.position, rot);
        sofa.transform.parent = Train_Object;
        Hit_Object.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
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
