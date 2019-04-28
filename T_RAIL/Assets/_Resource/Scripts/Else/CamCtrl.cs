using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour {


    // 혹시 카메라 전환할때?

    Transform tr;

    float MouseSpeed;

    int player_floor;
    bool move_nextTrain { get; set; } // 다음칸으로
    bool CamSetMove { get; set; } // 카메라 움직일건지
	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        MouseSpeed = 3.0f;
	}

    private void LateUpdate()
    {
        if (CamSetMove)
        {
            float positionX = tr.position.x;

            switch (player_floor)
            {

                case 1:
                    if (move_nextTrain)
                    {

                    }
                    else
                    {
                        // ㅎ므
                    }
                    break;
                case 2:
                case 3:
                    positionX -= Input.GetAxis("Mouse X") * MouseSpeed;
                    break;
                default:
                    TrainGameManager.instance.Error_print();
                    break;

            }
        }

        // 범위제한
       // positionX = Mathf.Clamp("")
       // tr.Translate(Input.GetAxis("Mouse X") * 3.0f, 0 , 0);
       // 카메라 좌우 움직이는거
    }

    public void uptoCeiling()
    {
        this.GetComponent<Camera>().fieldOfView = GameValue.Mcam_changeFOV;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(GameValue.Mcam_changerot_x, 0, 0);
        tr.rotation = rot;
    }

    public void inTrain()
    {
        GetComponent<Camera>().fieldOfView = GameValue.Mcam_initFOV;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(GameValue.Mcam_initrot_x, 0, 0);
        tr.rotation = rot;
    }

    public void GetPlayerX()
    {
        // 얼마나 더 갈수있는지 제한하려고
        // 2층에서는 필요없음? 
        // 아니지
    }
}
