using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiPosGroup : MonoBehaviour
{
    [SerializeField]
    Camera           MainCamera;

    [SerializeField]
    PaiPos           PrefabPos;

    [System.NonSerialized]
    public bool      IsGameBoard;

    class Result
    {
        public int   PosNo;
        public float Distance;
    }

    PaiModelGroup    modelGroup;
    List<PaiPos>     poss;

    // �h���b�O���̏��
    PaiModel         dragModel;
    Result           resBegin;
    Result           resDrag;

    static float     INIT_DISTANCE = Screen.width * Screen.width + Screen.height * Screen.height;

    /// <summary>
    /// �g�O���[�v�ɒǏ]���郂�f���O���[�v���`����
    /// �c�������̔v�̐����`����
    /// </summary>
    /// <param name="group">���f���O���[�v</param>
    /// <param name="xnumber">���̔v�̐�</param>
    /// <param name="ynumber">�c�̔v�̐�</param>
    public void Initialize(PaiModelGroup group, int xnumber, int ynumber)
    {
        modelGroup = group;
        modelGroup?.Initialize();

        poss       = new List<PaiPos>();

        for (int y = 0; y < ynumber; y++)
        {
            for (int x = 0; x < xnumber; x++)
            {
                var pos = Instantiate(PrefabPos, this.transform);
                pos.Initialize(pos);
                pos.PosNo = poss.Count;

                Vector3 v = pos.transform.localPosition;
                v.x = -2.3f - x * 4.4f;
                v.y = -3.1f - y * 6.7f;
                pos.transform.localPosition = v;
                pos.gameObject.name = $"{x}, {y}";

                poss.Add(pos);
            }
        }

        for (int i = 0; i < poss.Count; i++)
        {
            int next = i+1;
            if (next >= poss.Count)
            {
                next = 0;
            }

            poss[i].Next = poss[next];
        }
    }

    /// <summary>
    /// �h���b�O�J�n
    /// </summary>
    /// <param name="pos">�h���b�O�J�n���̘g</param>
    /// <param name="model">�h���b�O�����v</param>
    public void BeginDrag(PaiPos pos, PaiModel model)
    {
        dragModel = model;

        dragOn(true);

        resBegin = getNearestPosNo(model.transform.localPosition);
    }

    /// <summary>
    /// �h���b�O
    /// </summary>
    /// <param name="mousePos">�}�E�X�ʒu</param>
    public void Drag(Vector2 mousePos)
    {
        // �I�����ꂽobject�̃X�N���[�����W���擾�iZ �l���g���j
        Vector3 objectPoint
            = MainCamera.WorldToScreenPoint(dragModel.transform.position);
 
        // �}�E�X�ʒu
        Vector3 pointScreen = new Vector3(mousePos.x, mousePos.y, objectPoint.z);
        
        // �}�E�X�ʒu���R�������W�ɕϊ�
        Vector3 pointWorld = MainCamera.ScreenToWorldPoint(pointScreen);
        pointWorld.z = 0.1f;
        
        // ���f���̈ʒu���X�V
        dragModel.transform.position = pointWorld;

        // ���݃h���b�O���̔v���P�ԋ߂��g�̏ꏊ�����߂�
        resDrag = getNearestPosNo(dragModel.transform.localPosition);

        // �h���b�O���́A���� 30 �ȉ��ɂȂ�܂ő��̔v�͓������Ȃ�
        if (resDrag.Distance < 30)
        {
            replaceDragPai();

            // �g�J�[�\���̕ύX
            var pos = GetEntity(resDrag.PosNo);
            if (pos != null)
            {
                pos.ManagerSelected(true);
            }

            resBegin = resDrag;
        }
    }

    /// <summary>
    /// �h���b�O�I��
    /// </summary>
    public void EndDrag()
    {
        // �h���b�O�����v���J�[�\���̈ʒu�ɔ�΂�
        PaiPos w = GetEntity(resBegin.PosNo);
        w.MoveToPos();

        dragOn(false);

        dragModel = null;
    }

    /// <summary>
    /// �h���b�O�����v��������O�ɓ������A��Ɏ�����������o��
    /// </summary>
    void dragOn(bool on)
    {
        var pos = dragModel.transform.localPosition;
        pos.z   = on == true ? 0.1f : 0.0f;
        dragModel.transform.localPosition = pos;

        var scale = dragModel.transform.localScale;
        scale.x = on == true ? 1.1f : 1.0f;
        scale.y = on == true ? 1.1f : 1.0f;
        dragModel.transform.localScale = scale;
    }

    /// <summary>
    /// �h���b�O���A�ړ������v�ɍ��킹�đ��̔v�𓮂���
    /// </summary>
    void replaceDragPai()
    {
        if (resDrag.PosNo > resBegin.PosNo)
        {
            // �v�����ւ���i��..�h���b�O���A�E..�h���b�O��j
            for (int i = resBegin.PosNo; i < resDrag.PosNo; i++)
            {
                PaiPos w0 = GetEntity(i);
                PaiPos w1 = GetEntity(i+1);

                var model = w0.Model;
                w0.LinkModel(w1.Model);
                w0.MoveToPos();
                w1.LinkModel(model);
            }
        }
        else
        if (resDrag.PosNo < resBegin.PosNo)
        {
            // �v�����ւ���i�E..�h���b�O���A��..�h���b�O��j
            for (int i = resBegin.PosNo; i > resDrag.PosNo; i--)
            {
                PaiPos w0 = GetEntity(i);
                PaiPos w1 = GetEntity(i-1);

                var model = w0.Model;
                w0.LinkModel(w1.Model);
                w0.MoveToPos();
                w1.LinkModel(model);
            }
        }
    }

    /// <summary>
    /// �g�O���[�v��
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

        modelGroup?.SetPosition(x, y);
    }

    /// <summary>
    /// �X�P�[��
    /// </summary>
    public void SetScale(float size)
    {
        var scale = this.transform.localScale;
        scale.x = size;
        scale.y = size;
        this.transform.localScale = scale;

        modelGroup?.SetScale(size);
    }

    /// <summary>
    /// ��]�B����0�A����90�A�Ζ�180�A���270
    /// </summary>
    public void SetRotate(float angle)
    {
        var rotate = this.transform.localEulerAngles;
        rotate.z = angle;
        this.transform.localEulerAngles = rotate;

        modelGroup?.SetRotate(angle);
    }

    /// <summary>
    /// �g�Ɠ����ʒu�Ƀ��f����S�Đ���
    /// </summary>
    public void AddModels()
    {
        foreach (var pos in poss)
        {
            pos.LinkModel(modelGroup?.AddModel(pos));
        }
    }

    /// <summary>
    /// ���f���̔v�^�C�v��A���ݒ�
    /// </summary>
    /// <param name="types"></param>
    public void SetTypes(ePai[] types)
    {
        int cnt = 0;

        foreach (var type in types)
        {
            modelGroup?.SetType(cnt++, type);
        }
    }

    /// <summary>
    /// �v���Q�[���{�[�h��̂��̂ł���� true�A�v�ǉ��p�A�C�R���ł���� false
    /// </summary>
    public void SetGameBoard(bool isGameBoard)
    {
        foreach (var pos in poss)
        {
            pos.VisiblePosFrame(isGameBoard);
        }
        IsGameBoard = isGameBoard;
    }

    /// <summary>
    /// �\��
    /// </summary>
    public void Show()
    {
        foreach (var pos in poss)
        {
            pos.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(true);

        modelGroup?.Show();
    }

    /// <summary>
    /// �擪�̔v��I��
    /// </summary>
    public PaiPos GetTopEntity()
    {
        return poss.Count > 0 ? poss[0] : null;
    }

    /// <summary>
    /// ���Ԗڂ̔v��I��
    /// </summary>
    public PaiPos GetEntity(int no)
    {
        if (no < 0 || no >= poss.Count)
        {
            return null;
        }
        return poss[no];
    }

    /// <summary>
    /// �h���b�O���̔v�Ɉ�ԋ߂��g�ԍ��A���̘g�ւ̋����i�サ�Ă��Ȃ��j���擾
    /// </summary>
    Result getNearestPosNo(Vector3 loc)
    {
        float   distance  = INIT_DISTANCE;
        int     nearPosNo = -1;

        for (int i = 0; i < poss.Count; i++)
        {
            PaiPos pos = poss[i];

            float   x    = pos.transform.localPosition.x - loc.x;
            float   y    = pos.transform.localPosition.y - loc.y;
            float   d    = x*x + y*y;

            if (d < distance)
            {
                distance   = d;
                nearPosNo = i;
            }
        }

//Debug.Log($"{distance}");

        return new Result() { PosNo = nearPosNo, Distance = distance };
    }


}
