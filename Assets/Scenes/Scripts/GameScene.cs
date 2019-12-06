using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    private GameObject[] m_gg = new GameObject[3];
    private Image m_boss;
    private Text[] m_text_gg = new Text[3];
    private Text m_text_d;

    private string level;

    public GameObject m_layer;

    private Database mgrDB;
    private Dialogue mgrDial;


    public void onClickEasyButton()
    {
        m_text_d.text = "";
        level = "Easy";
        m_boss.sprite = Resources.Load<Sprite>("Character/웨어울프") as Sprite;

        for (int i = 0; i < 3; i++)
        {
            m_text_gg[i].text = "5x5";
            if (!m_gg[i].activeSelf)
                m_gg[i].SetActive(true);
            SetResource(m_gg[i], mgrDB.m_levels[0].isComplete[i]);
        }
    }
    public void onClickNormalButton()
    {
        m_text_d.text = "";
        level = "Normal";
        m_boss.sprite = Resources.Load<Sprite>("Character/악마") as Sprite;

        for (int i = 0; i < 3; i++)
        {
            m_text_gg[i].text = "8x8";
            if (!m_gg[i].activeSelf)
                m_gg[i].SetActive(true);
            SetResource(m_gg[i], mgrDB.m_levels[0].isComplete[i]);
        }
    }
    public void onClickHardButton()
    {
        m_text_d.text = "";
        level = "Hard";
        m_boss.sprite = Resources.Load<Sprite>("Character/뱀파이어") as Sprite;

        for (int i = 0; i < 3; i++)
        {
            m_text_gg[i].text = "10x10";
            if (!m_gg[i].activeSelf)
                m_gg[i].SetActive(true);
            SetResource(m_gg[i], mgrDB.m_levels[0].isComplete[i]);
        }
    }
    public void onClickDragonButton()
    {
        m_boss.sprite = null;
        level = "Dragon";
        m_text_d.text = "최후의 결전";

        for (int i = 0; i < 3; i++)
        {
            m_text_gg[i].text = "";
            m_gg[i].SetActive(false);
        }
        m_gg[1].SetActive(true);
    }

    private void SetResource(GameObject gg, int heartNum)
    {
        gg.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("etc/game_ground_h" + heartNum.ToString()) as Sprite;
    }

    private void SetActive(string par, bool isActive)
    {
        GameObject parent = GameObject.Find(par);
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }

    void Start()
    {
        mgrDial = FindObjectOfType<Dialogue>();
        mgrDB = FindObjectOfType<Database>();

        level = "Easy";

        m_boss = GameObject.Find("Img_Boss").GetComponent<Image>();
        m_text_d = GameObject.Find("Text_RoundD").GetComponent<Text>();
        m_text_d.text = "";

        GameObject parent= GameObject.Find("Obj_GG");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            m_gg[i] = parent.transform.GetChild(i).gameObject;
        }
        parent = GameObject.Find("Text_Round");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            m_text_gg[i] = parent.transform.GetChild(i).gameObject.GetComponent<Text>();
        }
    }

    void Update()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

        if (hit && Input.GetMouseButtonDown(0) && !m_layer.activeSelf)
        {
            if (hit.transform.gameObject.tag == "start")
            {
                mgrDB.SetData(level, hit.transform.gameObject.name[11]);
                SceneManager.LoadSceneAsync("Ingame");
            }
        }
    }
}