using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_SetFalse : MonoBehaviour {

    public float set_time;

    private void OnEnable()
    {
        Invoke("active_off", set_time);
    }
    void active_off()
    {
        this.gameObject.SetActive(false);
    }

}
