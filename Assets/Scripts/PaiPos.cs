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
    /// ������
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
    /// �g�Ƀ����N���郂�f����ݒ�
    /// </summary>
    public void LinkModel(PaiModel model)
    {
        Model = model;
    }

    /// <summary>
    /// �g�̑I���E��I��
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
    /// �g�̕\���E��\��
    /// </summary>
    /// <param name="visible"></param>
    public void VisiblePosFrame(bool visible)
    {
        posMesh.enabled = visible;
    }

    /// <summary>
    /// �}�l�[�W���[�o�R�őI���i�}�l�[�W���[�͈ȑO�I���������������Ă���̂ŁA������I���ɂ���j
    /// </summary>
    public void ManagerSelected(bool issel)
    {
        if (isSelect != issel)
        {
            manager.Selected(this, issel);
        }
    }

    /// <summary>
    /// �}�l�[�W���[�o�R�ŃJ�[�\���ʒu�̔v��ݒ肷��i�J�[�\�����̓}�l�[�W���[�������Ă���j
    /// </summary>
    public void ManagerSetType(ePai type)
    {
        manager.SetType(type);
    }

    /// <summary>
    /// �����N���Ă��郂�f�����A�g�Ɠ����ꏊ�Ɉړ�������
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
