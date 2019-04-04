using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    #region private 

    Transform thisObject;

    #endregion

    // Use this for initialization
    void Start () {

        thisObject = this.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update () {
        playerKeyInput();
    }


    void playerKeyInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            thisObject.position += new Vector3(0, 0, 5) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            thisObject.position += new Vector3(-5, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            thisObject.position += new Vector3(0, 0, -5) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            thisObject.position += new Vector3(5, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey (KeyCode.Space))
        {
            thisObject.position += new Vector3(0, 5, 0) * Time.deltaTime;
        }
    }
}
