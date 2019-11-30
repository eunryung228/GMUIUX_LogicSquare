using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject m_layer;
    private bool isFinished;

    void Start()
    {
        isFinished = false;
        FindObjectOfType<Dialogue>().SetScripts("유령집사", "tutorial", 0);
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

        if (hit && Input.GetMouseButtonDown(0) && !m_layer.activeSelf)
        {
            if(hit.transform.gameObject.tag=="start")
            {
                GameObject.Find("game_ground").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("etc/game_ground_h3") as Sprite;
                FindObjectOfType<Dialogue>().SetScripts("유령집사", "tutorial", 1);
                isFinished = true;
            }
        }

        if (isFinished && m_layer.activeSelf == false)
            SceneManager.LoadScene("GameScene");
    }
}