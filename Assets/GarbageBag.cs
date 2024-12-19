using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBag : MonoBehaviour
{
    [SerializeField] Vector3 minScale = new Vector3(0, 0, 0);
    [SerializeField] Vector3 maxScale = new Vector3(1, 1, 1);

    [SerializeField] Vector3 scaleSpeed = new Vector3( 0.1f, 0.1f, 0.1f);

    private bool isGrowing;

    private void Update()
    {
        // Scale the object progressively without exceeding maxScale
        if (transform.localScale.x < maxScale.x)
        {
            transform.localScale += new Vector3(scaleSpeed.x, 0, 0) * Time.deltaTime;
        }
        if (transform.localScale.y < maxScale.y)
        {
            transform.localScale += new Vector3(0, scaleSpeed.y, 0) * Time.deltaTime;
        }
        if (transform.localScale.z < maxScale.z)
        {
            transform.localScale += new Vector3(0, 0, scaleSpeed.z) * Time.deltaTime;
        }

        // Ensure scale doesn't go below minScale
        transform.localScale = new Vector3(
            Mathf.Max(transform.localScale.x, minScale.x),
            Mathf.Max(transform.localScale.y, minScale.y),
            Mathf.Max(transform.localScale.z, minScale.z)
        );

        // Clamp the scale to maxScale
        transform.localScale = new Vector3(
            Mathf.Min(transform.localScale.x, maxScale.x),
            Mathf.Min(transform.localScale.y, maxScale.y),
            Mathf.Min(transform.localScale.z, maxScale.z)
        );
    }

}
