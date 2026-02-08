using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DinoRun2D
{
    /// <summary>
    /// 배경 스크롤을 담당하는 스크립트.
    /// - 땅(Ground): 텍스처 오프셋을 이동시켜 땅이 흘러가는 효과를 만든다.
    /// - 구름(Cloud): 오브젝트 자체를 왼쪽으로 이동시키고, 화면 밖으로 나가면 오른쪽에서 다시 등장한다.
    /// </summary>
    public class Scroll : MonoBehaviour
    {
        /// <summary>땅 텍스처가 스크롤되는 속도</summary>
        [SerializeField] private float groundScrollSpeedX = 2f;

        /// <summary>구름이 왼쪽으로 이동하는 속도 (Start에서 랜덤 설정)</summary>
        private float cloudScrollSpeedX;

        /// <summary>텍스처 오프셋 변경을 위한 렌더러 (땅에서 사용)</summary>
        private Renderer quadRenderer;

        /// <summary>true이면 구름 방식으로 스크롤, false이면 땅 방식으로 스크롤</summary>
        [SerializeField] private bool isCloud;

        /// <summary>구름이 사라지는 왼쪽 경계 X 좌표</summary>
        [SerializeField] private float leftBoundary = -11f;

        /// <summary>구름이 다시 나타나는 오른쪽 경계 X 좌표</summary>
        [SerializeField] private float rightBoundary = 11f;

        /// <summary>구름 재등장 시 최소 Y 높이</summary>
        [SerializeField] private float cloudMinY = 0.5f;

        /// <summary>구름 재등장 시 최대 Y 높이</summary>
        [SerializeField] private float cloudMaxY = 4f;

        /// <summary>Material 인스턴스 누수 방지를 위해 복제된 Material을 추적</summary>
        private Material materialInstance;

        void Start()
        {
            quadRenderer = GetComponent<Renderer>();

            // 구름 오브젝트인 경우: 각 구름마다 서로 다른 속도를 부여하여 자연스러운 느낌을 준다
            if (isCloud)
            {
                // 0.05 ~ 0.09 사이의 랜덤 속도 (구름마다 속도가 달라 원근감 표현)
                cloudScrollSpeedX = Random.Range(0.05f, 0.09f);
            }
            else
            {
                cloudScrollSpeedX = 0.5f;

                // 텍스처 오프셋 변경 시 Unity가 자동 복제하는 Material을 미리 캐싱
                materialInstance = quadRenderer.material;
            }
        }

        void Update()
        {
            if (isCloud)
            {
                // 구름: 매 프레임 왼쪽으로 조금씩 이동 (Time.deltaTime으로 프레임 독립적 이동)
                transform.position = new Vector3(
                    transform.position.x - cloudScrollSpeedX * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );

                // 화면 왼쪽 밖으로 나가면 → 오른쪽에서 다시 등장
                // Y 위치를 랜덤으로 바꿔서 매번 다른 높이에 나타나게 한다
                if (transform.position.x <= leftBoundary)
                {
                    transform.position = new Vector3(rightBoundary, Random.Range(cloudMinY, cloudMaxY), 0f);
                }
            }
            else
            {
                // 땅: 시간에 비례해서 텍스처 오프셋을 이동 → 땅이 흘러가는 착시 효과
                float offsetX = Time.time * groundScrollSpeedX;
                materialInstance.mainTextureOffset = new Vector2(offsetX, 0);
            }
        }

        /// <summary>
        /// 오브젝트 파괴 시 복제된 Material 인스턴스를 정리하여 메모리 누수를 방지한다.
        /// </summary>
        void OnDestroy()
        {
            if (materialInstance != null)
            {
                Destroy(materialInstance);
            }
        }
    }
}
