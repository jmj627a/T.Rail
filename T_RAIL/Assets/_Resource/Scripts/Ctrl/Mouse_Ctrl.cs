using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Ctrl : MonoBehaviour {

    // 마우스 컨트롤.
    // 말그대로 마우스로 클릭해서 하는 것들 관리
    // UI 제외
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {

        }
	}
}
