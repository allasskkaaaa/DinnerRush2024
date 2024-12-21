using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float highScore;

    public PlayerData (GameManager manager)
    {
        highScore = manager.highScore;
    }
}
