using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mPopEntry_Meta_Info_3 : ScriptableObject
{
    ///当前顶点的插值关键帧  由于顶点动画通道的限制
    ///这里约定最多后续3帧的插值 最大动画时间段是3秒
	///可定义后续三帧的 x,y,a,timePoint


    public AnimationCurve x_offsetCurve = new AnimationCurve(
         new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 0f), new Keyframe(2f, 0f), new Keyframe(3f, 0f) }
    );
    public AnimationCurve y_offsetCurve = new AnimationCurve(
         new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 6f), new Keyframe(2f, 7f), new Keyframe(3f, 8f) }
    );

    public AnimationCurve x_scaleCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 0.3f), new Keyframe(1f, 1f), new Keyframe(2f, 1f), new Keyframe(3f, 1f) }
    );
    public AnimationCurve y_scaleCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 0.3f), new Keyframe(1f, 1f), new Keyframe(2f, 1f), new Keyframe(3f, 1f) }
    );
    public AnimationCurve alphaCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(1f, 1f), new Keyframe(2f, 0.5f), new Keyframe(3f, 0f) }  
    );
    
    /// <summary>
    /// 插值时间点
    /// </summary>
    public float[] timePoint = new float[] { 0f, 1f, 2f, 3f};
    
    /// <summary>
    /// 网格宽
    /// </summary>
    public float width = 3;
    
    /// <summary>
    /// 网格高
    /// </summary>
    public float height = 3;
}
