using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DinoRun2D
{
    /// <summary>
    /// 공룡 캐릭터의 점프를 제어하는 스크립트.
    /// 스페이스바를 누르면 공룡이 점프하고, 바닥에 착지하면 다시 점프할 수 있다.
    /// </summary>
    public class DinoController : MonoBehaviour
    {
        /// <summary>점프할 때 위로 가해지는 힘의 크기</summary>
        [SerializeField] private float jumpForce;

        /// <summary>현재 공룡이 바닥에 닿아있는지 여부</summary>
        [SerializeField] private bool isGrounded;

        /// <summary>공룡이 숙이고 있는지 여부 (현재 미사용)</summary>
        [SerializeField] private bool isDown;

        /// <summary>바닥 감지 기준점 (공룡 발 아래에 위치시킨다)</summary>
        [SerializeField] private Transform groundCheckPoint;

        /// <summary>바닥으로 인식할 레이어 (Inspector에서 Ground 레이어 지정)</summary>
        [SerializeField] private LayerMask whatIsGround;

        /// <summary>바닥 감지 원형 영역의 반지름 (캐릭터 크기에 맞게 조절)</summary>
        [SerializeField] private float groundCheckRadius = 0.2f;

        /// <summary>달리기/점프 애니메이션 전환용 애니메이터</summary>
        private Animator anim;

        /// <summary>점프 시 물리 이동을 위한 Rigidbody2D</summary>
        private Rigidbody2D rb;

        /// <summary>애니메이터 파라미터 해시 캐싱 (문자열 비교보다 빠르고 오타 방지)</summary>
        private static readonly int IsGroundHash = Animator.StringToHash("isGround");

        void Start()
        {
            // 이 오브젝트에 붙어있는 Rigidbody2D, Animator 컴포넌트를 가져온다
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            // 게임 시작 시 바닥에 서 있는 상태로 초기화
            anim.SetBool(IsGroundHash, true);
        }

        void Update()
        {
            // 매 프레임마다 공룡 발 아래에 원형 영역을 그려서
            // Ground 레이어와 겹치는지 확인 → 바닥에 닿아있는지 판별
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            // 스페이스바를 눌렀고, 바닥에 있을 때만 점프 실행
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
                // 수평 속도는 유지하고, 수직 속도만 jumpForce로 설정하여 위로 튀어오른다
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            // 바닥 상태에 따라 애니메이션 전환 (달리기 ↔ 점프)
            anim.SetBool(IsGroundHash, isGrounded);
        }

        /// <summary>
        /// Scene 뷰에서 바닥 감지 범위를 빨간 원으로 표시하여 디버깅을 돕는다.
        /// (게임 실행에는 영향 없음, Editor 전용)
        /// </summary>
        void OnDrawGizmos() {
            // groundCheckPoint가 설정되지 않은 경우 에러 방지
            if (groundCheckPoint == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
