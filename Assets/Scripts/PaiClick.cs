using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaiClick : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    PaiPosGroup    posGroup;
    PaiPos         pos;

    /// <summary>
    /// awake
    /// </summary>
    void Awake()
    {
        posGroup = GetComponentInParent<PaiPosGroup>();
        pos      = GetComponentInParent<PaiPos>();
    }
    
    /// <summary>
    /// ドラッグ開始
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (posGroup.IsGameBoard == false)
        {
            return;
        }
        posGroup.BeginDrag(pos, pos.Model);
    }

    /// <summary>
    /// ドラッグ
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (posGroup.IsGameBoard == false)
        {
            return;
        }
        posGroup.Drag(eventData.position);
    }

    /// <summary>
    /// ドラッグ終了
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (posGroup.IsGameBoard == false)
        {
            return;
        }
        posGroup.EndDrag();
    }

    /// <summary>
    /// クリック
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (posGroup.IsGameBoard == true)
        {
            // 枠を選択（カーソル）
            pos.ManagerSelected(true);
        }
        else
        {
            // カーソル位置の牌を変更
            pos.ManagerSetType(pos.Model.Type);
        }
    }
}
