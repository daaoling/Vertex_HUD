using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mPopValue
{
    public Vector3 pos;
    public bool isFacingRright;
    public List<string> value;
    public PopEntryType popType;
}

public class mPopEntryV2
{
    public float curStartTimePoint;  ///当前开始的时间

    public float curRuningTime; ///当前运行的时间

    public float totalRuningTime; ///总运行的时间

    /// <summary>
    /// pop list
    /// </summary>
    public BetterList<mPopValue> list;
    /// <summary>
    /// 顶点动画关键帧插值信息
    /// </summary>
    //public mPopEntry_Meta_Info_2 pop_entry_meta;



    MeshDrawCallV2 drawcall; ///mesh 绘制组件


    public virtual void OnBeforeAdd()
    {
        this.drawcall = createChild();

        ///填充顶点数据
        FillV2(
            this.drawcall.verts, this.drawcall.uvs, this.drawcall.cols,
                this.drawcall.vertex1, this.drawcall.vertex2, this.drawcall.vertex3);

        ///生成mesh
        this.drawcall.UpdateGeometry();
    }

    public void FillV2(
        BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols,
            BetterList<Vector3> vertex1, BetterList<Vector4> vertex2, BetterList<Vector2> vertex3)
    {
        Vector3 initPos;
        bool isFacingRright;
        PopEntryType type;
        string loadPath;
        List<string> popValue;

        foreach (mPopValue mPopValue in this.list)
        {
            initPos = mPopValue.pos;
            isFacingRright = mPopValue.isFacingRright;
            type = mPopValue.popType;
            loadPath = "VertexFrame/" + type;
            popValue = mPopValue.value;
            

            for (int i = 0; i < popValue.Count; i++)
            {
                ///第一帧
                ///第二帧关键帧
                ///第三帧关键帧
                ///第四帧关键帧
                
                ///是否是多个字符串组合
                if (isMultiply(type)){
                    multoPop_Meta_Info popInfo = Resources.Load<multoPop_Meta_Info>(loadPath);
                    popInfo.Fill(initPos, isFacingRright, i, popValue.Count, verts, vertex1, vertex2, vertex3, cols);
                }
                else {
                    sinlePop_Meta_Info popInfo = Resources.Load<sinlePop_Meta_Info>(loadPath);
                    popInfo.Fill(initPos, isFacingRright, verts, vertex1, vertex2, vertex3, cols);
                }

                UISpriteData mSprite =
                    mPopEntryManagerV2.Instance.uiAtlas.GetSprite(popValue[i].ToString());

                Texture tex = mPopEntryManagerV2.Instance.uiAtlas.texture;
                Rect outer = new Rect(mSprite.x, mSprite.y, mSprite.width, mSprite.height);
                outer = NGUIMath.ConvertToTexCoords(outer, tex.width, tex.height);

                uvs.Add(new Vector2(outer.xMin, outer.yMin));
                uvs.Add(new Vector2(outer.xMin, outer.yMax));
                uvs.Add(new Vector2(outer.xMax, outer.yMax));
                uvs.Add(new Vector2(outer.xMax, outer.yMin));
            }
        }
    }


    bool isMultiply(PopEntryType popEntryType) 
    {
        return popEntryType <= PopEntryType.beSkillTreat;
    }

    public virtual void OnClose()
    {
        this.list.Release();
        this.list = null;

        NGUITools.Destroy(this.drawcall.gameObject);
    }

    MeshDrawCallV2 createChild()
    {
        GameObject drawCallChild = new GameObject("drawCallChild");
            //NGUITools.AddChild(mPopEntryManagerV2.Instance.gameObject);
        MeshDrawCallV2 dc = drawCallChild.AddComponent<MeshDrawCallV2>();
        dc.Init(mPopEntryManagerV2.Instance.uiAtlas.texture);
        return dc;
    }

}