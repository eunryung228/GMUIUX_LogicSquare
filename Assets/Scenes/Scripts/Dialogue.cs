using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject m_layer;
    public Text m_text_script;
    public Text m_text_speaker;
    public GameObject m_img_speaker;
    public GameObject m_scriptWindow;

    private string m_npcName;
    private string m_scriptName;
    private int m_scriptNum;
    private bool m_isFinished;
    private bool m_isSkip;
    private int count;

    public string keyboardSound_1;
    public string keyboardSound_2;
    public string keyboardSound_3;

    public List<string> listSentences = new List<string>();
    public List<string> listSpeakers = new List<string>();

    private AudioManager mgrAudio;


    public void SetScripts(string npcName, string scriptName, int script_number)
    {
        m_npcName = npcName;
        m_text_speaker.text = m_npcName;
        m_scriptName = scriptName;
        m_scriptNum = script_number;

        m_img_speaker.GetComponent<Image>().sprite = Resources.Load<Sprite>("Character/" + m_npcName) as Sprite;
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
        m_text_speaker.text = listSpeakers[count];
        m_layer.SetActive(true);

        for (int i = 0; i < listSentences[count].Length; i++)
        {
            if (m_isSkip)
            {
                m_text_script.text= listSentences[count];
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
    }

    void Start()
    {
        mgrAudio = FindObjectOfType<AudioManager>();

        m_text_script.text = "";
        m_text_speaker.text = "";
        m_img_speaker.SetActive(false);
        m_scriptWindow.SetActive(false);
        m_layer.SetActive(false);
        count = 0;
        m_isFinished = false;
        m_isSkip = false;
}

    void Update()
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
}
