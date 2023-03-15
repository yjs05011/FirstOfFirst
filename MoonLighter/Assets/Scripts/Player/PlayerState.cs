using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    // 애니메이션 1프레임당 실제 시간을 계산한 상수
    public static float FramNumber { get { return 0.2f / 3f; } private set { } }
    //자식들의 애니메이션 동작 및 함수 실행을 위한 추상 함수
    public abstract void Action(ActState state);
}
