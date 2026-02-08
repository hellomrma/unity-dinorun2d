using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    // Ground 와 Cloud 을 제어. Ground 는 Offset 값을 조절하고 Cloud 는 X값 조절

    public float groundScrollSpeedX = 2f;
    private float cloudScrollSpeedX;
    private Renderer quadRenderer;

    public bool isCloud;

    // Start is called before the first frame update
    void Start()
    {
        quadRenderer = GetComponent<Renderer>();

        // 오브젝트의 이름이 Cloud, Cloud2, Cloud3, Cloud4 인 경우 cloudScrollSpeedX 값을 0.05f ~ 0.09f 사이 랜덤으로 설정.
        // 이외의 경우 cloudScrollSpeedX 값을 0.5f로 설정.
        if (gameObject.name.Contains("Cloud"))
        {
            cloudScrollSpeedX = Random.Range(0.05f, 0.09f);
        }
        else
        {
            cloudScrollSpeedX = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCloud)
        {
            // Cloud 의 X 값을 -cloudScrollSpeedX씩 감소 시킨다.
            transform.position = new Vector3(transform.position.x - cloudScrollSpeedX, transform.position.y, transform.position.z);
            // 만약에 X 값이 -11이 되면 11로 이동.
            // 이때 Y 값은 0.5 ~ 4 사이 랜덤으로 설정. 그리고 Z 값은 0.
            if (transform.position.x <= -11f)
            {
                transform.position = new Vector3(11f, Random.Range(0.5f, 4f), 0f);
            }
        }
        else
        {
            float offsetX = Time.time * groundScrollSpeedX;
            quadRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
        }
    }
}
