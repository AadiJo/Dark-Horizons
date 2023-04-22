using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    Vector2 targetPos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        targetPos = Input.mousePosition;
        transform.position = new Vector2(targetPos.x + 16f, targetPos.y - 16f);

    }
}
