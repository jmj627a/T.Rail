using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

public class Pos
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    public Pos(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
}

public class Rot
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    public Rot(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
}
public class Move_Actor
{
    // 움직이는 것들의 기본이 되는 클래스

    Actor act;
    public enum A_State { Idle, Walk,Jump, Run, Attack };
   
    // 그리고 애니메이션 속도랑 얘 속도도 묶어

    public Pos position = new Pos(-1,3.3f,-2.5f); // 이거 이렇게 하면 안되는데.. 일단 나중에 수정
    public Rot rotate = new Rot(0, 180.0f, 0);

    public string Name { get; set; }
    public int HP { get; set; }
    public float speed { get; set; }

    public int Actor_State { get; set; } // A_State 값을 담을 변수
    public int Actor_Property { get; set; } // 승객, 몬스터, 플레이어 구분

    public int Damage { get; set; } // 공격력
    public int Coin { get; set; } // 돈

    public int Direction { get; set; } // 방향

    public int Where_Train { get; set; } // 기차의 몇번칸에 있는지? 
    // 몇ㅁ번칸에 있는지 해서 카메라 움직일거임
    // slerp써서 만약에 문이랑 충돌하면
    // 카메라는 다음 칸으로 slerp로 움직이고 (미리 저장된 position 값으로)
    // 플레이어도 그리로 가기 이거는 path 찍어서 움직이게 해도 되겠다.


    // Move_Actor 클래스의 생성자
    protected Move_Actor()
    {
    
    }


    virtual public void Init_Setting()
    {
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

    public virtual void Move(char key)
    {
        // 인자로 받는 키는 asdw를 구분하는 키
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
