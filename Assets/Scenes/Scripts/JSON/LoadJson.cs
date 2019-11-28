using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

[System.Serializable]
public class LoadJson : MonoBehaviour
{
    void Start()
    {
        LoadScript();

        FindObjectOfType<Dialogue>().SetScripts("유령집사", "tutorial", 0);
        //딕셔너리 사용법
        //scripts = scriptDic["감기"];
        //Debug.Log(scripts[0].InnerScripts[0].name);
    }

    private string[] scriptName = { "tutorial" };
    private string[] scriptIdx = { "name", "script" };
    private string[] scriptOrder = { "first", "second", "third", "fourth", "fifth", "sixth" };

    private int[] scriptOrderIdx = { 1 };

    public static Dictionary<string, List<Script>> scriptDic = new Dictionary<string, List<Script>>();


    public void LoadScript()
    {
        for (int k = 0; k < scriptName.Length; k++)
        {
            TextAsset textasset = Resources.Load("Data/Scripts/" + scriptName[k]) as TextAsset;
            string loadstring = textasset.text;
            //string scriptString = File.ReadAllText(Application.dataPath + "/Data/CharacterScript/"+scriptName[k]+".json");
            JObject loadScript = JObject.Parse(loadstring);

            List<Script> tmp_script = new List<Script>();
            
            for (int j = 0; j < scriptOrderIdx[k]; j++)
            {
                JArray loadArr = (JArray)loadScript[scriptOrder[j]];
                List<Script.innerScript> tmp_innerScript = new List<Script.innerScript>();

                for (int i = 0; i < loadArr.Count; i++)
                {
                    string n, s;

                    n = loadArr[i][scriptIdx[0]].ToString();
                    s = loadArr[i][scriptIdx[1]].ToString();
                    tmp_innerScript.Add(new Script.innerScript(n,s));
                }
                tmp_script.Add(new Script(scriptOrder[j], tmp_innerScript));
            }
            scriptDic.Add(scriptName[k], tmp_script);
        }
    }


    [SerializeField]
    public class Script{
        public string order;
        public List<innerScript> InnerScripts = new List<innerScript>();

        public Script(string order,List<innerScript> innerScripts) {
            this.order = order;
            this.InnerScripts = innerScripts;

        }
        [Serializable]
        public class innerScript {

            public string name;
            public string script;

            public innerScript(string name, string script)
            {
                this.name = name;
                this.script = script;
            }
        }
    }
}
