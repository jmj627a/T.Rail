using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger_Actor : Move_Actor
{
    // 승객용
    public int Hungry { get; set; } // 배고픔
    public int Disease { get; set; } // 질병



    Passenger_Actor()
    {
        base.Actor_Property = (int)Actor.Passenger; // property에 승객이라고 정의
       // base.Coin = Random.Range(0,)
    }

}
