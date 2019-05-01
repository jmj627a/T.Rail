using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrainCollider : MonoBehaviour {


    public GameObject Wall; // 카메라에 보이는 벽


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer.Equals(GameValue.player_layer))
        {
            Wall.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer.Equals(GameValue.player_layer))
        {
            Wall.SetActive(true);
        }
    }
}
