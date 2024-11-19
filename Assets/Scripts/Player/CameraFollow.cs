using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minXClamp;
    public float maxXClamp;
    public float minYClamp;
    public float maxYClamp;
    public float xOffset;
    public float yOffset;
    private void LateUpdate()
    {
        if (GameManager.Instance.PlayerInstance == null) return;

        Vector3 cameraPos;

        cameraPos = transform.position;
        cameraPos.x = Mathf.Clamp(GameManager.Instance.PlayerInstance.transform.position.x + xOffset, minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(GameManager.Instance.PlayerInstance.transform.position.y + yOffset, minYClamp, maxYClamp);
        transform.position = cameraPos;

    }
}
