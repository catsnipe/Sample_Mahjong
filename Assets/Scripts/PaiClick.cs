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
    /// �h���b�O�J�n
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
    /// �h���b�O
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
    /// �h���b�O�I��
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
    /// �N���b�N
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (posGroup.IsGameBoard == true)
        {
            // �g��I���i�J�[�\���j
            pos.ManagerSelected(true);
        }
        else
        {
            // �J�[�\���ʒu�̔v��ύX
            pos.ManagerSetType(pos.Model.Type);
        }
    }
}
