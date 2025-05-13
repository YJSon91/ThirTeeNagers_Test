using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // 아이템이 생성된 후 5초가 지나면 자동으로 파괴됨 (플레이어가 안 먹었을 경우 대비)
        Destroy(gameObject, 5f);
    }
}
