using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBlocks : MonoBehaviour
{
    public GameObject blockPrefab;

    private int[] round;

    string level;

    private int rightNum = 0;

    private int num;
    private float scale;
    private float block_size;


    private void SetBlock()
    {
        level = "Hard";
        //level = FindObjectOfType<Database>().GetLevel();
        if (level == "Easy")
        {
            round = new int[] { 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0 };
            foreach (var item in round)
            {
                if (item == 1)
                    rightNum += 1;
            }
            num = 5;
            scale = 1.25f;
            block_size = 0.95f;
        }
        else if (level == "Hard")
        {
            Debug.Log(level);
            GetComponent<Ingame>().SetLevel(level, 180);

            round = new int[] { 0,0,0,1,1,1,1,0,0,1,1,0,1,0,0,0,0,1,0,0,
                0,0,1,1,0,1,0,1,0,0,
            0,0,1,0,0,0,0,1,0,1,
                1,0,0,1,1,1,1,0,0,0,0,0,1,0,0,0,0,1,0,0,0,1,0,0,1,0,0,0,1,0,
             0,1,0,0,0,1,0,0,1,0,
                0,1,0,0,0,0,0,0,1,0,
                0,0,1,1,1,1,1,1,0,0};
            foreach (var item in round)
            {
                if (item == 1)
                    rightNum += 1;
            }
            GetComponent<Ingame>().SetRightNum(rightNum);

            num = 10;
            scale = 1f;
            block_size = 0.78f;
        }
    }

    public void MakingBlocks()
    {
        int count = 0;
        GameObject block;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                block = Instantiate(blockPrefab) as GameObject;
                block.transform.localScale = new Vector3(scale, scale, 0);
                block.transform.position = new Vector3(-1 * Mathf.Floor(num / 2) * block_size + block_size * j, Mathf.Floor(num / 2) * block_size - block_size * (i + 1), 0);
                if (round[count] == 1)
                    block.tag = "right";
                else
                    block.tag = "false";
                count++;
            }
        }
    }

    void Start()
    {
        SetBlock();
        MakingBlocks();
    }

    void Update()
    {
    }
}