using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장애물의 이동과 자동 삭제를 담당하는 스크립트.
/// 각 장애물 프리팹에 부착되며, 생성 직후부터 왼쪽으로 일정 속도로 이동한다.
/// 카메라 화면 밖으로 완전히 나가면 오브젝트를 자동으로 파괴해 메모리를 절약한다.
/// </summary>
public class ObstacleController : MonoBehaviour
{
    /// <summary>
    /// 장애물이 이동하는 X축 속도. 음수 값을 설정하면 왼쪽으로 이동한다.
    /// (Inspector에서 설정)
    /// </summary>
    public float moveSpeedX;

    /// <summary>물리 기반 이동을 위한 Rigidbody2D 컴포넌트 참조</summary>
    private Rigidbody2D rb;

    /// <summary>
    /// [Unity 내장] 첫 프레임 직전에 한 번 호출된다.
    /// 이 오브젝트에 붙어있는 Rigidbody2D 컴포넌트를 가져와 캐싱한다.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// [Unity 내장] 매 프레임 호출된다.
    /// Rigidbody2D의 velocity를 직접 설정해 장애물을 왼쪽으로 이동시킨다.
    /// Y축 속도는 0으로 고정해 중력 영향을 받지 않고 수평 이동만 한다.
    /// </summary>
    void Update()
    {
        rb.velocity = new Vector2(moveSpeedX, 0);
    }

    /// <summary>
    /// [Unity 내장] 이 오브젝트의 Renderer가 카메라 화면 밖으로 완전히 벗어날 때 호출된다.
    /// Destroy(gameObject)로 이 장애물 오브젝트를 씬에서 제거해 불필요한 메모리 낭비를 막는다.
    /// 반대 이벤트로 OnBecameVisible(화면 안으로 들어올 때)도 존재한다.
    /// </summary>
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
