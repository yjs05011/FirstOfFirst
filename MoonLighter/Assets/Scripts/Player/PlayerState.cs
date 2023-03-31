using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    // 애니메이션 1프레임당 실제 시간을 계산한 상수 , 
    // 24 프레임이 1초 이므로 1/24 = b, 이 중 에니메이션의 길이가 16 이므로 1/16 = a, a : b = 1:x, b = ax ,  x = 0.066순환소수가 나온다
    // 이는 밑에 있는 0.2f/ 3f  와 같으므로 이와같이 계산했다.
    public static float FramNumber { get { return 0.2f / 3f; } private set { } }
    //자식들의 애니메이션 동작 및 함수 실행을 위한 추상 함수
    public abstract void Action(ActState state);
}
