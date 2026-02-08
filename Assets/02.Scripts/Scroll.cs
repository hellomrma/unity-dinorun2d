using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 배경 스크롤을 담당하는 스크립트.
/// - 땅(Ground): 텍스처 오프셋을 이동시켜 땅이 흘러가는 효과를 만든다.
/// - 구름(Cloud): 오브젝트 자체를 왼쪽으로 이동시키고, 화면 밖으로 나가면 오른쪽에서 다시 등장한다.
/// </summary>
public class Scroll : MonoBehaviour
{
    /// <summary>땅 텍스처가 스크롤되는 속도</summary>
    public float groundScrollSpeedX = 2f;

    /// <summary>구름이 왼쪽으로 이동하는 속도 (Start에서 랜덤 설정)</summary>
    private float cloudScrollSpeedX;

    /// <summary>텍스처 오프셋 변경을 위한 렌더러 (땅에서 사용)</summary>
    private Renderer quadRenderer;

    /// <summary>true이면 구름 방식으로 스크롤, false이면 땅 방식으로 스크롤</summary>
    public bool isCloud;

    void Start()
    {
        quadRenderer = GetComponent<Renderer>();

        // 구름 오브젝트인 경우: 각 구름마다 서로 다른 속도를 부여하여 자연스러운 느낌을 준다
        if (gameObject.name.Contains("Cloud"))
        {
            // 0.05 ~ 0.09 사이의 랜덤 속도 (구름마다 속도가 달라 원근감 표현)
            cloudScrollSpeedX = Random.Range(0.05f, 0.09f);
        }
        else
        {
            cloudScrollSpeedX = 0.5f;
        }
    }

    void Update()
    {
        if (isCloud)
        {
            // 구름: 매 프레임 왼쪽으로 조금씩 이동
            transform.position = new Vector3(transform.position.x - cloudScrollSpeedX, transform.position.y, transform.position.z);

            // 화면 왼쪽 밖(x=-11)으로 나가면 → 오른쪽(x=11)에서 다시 등장
            // Y 위치를 랜덤(0.5~4)으로 바꿔서 매번 다른 높이에 나타나게 한다
            if (transform.position.x <= -11f)
            {
                transform.position = new Vector3(11f, Random.Range(0.5f, 4f), 0f);
            }
        }
        else
        {
            // 땅: 시간에 비례해서 텍스처 오프셋을 이동 → 땅이 흘러가는 착시 효과
            float offsetX = Time.time * groundScrollSpeedX;
            quadRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
        }
    }
}
