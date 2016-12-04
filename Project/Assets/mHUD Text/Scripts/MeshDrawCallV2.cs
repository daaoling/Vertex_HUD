using UnityEngine;
using System.Collections;


public class MeshDrawCallV2 : MonoBehaviour
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

    [HideInInspector]
    [System.NonSerialized]
    public BetterList<Vector3> vertex1 = new BetterList<Vector3>();

    [HideInInspector]
    [System.NonSerialized]
    public BetterList<Vector4> vertex2 = new BetterList<Vector4>();

    [HideInInspector]
    [System.NonSerialized]
    public BetterList<Vector2> vertex3 = new BetterList<Vector2>();


    Mesh mMesh;			// 当前生成的显示Mesh
    MeshFilter mFilter;		// Mesh filter for this draw call
    MeshRenderer mRenderer = null;		// Mesh renderer for this screen

    Shader shader;
    Texture mainTexture;
    Material mDynamicMat;	// Instantiated material


    void OnEnable()
    {
        //CreateMaterial();
    }

    void OnDisable()
    {
        NGUITools.DestroyImmediate(mDynamicMat);
        mDynamicMat = null;
    }

    public void Init(Texture mainTexture)
    {
        this.mainTexture = mainTexture;
    }

    public void UpdateGeometry()
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

            mMesh.normals = vertex1.ToArray();
            mMesh.tangents = vertex2.ToArray();
            mMesh.uv1 = vertex3.ToArray();

            int triangleCount = (vertex_count >> 1) * 3;
            mMesh.triangles = GenerateCachedIndexBuffer(vertex_count, triangleCount);
            mFilter.mesh = mMesh;

            if (mRenderer == null) mRenderer = gameObject.GetComponent<MeshRenderer>();
            if (mRenderer == null) mRenderer = gameObject.AddComponent<MeshRenderer>();

            mPopEntryManagerV2.Instance.index++;
            //mRenderer.sortingLayerName = ""
                //FX.ToString();
            mRenderer.sortingOrder = mPopEntryManagerV2.Instance.index;
            
            this.UpdateMaterials();
        }
        else
        {
            //if (mFilter.mesh != null) mFilter.mesh.Clear();
            Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + vertex_count);
        }
    }

    void UpdateMaterials()
    {
        if (mDynamicMat == null)
        {
            CreateMaterial();
        }
    }

    void CreateMaterial()
    {
        shader = Shader.Find("Custom/VertexAnimation");
        mDynamicMat = new Material(shader);
        mDynamicMat.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
        mDynamicMat.mainTexture = mainTexture;

        mRenderer.sharedMaterial = mDynamicMat;
        //mRenderer.sharedMaterial.SetFloat("_StartTime", Time.time);
        mRenderer.sharedMaterial.SetFloat("_StartTime", Time.timeSinceLevelLoad);
    }

    int[] GenerateCachedIndexBuffer(int vertexCount, int triangleCount)
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
