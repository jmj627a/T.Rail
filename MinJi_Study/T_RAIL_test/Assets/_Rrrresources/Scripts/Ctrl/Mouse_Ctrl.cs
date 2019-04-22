using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_Ctrl : MonoBehaviour
{

    // 마우스 컨트롤.
    // 말그대로 마우스로 클릭해서 하는 것들 관리
    // UI 제외

  //  float ScreenWidth;
  //  float ScreenHeight;

    public GameObject Inventory;
    public GameObject[] ChoiceButton;

    //   layerMask = (1 << LayerMask.NameToLayer("Furniture")); 

    private void Start()
    {
        
       // ScreenWidth = Screen.width;
       // ScreenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        // 근데 maincamera에 tag가 maincamera로 안달려있어서
        // 계속 null 오류가 떴었음

        // 그러면 결국 maincamera에 태그 달아줬는데
        // 이 ray가 카메라를 태그로 인식하는거면
        // 얘도 결국 태그연산 아니야?

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 만약에 마우스 클릭이 안된다?
            // max distance 200.0f을 mathf.infinity로 바꿔볼것
            //if (Physics.Raycast(ray, out hit))
            //{
            //   // Debug.Log(hit.collider.gameObject.layer);
            //}
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
                    ChoiceButton[0].SetActive(true);
                    ChoiceButton[0].transform.position = Input.mousePosition;
                    ChoiceButton[0].GetComponent<UI_ChoiceButton>().GetHitObject(hit.collider.gameObject);

                    ChoiceButton[1].SetActive(true);
                    ChoiceButton[1].transform.position = Input.mousePosition;
                    ChoiceButton[1].GetComponent<UI_ChoiceButton>().GetHitObject(hit.collider.gameObject);

                }
            }
        }
    }
}
