using UnityEngine;
using System.Collections;

//相关运动轨迹参数
public class mPopEntry_Meta_Info : ScriptableObject
{


    //public PopEntryType popEntryType;

    //public string desc;

    //public GameObject popEntryPrefab;

    /// <summary>
    /// Curve used to move entries with time.
    /// </summary>

    public AnimationCurve x_offsetCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.8f, 8f) }
    );
    public AnimationCurve y_offsetCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.8f, 8f) }
    );

    /// <summary>
    /// Curve used to fade out entries with time.
    /// </summary>

    public AnimationCurve alphaCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 0.5f), new Keyframe(0.3f, 1f), new Keyframe(0.8f, 1f) }
    );

    /// <summary>
    /// Curve used to scale the entries.
    /// </summary>

    public AnimationCurve x_scaleCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.3f, 8f), new Keyframe(0.8f, 8f) }
    );
    public AnimationCurve y_scaleCurve = new AnimationCurve(
        new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.3f, 8f), new Keyframe(0.8f, 8f) }
    );


    public float getTotalRuningTime
    {
        get
        {
            Keyframe[] x_offsetCurve_frames = x_offsetCurve.keys;
            Keyframe[] y_offsetCurve_frames = y_offsetCurve.keys;
            Keyframe[] alpha_frames = alphaCurve.keys;
            Keyframe[] x_scaleCurve_frames = x_scaleCurve.keys;
            Keyframe[] y_scaleCurve_frames = y_scaleCurve.keys;

            float end1 = x_offsetCurve_frames[x_offsetCurve_frames.Length - 1].time;
            float end2 = y_offsetCurve_frames[y_offsetCurve_frames.Length - 1].time;
            float end3 = alpha_frames[alpha_frames.Length - 1].time;
            float end4 = x_scaleCurve_frames[x_scaleCurve_frames.Length - 1].time;
            float end5 = y_scaleCurve_frames[x_scaleCurve_frames.Length - 1].time;

            float totalEnd = 
                Mathf.Max(end1, 
                    Mathf.Max(end2, 
                        Mathf.Max(end3, 
                            Mathf.Max(end4, end5))));

            return totalEnd;
        }
    }
}
