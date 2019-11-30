using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame : MonoBehaviour
{
    private string level;
    private int round;

    private void SetData(string lev, int r)
    {
        level = FindObjectOfType<Database>().GetLevel();
        round = FindObjectOfType<Database>().GetRoundNum();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
