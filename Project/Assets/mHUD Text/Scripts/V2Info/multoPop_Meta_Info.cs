using UnityEngine;
using System.Collections;

/// <summary>
/// 类似+111 -111 这种多个字符串拼接的
/// </summary>
public class multoPop_Meta_Info : mPopEntry_Meta_Info_3
{

    Vector3[] GetVertexKeyFrameArray(Vector3 initPos, bool isFacingRight, int index, int count, float timePoint)
    {
        Vector3[] points = new Vector3[4];

        float x_scale = x_scaleCurve.Evaluate(timePoint);
        float y_scale = y_scaleCurve.Evaluate(timePoint);

        ///先算偏移后的中心点
        initPos += new Vector3(1, 0, 0) * x_offsetCurve.Evaluate(timePoint) * (isFacingRight ? 1 : -1);
        initPos += new Vector3(0, 1, 0) * y_offsetCurve.Evaluate(timePoint);

        ///再根据当前第几个格子 + 中心点 算出格子的中心点
        float middleWidth = count * width / 2.0f;
        float curWidth = index * width + width / 2.0f;
        initPos += new Vector3(1, 0, 0) * (curWidth - middleWidth) * x_scale;

        ///left_bottom
        points[0] = initPos
                        + new Vector3(-1, 0, 0) * (width / 2.0f) * x_scale
                        + new Vector3(0, -1, 0) * (height / 2.0f) * y_scale;

        ///left_top
        points[1] = initPos
                    + new Vector3(-1, 0, 0) * (width / 2.0f) * x_scale
                    + new Vector3(0, 1, 0) * (height / 2.0f) * y_scale;

        ///right_top
        points[2] = initPos
                    + new Vector3(1, 0, 0) * (width / 2.0f) * x_scale
                    + new Vector3(0, 1, 0) * (height / 2.0f) * y_scale;

        ///right_bottom
        points[3] = initPos
                    + new Vector3(1, 0, 0) * (width / 2.0f) * x_scale
                    + new Vector3(0, -1, 0) * (height / 2.0f) * y_scale;

        return points;
    }

    public void Fill(Vector3 initPos, bool isFacingRight, int index, int count,
        BetterList<Vector3> vertex, BetterList<Vector3> vertex1,
            BetterList<Vector4> vertex2, BetterList<Vector2> vertex3,
                BetterList<Color32> cols)
    {
        Vector3[] _vertex0 = GetVertexKeyFrameArray(initPos, isFacingRight, index, count, this.timePoint[0]);
        vertex.Add(_vertex0[0]);
        vertex.Add(_vertex0[1]);
        vertex.Add(_vertex0[2]);
        vertex.Add(_vertex0[3]);

        Vector3[] _vertex1 = GetVertexKeyFrameArray(initPos, isFacingRight, index, count, this.timePoint[1]);
        vertex1.Add(new Vector3(_vertex1[0].x, _vertex1[0].y, this.timePoint[1]));
        vertex1.Add(new Vector3(_vertex1[1].x, _vertex1[1].y, this.timePoint[1]));
        vertex1.Add(new Vector3(_vertex1[2].x, _vertex1[2].y, this.timePoint[1]));
        vertex1.Add(new Vector3(_vertex1[3].x, _vertex1[3].y, this.timePoint[1]));
        //vertex1.Add(_vertex1[0]);
        //vertex1.Add(_vertex1[1]);
        //vertex1.Add(_vertex1[2]);
        //vertex1.Add(_vertex1[3]);

        Vector3[] _vertex2 = GetVertexKeyFrameArray(initPos, isFacingRight, index, count, this.timePoint[2]);
        Vector3[] _vertex3 = GetVertexKeyFrameArray(initPos, isFacingRight, index, count, this.timePoint[3]);
        vertex2.Add(new Vector4(_vertex2[0].x, _vertex2[0].y, this.timePoint[2], _vertex3[0].x));
        vertex2.Add(new Vector4(_vertex2[1].x, _vertex2[1].y, this.timePoint[2], _vertex3[1].x));
        vertex2.Add(new Vector4(_vertex2[2].x, _vertex2[2].y, this.timePoint[2], _vertex3[2].x));
        vertex2.Add(new Vector4(_vertex2[3].x, _vertex2[3].y, this.timePoint[2], _vertex3[3].x));
        //vertex2.Add(new Vector4(_vertex2[0].x, _vertex2[0].y, _vertex2[0].z, _vertex3[0].x));
        //vertex2.Add(new Vector4(_vertex2[1].x, _vertex2[1].y, _vertex2[1].z, _vertex3[1].x));
        //vertex2.Add(new Vector4(_vertex2[2].x, _vertex2[2].y, _vertex2[2].z, _vertex3[2].x));
        //vertex2.Add(new Vector4(_vertex2[3].x, _vertex2[3].y, _vertex2[3].z, _vertex3[3].x));

        vertex3.Add(new Vector2(_vertex3[0].y, this.timePoint[3]));
        vertex3.Add(new Vector2(_vertex3[1].y, this.timePoint[3]));
        vertex3.Add(new Vector2(_vertex3[2].y, this.timePoint[3]));
        vertex3.Add(new Vector2(_vertex3[3].y, this.timePoint[3]));


        cols.Add(
            new Color(
                this.alphaCurve.Evaluate(this.timePoint[1]),
                this.alphaCurve.Evaluate(this.timePoint[2]),
                this.alphaCurve.Evaluate(this.timePoint[3]),
                this.alphaCurve.Evaluate(this.timePoint[0])));
        cols.Add(
            new Color(
                this.alphaCurve.Evaluate(this.timePoint[1]),
                this.alphaCurve.Evaluate(this.timePoint[2]),
                this.alphaCurve.Evaluate(this.timePoint[3]),
                this.alphaCurve.Evaluate(this.timePoint[0])));
        cols.Add(
            new Color(
                this.alphaCurve.Evaluate(this.timePoint[1]),
                this.alphaCurve.Evaluate(this.timePoint[2]),
                this.alphaCurve.Evaluate(this.timePoint[3]),
                this.alphaCurve.Evaluate(this.timePoint[0])));
        cols.Add(
            new Color(
                this.alphaCurve.Evaluate(this.timePoint[1]),
                this.alphaCurve.Evaluate(this.timePoint[2]),
                this.alphaCurve.Evaluate(this.timePoint[3]),
                this.alphaCurve.Evaluate(this.timePoint[0])));
    }
}
