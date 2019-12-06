using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ingame : MonoBehaviour
{
    public GameObject m_layer;
    public Text m_text_script;
    public Text m_text_speaker;
    public GameObject m_img_speaker;
    public GameObject m_scriptWindow;

    public GameObject m_gameover;
    public GameObject[] m_heartList = new GameObject[3];
    public Text m_text_time;

    private string m_npcName;
    private string m_scriptName;
    private int m_scriptNum;
    private bool m_isFinished;
    private bool m_isSkip;
    private bool m_isIng = false;
    private bool m_isEnd = false;
    private int count;

    public string keyboardSound_1;
    public string keyboardSound_2;
    public string keyboardSound_3;

    public List<string> listSentences = new List<string>();
    public List<string> listSpeakers = new List<string>();

    private AudioManager mgrAudio;
   

    private int[] m_heart = new int[3];
    private bool isFinished = false;

    private string level;
    private int round;
    private float roundTime;
    private float time;

    private int m_rightNum;
    private int m_currNum = 0;

    public string m_clickButton;
    public string m_clickFalse;


    public void SetScripts(string npcName, string scriptName, int script_number)
    {
        count = 0;

        m_npcName = npcName;
        m_text_speaker.text = m_npcName;
        m_scriptName = scriptName;
        m_scriptNum = script_number;

        m_layer.SetActive(true);

        m_img_speaker.SetActive(true);
        m_scriptWindow.SetActive(true);

        List<LoadJson.Script> scripts = LoadJson.scriptDic[m_scriptName];
        for (int i = 0; i < scripts[script_number].InnerScripts.Count; i++)
        {
            listSentences.Add(scripts[script_number].InnerScripts[i].script);
            listSpeakers.Add(scripts[script_number].InnerScripts[i].name);
        }

        StartCoroutine(StartScriptsCoroutine());
    }

    IEnumerator StartScriptsCoroutine()
    {
        m_isFinished = false;

        for (int i = 0; i < listSentences[count].Length; i++)
        {
            m_text_speaker.text = listSpeakers[count];
            m_img_speaker.GetComponent<Image>().sprite = Resources.Load<Sprite>("Character/" + listSpeakers[count]) as Sprite;

            if (m_isSkip)
            {
                m_text_script.text = listSentences[count];
                m_isSkip = false;
                break;
            }

            m_text_script.text += listSentences[count][i];
            if (i == 0)
                mgrAudio.Play(keyboardSound_1);
            if (i % 6 == 1)
                mgrAudio.Play(keyboardSound_2);
            else if (i % 6 == 4)
                mgrAudio.Play(keyboardSound_3);
            yield return new WaitForSeconds(0.03f);
        }
        m_isFinished = true;
    }

    public void ExitScripts()
    {
        count = 0;
        m_text_script.text = " ";
        m_text_speaker.text = " ";
        listSentences.Clear();
        listSpeakers.Clear();
        m_layer.SetActive(false);
        m_scriptWindow.SetActive(false);
        m_img_speaker.SetActive(false);
        m_isFinished = false;

        if (m_isEnd)
            m_layer.SetActive(true);
    }

    public void SetLevel(string lev, float rt)
    {
        level = lev;
        roundTime = rt;
    }
    public void SetRightNum(int num)
    {
        m_rightNum = num;
    }

    public string GetLevel()
    {
        return level;
    }

    private void SetHeart()
    {
        if (m_heart[2] == 1)
        {
            m_heartList[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/heart_erase") as Sprite;
            m_heart[2] = 0;
        }
        else if (m_heart[1] == 1)
        {
            m_heartList[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/heart_erase") as Sprite;
            m_heart[1] = 0;
        }
        else if (m_heart[0] == 1)
        {
            m_heartList[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/heart_erase") as Sprite;
            m_heart[0] = 0;
            m_gameover.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Start()
    {
        m_layer.SetActive(true);
        mgrAudio = FindObjectOfType<AudioManager>();
        level = FindObjectOfType<Database>().GetLevel();

        SetScripts("뱀파이어", "hard", 0);

        m_heart = new int[] { 1, 1, 1 };
    }
   
    void Update()
    {
        if (!m_layer.activeSelf && !m_gameover.activeSelf)
        {
            time += Time.deltaTime;
            m_text_time.text = (roundTime - Mathf.Ceil(time)).ToString();

            if ((roundTime - Mathf.Ceil(time)) <= 0)
                m_gameover.SetActive(true);
        }

        Debug.Log(m_currNum);
        if (m_currNum == (m_rightNum / 2) && !m_isIng)
        {
            m_isIng = true;
            SetScripts("뱀파이어", "hard", 1);
            roundTime -= 30;
        }
        if (m_currNum == m_rightNum && !m_isEnd)
        {
            m_isEnd = true;
            Debug.Log("Clear!");
            SetScripts("뱀파이어", "hard", 2);
        }

        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

        if(m_layer.activeSelf)
        {
            if (count != 0 && Input.GetMouseButtonDown(0) && !m_isFinished)
                m_isSkip = true;
            else if (Input.GetMouseButtonDown(0) && m_isFinished)
            {
                m_isSkip = false;
                Vector3 temp = Input.mousePosition;
                if (temp.x <= Screen.width && temp.y <= Screen.height * 0.28)
                {
                    count++;
                    m_text_script.text = "";
                    if (count >= listSentences.Count)
                    {
                        StopAllCoroutines();
                        ExitScripts();
                    }
                    else
                    {
                        StopAllCoroutines();
                        StartCoroutine(StartScriptsCoroutine());
                    }
                }
            }
        }

        if (hit && !m_layer.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.gameObject.CompareTag("right"))
                {
                    hit.transform.gameObject.tag = "finish";
                    mgrAudio.Play(m_clickButton);
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("etc/block_t") as Sprite;
                    m_currNum += 1;
                }
                else if (hit.transform.gameObject.CompareTag("false"))
                {
                    hit.transform.gameObject.tag = "finish";
                    mgrAudio.Play(m_clickFalse);
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("etc/block_f") as Sprite;
                    SetHeart();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (!hit.transform.gameObject.CompareTag("finish")) // finish가 아니면
                {
                    if (hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite.name == "block_temp") // 이미 체크해놓은 블럭 -> 원상복구
                    {
                        mgrAudio.Play(m_clickButton);
                        hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("etc/block") as Sprite;
                    }
                    else
                    {
                        mgrAudio.Play(m_clickButton);
                        hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("etc/block_temp") as Sprite;
                    }
                }
            }
        }
    }
}
