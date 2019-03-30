using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Ctrl : MonoBehaviour
{

    // 마우스 컨트롤.
    // 말그대로 마우스로 클릭해서 하는 것들 관리
    // UI 제외

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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // 만약 layer 가 상자이면?



                // 만약 layer가 승객이면?
            }
        }
    }
}
