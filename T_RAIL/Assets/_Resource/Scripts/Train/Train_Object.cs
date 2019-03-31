

public class Train_Object{


    public int HP { get; set; } // 내구도 (기차의 체력) 
    public float speed { get; set; } // 현재 기차가 달리는 스피드 -> 맵에서 사용할거임
    public float noise { get; set; } // 현재 기차가 내는 소음


    public Train_Object()
    {
    }

    public Train_Object(int _hp, float _speed, float _noise)
    {

    }
}
