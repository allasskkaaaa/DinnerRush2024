using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderNode : MonoBehaviour
{
    [SerializeField] public bool isOccupied = false;

    private void Update()
    {
        int childCount = transform.childCount;

        if (childCount > 0 )
        {
            isOccupied = true;
        }
        else
        {
            isOccupied = false;
        }
    }
}
