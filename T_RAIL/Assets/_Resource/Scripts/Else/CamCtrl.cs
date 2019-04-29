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


    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        MouseSpeed = 0.5f;
    }

    private void LateUpdate()
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
                // 얘는 마우스임 
                // 혹시몰라서 일단 남겨놓기는 함
                positionX += Input.GetAxis("Mouse X") * MouseSpeed;
                positionX = Mathf.Clamp(positionX, player_position_x - 5.0f, player_position_x + 5.0f);
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

        Cam_1FloorInitPosition();
    }

    public void GetPlayerX(float position_x)
    {
        player_position_x = position_x;
    }

    public void Change_floor(int _floor)
    {
        player_floor = _floor;
    }

    public void Camera_LeftSide()
    {
        // 왼쪽으로 

        
    }
    public void Camera_standSide()
    {
        // 기본으로 


    }

    public void Cam_1FloorInitPosition()
    {
        // 2층에서 카메라 옮기다가 1층 내려오면 거기에 고정.
        // 1층 camera position 다시 셋팅하는 함수

        // 이것도 호출하면 뭐 bool되게 해서 부드럽게 내려가게 해야될듯
    }

    public void NextTrain_CamPositionChange(int index)
    {
        // index -> 몇번 기차로 넘어갔는지
    }

    public void PrevTrain_CamPositionChange(int index)
    {
        // index -> 몇번 기차로 넘어갔는지
    }
}
