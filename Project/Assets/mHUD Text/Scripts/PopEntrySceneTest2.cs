using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class PopEntrySceneTest2 : MonoBehaviour {


    List<Transform> posList; ///弹出位置集合

    //public List<string> values; ///字符串集合

    //public mPopEntry_Meta_Info_2 pop_entry_meta; ///运动轨迹信息

    public bool needAuto;

    public bool isPopOrigin;

    void Start()
    {

        //mPopEntryManager.Instance.uiAtlas = uiAtlas;

        posList = GameObject.Find("pos").GetComponentsInChildren<Transform>().ToList();

        //Debug.Log(" NumberToChar:" + Util.NumberToChar(0));
    }

    float timer = 0.1f;
    BetterList<mPopValue> mPopValues;
    void Update()
    {
        //Debug.Log(Time.timeSinceLevelLoad);

        if (!needAuto) return;

        if ((timer -= RealTime.deltaTime) < 0f)
        {
            timer = 0.1f;

            mPopValues = new BetterList<mPopValue>();

            for (int i = 0; i < 100; i++)
            {
                Vector3 pos = posList[UnityEngine.Random.Range(0, posList.Count)].transform.position;
                PopEntryType[] randomPopType = (PopEntryType[])Enum.GetValues(typeof(PopEntryType));
                PopEntryType mvalue = randomPopType[UnityEngine.Random.Range(0, randomPopType.Length)];
                bool isFacingRight = (UnityEngine.Random.Range(0,2) == 0 ? true : false);
                mPopEntryManagerV2.Instance.AddEntry(pos, isFacingRight, mvalue, UnityEngine.Random.Range(10, 100));
            }
        }


    }
    public static int value = 0;


    public PopEntryType[] popTypes;
    Vector3 pos;
    void OnGUI()
    {
        //value = 0;
        //foreach(var popEntry in mPopEntryManagerV2.Instance.mList)
        //{
        //    value += popEntry.list.size;
        //}
        GUI.Label(new Rect(10, 0, 1000, 30), " 当前 pop count " + value);

        if (GUI.Button(new Rect(10, 50, 1000, 30), " pop 数量 +10"))
        {
            for (int i = 0; i < 1; i++)
            {
                if (isPopOrigin)
                {
                    pos = Vector3.zero;
                }
                else
                {
                    pos = posList[UnityEngine.Random.Range(0, posList.Count)].transform.position;
                }
                
                //PopEntryType[] randomPopType = (PopEntryType[])Enum.GetValues(typeof(PopEntryType));
                PopEntryType mvalue = popTypes[UnityEngine.Random.Range(0, popTypes.Length)];
                bool isFacingRight = (UnityEngine.Random.Range(0,2) == 0? true : false);
                mPopEntryManagerV2.Instance.AddEntry(pos, isFacingRight, mvalue, UnityEngine.Random.Range(10, 100));
            }
        }
    }
}
