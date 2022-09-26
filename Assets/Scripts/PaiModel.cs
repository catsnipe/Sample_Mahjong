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
    /// ������
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
    /// �v��ݒ�BNone �Ŕ�\��
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
    /// �v���\������Ă��邩�m�F����
    /// </summary>
    /// <returns></returns>
    public bool CheckVisible()
    {
        return modelMesh.enabled;
    }

    /// <summary>
    /// ePai ����v�̃}�e���A�����擾����
    /// </summary>
    Material getMaterial(ePai type)
    {
        // ���Ȃ�댯�Ȃ���
        return typeMaterials[(int)type-1];
    }
}
