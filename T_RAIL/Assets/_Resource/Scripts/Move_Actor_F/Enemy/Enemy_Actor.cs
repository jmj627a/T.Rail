using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Actor : Move_Actor {

	// Use this for initialization
	void Start () {
        base.Actor_Property = (int)Actor.Monster;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    // 여기서 position 다 
}
