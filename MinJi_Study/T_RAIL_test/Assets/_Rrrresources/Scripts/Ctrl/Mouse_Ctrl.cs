using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Mouse_Ctrl : MonoBehaviourPunCallbacks
{
    public GameObject Inventory;
    public UI_ChoiceButton ChoiceButton;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 아... Equals("12") 라고 해서 계속 안됐던거임 흑흑 젠장
                if (hit.collider.gameObject.layer.Equals(GameValue.itembox_layer))
                {
                    // 상자일 경우!
                    Inventory.SetActive(true);
                    // Vector3 m_Position = Input.mousePosition;

                    Inventory.transform.position = Input.mousePosition;
                    //new Vector3(m_Position.x, m_Position.y, m_Position.z);
                }

                else if (hit.collider.gameObject.layer.Equals(GameValue.passenger_layer))
                {
                    // 승객일 경우 
                }
                else if (hit.collider.gameObject.layer.Equals(GameValue.choice_layer))
                {
                    ChoiceButton.gameObject.SetActive(true);
                    ChoiceButton.transform.position = Input.mousePosition;
                    //ChoiceButton.GetComponent<UI_ChoiceButton>().GetHitObject(hit.collider.gameObject);
                    Debug.Log(hit.collider.gameObject.name + "  dkdkdkkdkdk   ");// + hit.collider.transform.root.gameObject.name);
                    photonView.RPC("getHitObjectRPC", RpcTarget.AllBuffered ,hit.collider.gameObject.GetPhotonView().ViewID);
                }
            }
        }
    }

    [PunRPC]
    public void getHitObjectRPC(int hit_object_viewID)
    {
        ChoiceButton.GetHitObject(PhotonView.Find(hit_object_viewID).gameObject);
    }
}
