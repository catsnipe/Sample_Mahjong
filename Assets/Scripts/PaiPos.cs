using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiPos : MonoBehaviour
{
    [SerializeField]
    List<Material>    posFrameMaterials;

    [System.NonSerialized]
    public PaiPos     Next;

    [System.NonSerialized]
    public PaiPos     Pos;
    MeshRenderer      posMesh;

    [System.NonSerialized]
    public int        PosNo;

    public PaiModel   Model;
    //{ get; private set; }

    PaiManager        manager;
    bool              isSelect;

    Coroutine         co_move;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(PaiPos pos)
    {
        if (Pos == null)
        {
            Pos      = pos;
            posMesh  = Pos.GetComponentInChildren<MeshRenderer>();

            Pos.gameObject.SetActive(true);

            manager   = Pos.GetComponentInParent<PaiManager>();
        }
    }

    /// <summary>
    /// 枠にリンクするモデルを設定
    /// </summary>
    public void LinkModel(PaiModel model)
    {
        Model = model;
    }

    /// <summary>
    /// 枠の選択・非選択
    /// </summary>
    public void Selected(bool issel)
    {
        if (issel == true)
        {
            posMesh.material = posFrameMaterials[1];
        }
        else
        {
            posMesh.material = posFrameMaterials[0];
        }
        isSelect = issel;
    }

    /// <summary>
    /// 枠の表示・非表示
    /// </summary>
    /// <param name="visible"></param>
    public void VisiblePosFrame(bool visible)
    {
        posMesh.enabled = visible;
    }

    /// <summary>
    /// マネージャー経由で選択（マネージャーは以前選択した情報を持っているので、それを非選択にする）
    /// </summary>
    public void ManagerSelected(bool issel)
    {
        if (isSelect != issel)
        {
            manager.Selected(this, issel);
        }
    }

    /// <summary>
    /// マネージャー経由でカーソル位置の牌を設定する（カーソル情報はマネージャーが持っている）
    /// </summary>
    public void ManagerSetType(ePai type)
    {
        manager.SetType(type);
    }

    /// <summary>
    /// リンクしているモデルを、枠と同じ場所に移動させる
    /// </summary>
    public void MoveToPos()
    {
        if (co_move != null)
        {
            StopCoroutine(co_move);
        }
        co_move = StartCoroutine(moveToPos(Pos, Model));
    }

    IEnumerator moveToPos(PaiPos pos, PaiModel model)
    {
        var targetpos = pos.transform.localPosition;

        while (targetpos != Vector3.zero)
        {
            var modelpos = model.transform.localPosition;
            var vec      = modelpos + (targetpos - modelpos) * 0.3f;
            var diff     = targetpos - vec;

            if (diff.x * diff.x + diff.y * diff.y < 0.01f)
            {
                vec       = targetpos;
                targetpos = Vector3.zero;
            }

            model.transform.localPosition = vec;

            yield return null;
        }

        co_move = null;
    }

}
