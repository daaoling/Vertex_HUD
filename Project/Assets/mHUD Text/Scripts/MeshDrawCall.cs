using UnityEngine;
using System.Collections;




/// <summary>
/// 负责绘制drawcall
/// </summary>
public class MeshDrawCall : BaseMeshDrawCall
{
    Material shareMaterial;		// Material used by this screen



    public Transform child;
    void Awake()
    {
        child = NGUITools.AddChild(gameObject).transform;
    }

    public void Init(Material shareMaterial)
    {
        this.shareMaterial = shareMaterial;
    }


    public override void UpdateMaterials()
    {
        mRenderer.enabled = true;
        mRenderer.sharedMaterial = this.shareMaterial;
    }




    //static public MeshDrawCall Create(string name, Material shareMaterial)
    //{
    //    GameObject go = mPopEntryManager.Instance.pool.Dequeue();
    //        //new GameObject(name);
    //    MeshDrawCall newDC = go.AddComponent<MeshDrawCall>();
    //    newDC.shareMaterial = shareMaterial;
    //    return newDC;
    //}

    //public void OnClose()
    //{

    //}
}