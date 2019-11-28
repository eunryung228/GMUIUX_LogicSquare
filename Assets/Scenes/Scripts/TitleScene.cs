using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public Text m_helpText;
    public string btnSound;

    private AudioManager mgrAudio;


    public void OnClickStartButton()
    {
        mgrAudio.Play(btnSound);
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickExitButton()
    {
        mgrAudio.Play(btnSound);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");
#else
        Application.Quit();
#endif
    }

    public void OnClickHelpButton()
    {
        mgrAudio.Play(btnSound);
        SetActive("Title", false);
        SetActive("Story", true);
        m_helpText.text = "도움말";
    }

    public void OnClickNextButton()
    {
        mgrAudio.Play(btnSound);
        SetActive("Story", false);
        SetActive("Help", true);
    }

    public void OnClickPreviousButton()
    {
        mgrAudio.Play(btnSound);
        SetActive("Story", true);
        SetActive("Help", false);
    }

    public void OnClickBackButton()
    {
        mgrAudio.Play(btnSound);
        SetActive("Help", false);
        SetActive("Title", true);
        m_helpText.text = "";
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
        mgrAudio = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
    }
}
