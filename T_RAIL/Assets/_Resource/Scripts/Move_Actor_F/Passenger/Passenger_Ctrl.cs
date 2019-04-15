using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger_Ctrl : MonoBehaviour {


    Passenger_Actor pass;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
