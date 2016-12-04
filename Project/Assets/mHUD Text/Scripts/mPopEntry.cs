using UnityEngine;
using System.Collections;


/// <summary>
/// 头顶跳出 pop 基类
/// </summary>
public class BasePopEntry
{
    public float curStartTimePoint;  ///当前开始的时间

    public float curRuningTime; ///当前运行的时间

    public float totalRuningTime; ///总运行的时间
                                  
    public BaseMeshDrawCall drawcall; ///mesh 绘制组件


    public virtual void Fill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols) { }
}



public class mPopEntry : BasePopEntry
{
    public PopEntryType popType; ///pop 类型

    /// <summary>
    /// Pop 轨迹数据
    /// </summary>
    public AnimationCurve x_offsetCurve;
    public AnimationCurve y_offsetCurve;
    public AnimationCurve x_scaleCurve;
    public AnimationCurve y_scaleCurve;
    public AnimationCurve alphaCurve;

    public Vector3 initPos; ///pop 初始开始位置
    public string popValue; ///pop 显示的值       




    public virtual void OnBeforeAdd()
    {
        this.drawcall =
            mPopEntryManager.Instance.Dequeue();

        ///确定mesh原点位置
        this.drawcall.transform.position = initPos;

        ///填充顶点数据
        Fill(this.drawcall.verts, this.drawcall.uvs, this.drawcall.cols);

        ///生成mesh
        this.drawcall.UpdateGeometry();
    }

    public override void Fill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
        int length = popValue.Length;

        for (int i = 0; i < length; i++)
        {
            UISpriteData mSprite =
                mPopEntryManager.Instance.uiAtlas.GetSprite(popValue[i].ToString());

            verts.Add(new Vector3(0 + i, 0, 0));
            verts.Add(new Vector3(0 + i, 1, 0));
            verts.Add(new Vector3(1 + i, 1, 0));
            verts.Add(new Vector3(1 + i, 0, 0));


            Texture tex = mPopEntryManager.Instance.uiAtlas.texture;
            Rect outer = new Rect(mSprite.x, mSprite.y, mSprite.width, mSprite.height);
            outer = NGUIMath.ConvertToTexCoords(outer, tex.width, tex.height);

            uvs.Add(new Vector2(outer.xMin, outer.yMin));
            uvs.Add(new Vector2(outer.xMin, outer.yMax));
            uvs.Add(new Vector2(outer.xMax, outer.yMax));
            uvs.Add(new Vector2(outer.xMax, outer.yMin));

            cols.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
            cols.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
            cols.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
            cols.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        }
    }

    public void Update()
    {
        SampleCurve(); ///插值运动轨迹
    }

    public virtual void OnClose()
    {
        //this.drawcall.mRenderer.enabled = false;
        mPopEntryManager.Instance.Enqueue(this.drawcall); ///销毁绘制组件    
    }

    public static mPopEntry SpawnEntry(PopEntryType popEntryType)
    {
        mPopEntry result = new mPopEntry();

        switch (popEntryType)
        {
            case PopEntryType.beNormalAttacked:
                break;
            case PopEntryType.beNormalBaoJiAttacked:
                break;
            case PopEntryType.baoji:
                break;
            case PopEntryType.shanbi:
                break;
            case PopEntryType.beSkillAttacked:
                break;
            case PopEntryType.beSkillTreat:
                break;
            default:
                break;
        }

        return result;
    }

    float x_offset;
    float y_offset;
    float x_scale;
    float y_scale;
    float alpha;
    /// <summary>
    /// 插值运动轨迹
    /// </summary>
    public virtual void SampleCurve()
    {
        x_offset = x_offsetCurve.Evaluate(curRuningTime);
        y_offset = y_offsetCurve.Evaluate(curRuningTime);
        x_scale = x_scaleCurve.Evaluate(curRuningTime);
        y_scale = y_scaleCurve.Evaluate(curRuningTime);
        alpha = alphaCurve.Evaluate(curRuningTime);

        this.drawcall.transform.localPosition = new Vector3(x_offset, y_offset, 0);
        this.drawcall.transform.localScale = new Vector3(x_scale, y_scale, 0);
    }
}