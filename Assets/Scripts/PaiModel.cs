using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiModel : MonoBehaviour
{
    [SerializeField]
    List<Material>    typeMaterials;

    [System.NonSerialized]
    public ePai       Type;

    [System.NonSerialized]
    public PaiModel   Model;
    MeshRenderer      modelMesh;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(PaiModel model)
    {
        if ((int)ePai.Chun != typeMaterials.Count)
        {
            Debug.LogError("eType < mats.Count");
            return;
        }

        if (Model == null)
        {
            model.gameObject.SetActive(true);

            Model     = model;
            modelMesh = Model.GetComponentInChildren<MeshRenderer>();
        }
    }

    /// <summary>
    /// 牌を設定。None で非表示
    /// </summary>
    public void SetType(ePai type)
    {
        if (type == ePai.None)
        {
            modelMesh.enabled  = false;
        }
        else
        {
            modelMesh.enabled  = true;

            Material[] materials = modelMesh.materials;
            materials[0] = materials[0];
            materials[1] = materials[1];
            materials[2] = getMaterial(type);
            modelMesh.materials = materials;

            Model.name = type.ToString();
        }

        Type = type;
    }

    /// <summary>
    /// 牌が表示されているか確認する
    /// </summary>
    /// <returns></returns>
    public bool CheckVisible()
    {
        return modelMesh.enabled;
    }

    /// <summary>
    /// ePai から牌のマテリアルを取得する
    /// </summary>
    Material getMaterial(ePai type)
    {
        // かなり危険なやり方
        return typeMaterials[(int)type-1];
    }
}
