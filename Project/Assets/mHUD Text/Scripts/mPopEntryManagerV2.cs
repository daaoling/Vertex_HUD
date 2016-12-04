using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Pop 来源类型  这块根据策划那边需求添加
///// </summary>
public enum PopEntryType
{
    beNormalAttacked, //被标准伤害显示
    beNormalBaoJiAttacked,  //被暴击伤害显示
    beSkillAttacked,  //被技能伤害
    beSkillTreat,     //被技能治疗
    baoji,            //触发暴击显示
    shanbi            //触发闪避显示
}


public class mPopEntryManagerV2 : UnityNormalSingleton<mPopEntryManagerV2>
{

    public UIAtlas uiAtlas;
    public int index;
    void Awake()
    {
        uiAtlas = Resources.Load<GameObject>("HUD2").GetComponent<UIAtlas>();
        index = 0;
    }

    void LateUpdate()
    {
        collectPopValue();

        showPopValue();
    }

    void OnDisable()
    {
        for (int i = 0; i < mList.size; )
        {
            mPopEntryV2 ent = mList.buffer[i];

            mList.RemoveAt(i);

            ent.OnClose();
        }
    }

    
    public BetterList<mPopValue> pops = new BetterList<mPopValue>();
    BetterList<mPopValue> value_list;
    void collectPopValue()
    {
        if (pops.size > 0) 
        {
            value_list = new BetterList<mPopValue>();

            for (int i = 0; i < pops.size; )
            {
                mPopValue ent = pops.buffer[i];
                //Debug.Log(" ent.popType " + ent.popType);
                //Debug.Log(" ent.value " + ent.value);
                value_list.Add(
                    new mPopValue()
                    {
                        pos = ent.pos,
                        isFacingRright = ent.isFacingRright,
                        popType = ent.popType,
                        value = ent.value
                    });
                pops.RemoveAt(i);
            }

            AddEntry(value_list);
        }
    }

    public BetterList<mPopEntryV2> mList = new BetterList<mPopEntryV2>();
    float time; //curTime from runing start
    void showPopValue()
    {
        time = Time.time;

        for (int i = 0; i < mList.size; )
        {
            mPopEntryV2 ent = mList.buffer[i];
            ent.curRuningTime = time - ent.curStartTimePoint;
            if (ent.curRuningTime > ent.totalRuningTime)
            {
                PopEntrySceneTest2.value -= ent.list.size;

                mList.RemoveAt(i);
                ent.OnClose();

                continue;
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
    void AddEntry(BetterList<mPopValue> value_list)
    {
        mPopEntryV2 newPopEntry = new mPopEntryV2();

        ///设置运行数据
        newPopEntry.curStartTimePoint = Time.time;
        newPopEntry.curRuningTime = 0f;
        newPopEntry.totalRuningTime = 3.0f;
        newPopEntry.list = value_list;

        ///返回当前pop类型对应的运动轨迹曲线
        //string loadPath = "VertexFrame/" + PopEntryType.baoji.ToString();
        //newPopEntry.pop_entry_meta = Resources.Load<mPopEntry_Meta_Info_2>(loadPath);

        newPopEntry.OnBeforeAdd();

        PopEntrySceneTest2.value += value_list.size;

        mList.Add(newPopEntry);
    }

    public void AddEntry(Vector3 initPos, bool isFacingRight, PopEntryType popType, object value)
    {
        mPopValue _value = new mPopValue();
        _value.pos = initPos;
        _value.isFacingRright = isFacingRight;
        _value.popType = popType;
        _value.value = new List<string>();
        switch (popType)
        {
            case PopEntryType.beNormalAttacked:
                _value.value.Add("-");
                _value.value
                    .AddRange(
                        ((int)value).ToString().ToCharArray().Select(t => "" + t));
                break;
            case PopEntryType.beNormalBaoJiAttacked:
                _value.value.Add("-");
                _value.value
                    .AddRange(
                        ((int)value).ToString().ToCharArray().Select(t => "_" + t));
                break;
            case PopEntryType.baoji:
                _value.value.Add("z");
                break;
            case PopEntryType.shanbi:
                _value.value.Add("x");
                break;
            case PopEntryType.beSkillAttacked:
                _value.value.Add("-");
                _value.value
                    .AddRange(
                        ((int)value).ToString().ToCharArray().Select(t => "" + t));
                break;
            case PopEntryType.beSkillTreat:
                _value.value.Add("+");
                _value.value
                    .AddRange(
                        ((int)value).ToString().ToCharArray().Select(t => "__" + t));
                break;
            default:
                break;
        }

        pops.Add(_value);
    }

}

