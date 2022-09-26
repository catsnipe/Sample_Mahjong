using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiModelGroup : MonoBehaviour
{
    [SerializeField]
    PaiModel        PrefabModel;

    List<PaiModel>  pais;

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize()
    {
        pais = new List<PaiModel>();
    }

    /// <summary>
    /// �O���[�v��
    /// </summary>
    public void SetName(string name)
    {
        this.gameObject.name = name;
    }

    /// <summary>
    /// �ʒu
    /// </summary>
    public void SetPosition(float x, float y)
    {
        var trans = this.transform.localPosition;
        trans.x = x;
        trans.y = y;
        this.transform.localPosition = trans;
    }

    /// <summary>
    /// �X�P�[��
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
    /// ��]�B����0�A����90�A�Ζ�180�A���270
    /// </summary>
    public void SetRotate(float angle)
    {
        var rotate = this.transform.localEulerAngles;
        rotate.z = angle;
        this.transform.localEulerAngles = rotate;
    }

    /// <summary>
    /// �g�ɍ��킹�ă��f���I�u�W�F�N�g��ǉ�
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
    /// ���f���̔v�^�C�v��ݒ�
    /// </summary>
    /// <param name="no">���ォ��̘A��</param>
    /// <param name="type">�v�^�C�v</param>
    public void SetType(int no, ePai type)
    {
        if (no < 0 || no >= pais.Count)
        {
            return;
        }
        pais[no].SetType(type);
    }

    /// <summary>
    /// �\��
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
