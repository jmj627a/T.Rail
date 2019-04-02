using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Train_Object : MonoBehaviour{


    public int HP { get; set; } //  (기차의 체력) 

    public Train_Object()
    {
        HP = GameValue.Train_Standard_HP; // 기차의 기본 체력
    }


}
