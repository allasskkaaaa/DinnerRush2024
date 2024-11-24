using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SpawnPlayer(spawn);
    }
}
