using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class mPopEntryManager : UnityNormalSingleton<mPopEntryManager>
{

    public UIAtlas uiAtlas;

    public BetterList<mPopEntry> mList = new BetterList<mPopEntry>();

    Queue<BaseMeshDrawCall> pool = new Queue<BaseMeshDrawCall>();

    float time; //curTime from runing start


    //public bool useV1

    void Awake()
    {
        uiAtlas = Resources.Load<GameObject>("HUD").GetComponent<UIAtlas>();
        
        for (int i = 0; i < 100; i++)
        {
            this.Enqueue(createChild());
        }
    }
    public MeshDrawCall createChild()
    {
        Debug.Log(" MeshDrawCall createChild ");

        GameObject child = NGUITools.AddChild(gameObject);
        MeshDrawCall dc = child.AddComponent<MeshDrawCall>();
        dc.Init(this.uiAtlas.spriteMaterial);
        return dc;
    }

    public BaseMeshDrawCall Dequeue() 
    {
        if (pool.Count <= 0)
        {
            this.Enqueue(createChild());
        }
        BaseMeshDrawCall dc = pool.Dequeue();
        if (dc.mRenderer != null)
        {
            dc.mRenderer.enabled = true;
        }
        
        return dc;
    }

    public void Enqueue(BaseMeshDrawCall dc)
    {
        if (dc.mRenderer != null) dc.mRenderer.enabled = false;
        pool.Enqueue(dc);
    }

  

    public void Update()
    {
        time = RealTime.time;

        for (int i = 0; i < mList.size; )
        {
            mPopEntry ent = mList.buffer[i];
            ent.curRuningTime = time - ent.curStartTimePoint;
            if (ent.curRuningTime > ent.totalRuningTime)
            {
                mList.RemoveAt(i);
                ent.OnClose();
                continue;
            }
            else {
                ent.Update();
            }
            i++;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="popTye"></param>
    /// <param name="curve_info"> 轨迹信息 </param>
    /// <param name="pos"> 初始生成位置 </param>
    public void AddEntry(
        PopEntryType popTye, 
            mPopEntry_Meta_Info curve_info,
                Vector3 initPos,
                    string value)
    {
        mPopEntry newPopEntry = mPopEntry.SpawnEntry(popTye);
        
        ///设置运行数据
        newPopEntry.curStartTimePoint = RealTime.time;
        newPopEntry.curRuningTime = 0f;
        newPopEntry.totalRuningTime = curve_info.getTotalRuningTime;

        newPopEntry.x_offsetCurve = curve_info.x_offsetCurve;
        newPopEntry.y_offsetCurve = curve_info.y_offsetCurve;
        newPopEntry.alphaCurve = curve_info.alphaCurve;
        newPopEntry.x_scaleCurve = curve_info.x_scaleCurve;
        newPopEntry.y_scaleCurve = curve_info.y_scaleCurve;

        newPopEntry.initPos = initPos;

        newPopEntry.popValue = value;

        newPopEntry.OnBeforeAdd();

        mList.Add(newPopEntry);
    }
}

