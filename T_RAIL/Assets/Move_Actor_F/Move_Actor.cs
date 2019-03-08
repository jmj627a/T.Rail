using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct pos
{
    float x;
    float y;
    float z; // 여기를 float로 해야되는지 int로 해야되는지 몰라서 일단 float로
};

  enum Actor
    {
        Player = 1,
        Monster,
        Passenger

    };

//이 move_actor를 :monobehaviour를 상속할 것인가?
// 이거 상속할거면 virtual start(){ } 해서 상속받은 애들이 start나 update쓸 수 있는데
// 흠 근데 그럴거면...? 굳이?
// 얘네는 속성치 가지고 있는 애들로 생각하고 
// update같은 부분은 ctrl 스크립트를 따로 빼서 걔네로만 관리


public class Move_Actor
{
    Actor act;
    enum State { Idle, Walk, Run, Attack };

    public pos M_Pos { get; set; }
    public string Name { get; set; }
    public int HP { get; set; }
    public float speed { get; set; }vv

    public int now_anim { get; set; }

    public int Actor_State { get; set; }
    public int Actor_Property { get; set; } // 승객, 몬스터, 플레이어 구분

    public int Damage { get; set; } // 공격력
    public int Coin { get; set; } // 돈




   
    // Move_Actor 클래스의 생성자
    protected Move_Actor()
    {
        
    }


    virtual public void Init_Setting()
    {
     
        Name = null;
        HP = 0;
        Coin = 0;
    }


    protected virtual void Hit()
    {
        // 맞았을 때
    }
    protected virtual void Attack()
    {
        // 때릴 때
    }
    protected virtual void NameMake(string _name)
    {
        // 이름짓기 
        Name = _name;
    }

    //public virtual void Animate_Act()
    //{

    //}


    //public pos GetPos()
    //{
    //    return pos;
    //}
    //public void SetPos(Vector3 _set)
    //{
    //    m_Pos = _set;
    //}


}
