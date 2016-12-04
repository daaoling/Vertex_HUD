using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
public class PopEntrySceneTest : MonoBehaviour {

    public GameObject target;

    public int value = 1000;



    //public UIAtlas uiAtlas; ///选择的图集
     
    public List<mPopEntry_Meta_Info> popList; ///轨迹集合

    List<Transform> posList; ///弹出位置集合

    public List<string> values; ///字符串集合

    void Start() {
        
        //mPopEntryManager.Instance.uiAtlas = uiAtlas;

        posList = GameObject.Find("pos").GetComponentsInChildren<Transform>().ToList();
    
    }

    float timer = 0.1f;
    void Update()
    {
        //mPopEntryManager.Instance.Update();

        if ((timer -= RealTime.deltaTime) < 0f)
        {
            timer = 0.1f;

            for (int i = 0; i < 10; i++)
            {
                mPopEntryManager.Instance
                    .AddEntry(
                        PopEntryType.beNormalBaoJiAttacked,
                            popList[UnityEngine.Random.Range(0, popList.Count)],
                                posList[UnityEngine.Random.Range(0, posList.Count)].transform.position,
                                    values[UnityEngine.Random.Range(0, values.Count)]);
            }
        }
    }

    void OnGUI()
    {
        //"battle/Sprites/FightScene/normalBeAttacked"
        //for (int i = 0; i < popList.Count; i++)
        //{
            //mPopEntry_Meta_Info info = popList[i];

        GUI.Label(new Rect(10, 0, 1000, 30), " 当前 pop count " + mPopEntryManager.Instance.mList.size);

        if (GUI.Button(new Rect(10, 50, 1000, 30), " pop 数量 +100"))
        {

            for (int i = 0; i < 10; i++)
            {
                mPopEntryManager.Instance
                    .AddEntry(
                        PopEntryType.beNormalBaoJiAttacked,
                            popList[UnityEngine.Random.Range(0, popList.Count)],
                                posList[UnityEngine.Random.Range(0, posList.Count)].transform.position,
                                    values[UnityEngine.Random.Range(0, values.Count)]);
            }
        }
        //}
    }

    //public void beNormalAttacked(mPopEntry_Meta_Info info)
    //{
    //    mPopEntryManager.Instance.AddEntry(info, target.transform.position,);
    //}
    //public void beBaoJiAttacked(mPopEntry_Meta_Info info)
    //{
    //    mPopEntryManager.Instance.AddEntry(info, target.transform.position);
    //}
    //public void beTreat(mPopEntry_Meta_Info info)
    //{
    //    mPopEntryManager.Instance.AddEntry(info, target.transform.position);
    //}
    //public void beShanBi(mPopEntry_Meta_Info info)
    //{
    //    mPopEntryManager.Instance.AddEntry(info, target.transform.position);
    //}
}
