using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{


    // 혹시 카메라 전환할때?

    Transform tr;

    float MouseSpeed;

    int player_floor;
    bool move_nextTrain { get; set; } // 다음칸으로
    bool CamSetMove { get; set; } // 카메라 움직일건지

    float player_position_x;
    float floor2_MinX;
    bool position_once; // 잉ㅁ시ㅏ방편

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        MouseSpeed = 0.5f;
    }

    //private void FixedUpdate()
    //{
    //    if (player_floor.Equals(1))
    //    {
    //        float targetX = tr.position.x;

    //        if (Mathf.Abs(tr.position.x - player_position_x) > 3)
    //        {
    //            targetX = Mathf.Lerp(tr.position.x, player_position_x, 15.0f * Time.deltaTime);
    //        }
    //        tr.position = new Vector3(targetX, tr.position.y, tr.position.z);
    //    }
    //}
    private void LateUpdate()
    {

        float positionX = tr.position.x;

        switch (player_floor)
        {



            case 1:

                float targetX = tr.position.x;

               // if (Mathf.Abs(tr.position.x - player_position_x) > 3)
                {
                    targetX = Mathf.Lerp(tr.position.x, player_position_x, 15.0f * Time.deltaTime);
                }
                tr.position = new Vector3(targetX, tr.position.y, tr.position.z);

                position_once = true;
                break;
            case 2:
            case 3:
                // 얘는 마우스임 
                // 혹시몰라서 일단 남겨놓기는 함
                if (position_once)
                {
                    floor2_MinX = player_position_x;
                    position_once = false;
                }

                positionX += Input.GetAxis("Mouse X") * MouseSpeed;
                positionX = Mathf.Clamp(positionX, floor2_MinX - 12.0f, floor2_MinX + 3.0f);
                float temp_x;
                temp_x = Mathf.Lerp(positionX, tr.position.x, Time.deltaTime);
                tr.position = new Vector3(temp_x, tr.position.y, tr.position.z);

                break;
            default:
                TrainGameManager.instance.Error_print();
                break;

        }
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

       // Cam_1FloorInitPosition();
    }

    public void GetPlayerX(float position_x)
    {
        player_position_x = position_x;
    }

    public void Change_floor(int _floor)
    {
        player_floor = _floor;
    }


}
