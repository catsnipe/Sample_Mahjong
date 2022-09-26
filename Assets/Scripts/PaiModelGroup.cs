using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiModelGroup : MonoBehaviour
{
    [SerializeField]
    PaiModel        PrefabModel;

    List<PaiModel>  pais;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        pais = new List<PaiModel>();
    }

    /// <summary>
    /// グループ名
    /// </summary>
    public void SetName(string name)
    {
        this.gameObject.name = name;
    }

    /// <summary>
    /// 位置
    /// </summary>
    public void SetPosition(float x, float y)
    {
        var trans = this.transform.localPosition;
        trans.x = x;
        trans.y = y;
        this.transform.localPosition = trans;
    }

    /// <summary>
    /// スケール
    /// </summary>
    public void SetScale(float size)
    {
        var scale = this.transform.localScale;
        scale.x = size;
        scale.y = size;
        scale.z = size;
        this.transform.localScale = scale;
    }

    /// <summary>
    /// 回転。自家0、下家90、対面180、上家270
    /// </summary>
    public void SetRotate(float angle)
    {
        var rotate = this.transform.localEulerAngles;
        rotate.z = angle;
        this.transform.localEulerAngles = rotate;
    }

    /// <summary>
    /// 枠に合わせてモデルオブジェクトを追加
    /// </summary>
    public PaiModel AddModel(PaiPos pos)
    {
        var model = Instantiate(PrefabModel, this.transform);
        model.Initialize(model);
        model.SetType(ePai.None);

        Vector3 v = model.transform.position;
        v.x = pos.transform.localPosition.x;
        v.y = pos.transform.localPosition.y;
        model.transform.position = v;

        pais.Add(model);

        return model;
    }

    /// <summary>
    /// モデルの牌タイプを設定
    /// </summary>
    /// <param name="no">左上からの連番</param>
    /// <param name="type">牌タイプ</param>
    public void SetType(int no, ePai type)
    {
        if (no < 0 || no >= pais.Count)
        {
            return;
        }
        pais[no].SetType(type);
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void Show()
    {
        foreach (var pai in pais)
        {
            pai.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(true);
    }
}
