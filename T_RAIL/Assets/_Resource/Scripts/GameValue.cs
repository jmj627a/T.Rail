﻿

static class GameValue {


    // 카메라 셋팅
    public const float Mcam_initrot_x = 20.0f;
    public const int Mcam_initFOV = 60;

    public const float Mcam_changerot_x = 10.0f;
    public const int Mcam_changeFOV = 70;


    // layer int값
    public const int itembox_layer = 12;
    public const int passenger_layer = 13;
    public const int choice_layer = 14;
    public const int ladder_layer = 15;
    public const int machinegun_layer = 16;
    public const int sofa_layer = 17;
    public const int NextTrain_layer = 18;
    public const int PrevTrain_layer = 19;
    public const int floor2_layer = 20;
    public const int player_layer = 21;
    public const int bullet_layer = 22;
    public const int enemy_layer = 23;
    public const int train_layer = 24;

    // 기차 영역에 구성되어있는 오브젝트들의 값
    // local 좌표임
    public const float furnitureX = 0.7f;
    public const float furnitureY = 0.35f;


    // 기차의 기본 체력
    public const int Train_Standard_HP = 300;
    // 기차 속성의 기본값 
    public const float Durability = 100.0f;
    public const float speed = 10.0f;
    public const float noise = 100.0f;


    // 기차 간격
    public const float Train_distance = -13.0f;

    // 기차의 기본 위치
    public const float Train_y = 3.0f;
    public const float Train_z = -2.0f;


    public const int MaxTrainNumber = 13;


    public const float player_2f_position_y = 7.6f;
    public const float player_1f_position_y = 3.8f;


    // enemy1 
    public const int enemy1_FullHp = 100; // 한 텀

}
