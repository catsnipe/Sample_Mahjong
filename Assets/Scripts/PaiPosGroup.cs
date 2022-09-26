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

    // ドラッグ時の情報
    PaiModel         dragModel;
    Result           resBegin;
    Result           resDrag;

    static float     INIT_DISTANCE = Screen.width * Screen.width + Screen.height * Screen.height;

    /// <summary>
    /// 枠グループに追従するモデルグループを定義する
    /// 縦横方向の牌の数を定義する
    /// </summary>
    /// <param name="group">モデルグループ</param>
    /// <param name="xnumber">横の牌の数</param>
    /// <param name="ynumber">縦の牌の数</param>
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
    /// ドラッグ開始
    /// </summary>
    /// <param name="pos">ドラッグ開始時の枠</param>
    /// <param name="model">ドラッグした牌</param>
    public void BeginDrag(PaiPos pos, PaiModel model)
    {
        dragModel = model;

        dragOn(true);

        resBegin = getNearestPosNo(model.transform.localPosition);
    }

    /// <summary>
    /// ドラッグ
    /// </summary>
    /// <param name="mousePos">マウス位置</param>
    public void Drag(Vector2 mousePos)
    {
        // 選択されたobjectのスクリーン座標を取得（Z 値を使う）
        Vector3 objectPoint
            = MainCamera.WorldToScreenPoint(dragModel.transform.position);
 
        // マウス位置
        Vector3 pointScreen = new Vector3(mousePos.x, mousePos.y, objectPoint.z);
        
        // マウス位置を３次元座標に変換
        Vector3 pointWorld = MainCamera.ScreenToWorldPoint(pointScreen);
        pointWorld.z = 0.1f;
        
        // モデルの位置を更新
        dragModel.transform.position = pointWorld;

        // 現在ドラッグ中の牌が１番近い枠の場所を求める
        resDrag = getNearestPosNo(dragModel.transform.localPosition);

        // ドラッグ中は、距離 30 以下になるまで他の牌は動かさない
        if (resDrag.Distance < 30)
        {
            replaceDragPai();

            // 枠カーソルの変更
            var pos = GetEntity(resDrag.PosNo);
            if (pos != null)
            {
                pos.ManagerSelected(true);
            }

            resBegin = resDrag;
        }
    }

    /// <summary>
    /// ドラッグ終了
    /// </summary>
    public void EndDrag()
    {
        // ドラッグした牌をカーソルの位置に飛ばす
        PaiPos w = GetEntity(resBegin.PosNo);
        w.MoveToPos();

        dragOn(false);

        dragModel = null;
    }

    /// <summary>
    /// ドラッグした牌を少し手前に動かし、手に取った感じを出す
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
    /// ドラッグし、移動した牌に合わせて他の牌を動かす
    /// </summary>
    void replaceDragPai()
    {
        if (resDrag.PosNo > resBegin.PosNo)
        {
            // 牌を入れ替える（左..ドラッグ元、右..ドラッグ先）
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
            // 牌を入れ替える（右..ドラッグ元、左..ドラッグ先）
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
    /// 枠グループ名
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

        modelGroup?.SetPosition(x, y);
    }

    /// <summary>
    /// スケール
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
    /// 回転。自家0、下家90、対面180、上家270
    /// </summary>
    public void SetRotate(float angle)
    {
        var rotate = this.transform.localEulerAngles;
        rotate.z = angle;
        this.transform.localEulerAngles = rotate;

        modelGroup?.SetRotate(angle);
    }

    /// <summary>
    /// 枠と同じ位置にモデルを全て生成
    /// </summary>
    public void AddModels()
    {
        foreach (var pos in poss)
        {
            pos.LinkModel(modelGroup?.AddModel(pos));
        }
    }

    /// <summary>
    /// モデルの牌タイプを連続設定
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
    /// 牌がゲームボード上のものであれば true、牌追加用アイコンであれば false
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
    /// 表示
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
    /// 先頭の牌を選択
    /// </summary>
    public PaiPos GetTopEntity()
    {
        return poss.Count > 0 ? poss[0] : null;
    }

    /// <summary>
    /// ｎ番目の牌を選択
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
    /// ドラッグ中の牌に一番近い枠番号、その枠への距離（√していない）を取得
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
