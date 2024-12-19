
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        // Check if the position is within the viewport
        if (position.x < 0 || position.x > camera.pixelWidth ||
            position.y < 0 || position.y > camera.pixelHeight)
        {
            Debug.LogWarning("Screen position is outside of the viewport area.");
            return Vector3.zero; // Or any other default value you want to use
        }

        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

}
