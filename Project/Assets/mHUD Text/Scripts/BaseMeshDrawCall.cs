using UnityEngine;
using System.Collections;

public class BaseMeshDrawCall : MonoBehaviour
{
    [HideInInspector]
    [System.NonSerialized]
    public BetterList<Vector3> verts = new BetterList<Vector3>();
    [HideInInspector]
    [System.NonSerialized]
    public BetterList<Vector2> uvs = new BetterList<Vector2>();
    [HideInInspector]
    [System.NonSerialized]
    public BetterList<Color32> cols = new BetterList<Color32>();


    protected Mesh mMesh;			// 当前生成的显示Mesh
    protected MeshFilter mFilter;		// Mesh filter for this draw call
    public MeshRenderer mRenderer = null;		// Mesh renderer for this screen


    public virtual void UpdateGeometry()
    {
        int vertex_count = verts.size;

        //Safety check to ensure we get valid values
        if (vertex_count > 0
                && (vertex_count == uvs.size && vertex_count == cols.size)
                    && (vertex_count % 4) == 0)
        {
            if (mFilter == null) mFilter = gameObject.GetComponent<MeshFilter>();
            if (mFilter == null) mFilter = gameObject.AddComponent<MeshFilter>();

            // Create the mesh
            if (mMesh == null)
            {
                mMesh = new Mesh();
                mMesh.hideFlags = HideFlags.DontSave;
                mMesh.name = "Mesh";
                mMesh.MarkDynamic();
            }

            mMesh.Clear();
            mMesh.vertices = verts.ToArray();
            mMesh.uv = uvs.ToArray();
            mMesh.colors32 = cols.ToArray();

            int triangleCount = (vertex_count >> 1) * 3;
            mMesh.triangles = GenerateCachedIndexBuffer(vertex_count, triangleCount);
            mFilter.mesh = mMesh;

            if (mRenderer == null) mRenderer = gameObject.GetComponent<MeshRenderer>();
            if (mRenderer == null) mRenderer = gameObject.AddComponent<MeshRenderer>();

            this.UpdateMaterials();
        }
        else
        {
            if (mFilter.mesh != null) mFilter.mesh.Clear();
            Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + vertex_count);
        }

    }

    public virtual void UpdateMaterials() { }
    
    protected int[] GenerateCachedIndexBuffer(int vertexCount, int triangleCount)
    {
        //for (int i = 0, imax = mCache.Count; i < imax; ++i)
        //{
        //    int[] ids = mCache[i];
        //    if (ids != null && ids.Length == indexCount)
        //        return ids;
        //}

        int[] rv = new int[triangleCount];
        int index = 0;

        for (int i = 0; i < vertexCount; i += 4)
        {
            rv[index++] = i;
            rv[index++] = i + 1;
            rv[index++] = i + 2;

            rv[index++] = i + 2;
            rv[index++] = i + 3;
            rv[index++] = i;
        }

        //if (mCache.Count > maxIndexBufferCache) mCache.RemoveAt(0);
        //mCache.Add(rv);
        return rv;
    }



}
