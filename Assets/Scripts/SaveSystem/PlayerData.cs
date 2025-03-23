using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float highScore;
    public int money;

    public PlayerData (GameManager manager)
    {
        highScore = manager.highScore;
        money = manager.money;
    }


}
