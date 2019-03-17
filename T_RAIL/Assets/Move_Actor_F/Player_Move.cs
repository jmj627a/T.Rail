using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {


    Player_Actor player1 = new Player_Actor();

	// Update is called once per frame
	void Update () {

        // switch- case는 쓰는데
        // 키 동시에 입력되면?

















        if (Input.GetKeyDown(KeyCode.A))
        {
            // x = -1
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // x = +1  
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // y = -1
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // y = +1
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // 공격
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Jump
        }

        if (Input.GetKey(KeyCode.C))
        {
            // 카메라 전환 
        }

    }
}
