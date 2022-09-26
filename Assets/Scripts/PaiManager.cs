using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePai
{
    None,
    Man1,
    Man2,
    Man3,
    Man4,
    Man5,
    Man5b,
    Man6,
    Man7,
    Man8,
    Man9,
    Sou1,
    Sou2,
    Sou3,
    Sou4,
    Sou5,
    Sou5b,
    Sou6,
    Sou7,
    Sou8,
    Sou9,
    Pin1,
    Pin2,
    Pin3,
    Pin4,
    Pin5,
    Pin5b,
    Pin6,
    Pin7,
    Pin8,
    Pin9,
    Ton,
    Nan,
    Sha,
    Pei,
    Haku,
    Hatsu,
    Chun,
}

public class PaiManager : MonoBehaviour
{
    [SerializeField]
    PaiPosGroup     PosGroup;
    [SerializeField]
    PaiModelGroup   ModelGroup;

    PaiPos          cursor;

    void Start()
    {
        PaiPosGroup    pos;
        PaiModelGroup   model;

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Jihai");
        model.SetName($"[Model] Jihai");
        pos.Initialize(model, 12, 1);
        pos.AddModels();
        pos.SetPosition(39.3f, -17.3f);
        pos.SetScale(0.9f);
        pos.SetGameBoard(true);
        pos.SetTypes(new ePai[]
            {
                ePai.Sou1,
                ePai.Sou2,
                ePai.Sou3,
                ePai.Sou4,
                ePai.Sou5,
            }
            );
        pos.Show();

        cursor = pos.GetTopEntity();

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Sutehai");
        model.SetName($"[Model] Sutehai");
        pos.Initialize(model, 6, 2);
        pos.AddModels();
        pos.SetPosition(21.44f, -4.96f);
        pos.SetScale(0.5f);
        pos.SetRotate(0);
        pos.SetGameBoard(true);
        pos.Show();

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Sutehai Simo");
        model.SetName($"[Model] Sutehai Simo");
        pos.Initialize(model, 6, 2);
        pos.AddModels();
        pos.SetPosition(6.72f, -4.82f);
        pos.SetScale(0.5f);
        pos.SetRotate(-90);
        pos.SetGameBoard(true);
        pos.Show();

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Sutehai Toimen");
        model.SetName($"[Model] Sutehai Toimen");
        pos.Initialize(model, 6, 2);
        pos.AddModels();
        pos.SetPosition(6.8f, 9.78f);
        pos.SetScale(0.5f);
        pos.SetRotate(-180);
        pos.SetGameBoard(true);
        pos.Show();

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Sutehai Kami");
        model.SetName($"[Model] Sutehai Kami");
        pos.Initialize(model, 6, 2);
        pos.AddModels();
        pos.SetPosition(21.55f, 9.63f);
        pos.SetScale(0.5f);
        pos.SetRotate(90);
        pos.SetGameBoard(true);
        pos.Show();

        pos  = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Manzu 1-9");
        model.SetName($"[Model] Manzu 1-9");
        pos.Initialize(model, 10, 1);
        pos.AddModels();
        pos.SetPosition(-17.6f, 30.7f);
        pos.SetScale(0.5f);
        pos.SetGameBoard(false);
        pos.SetTypes(new ePai[]
            {
                ePai.Man1,
                ePai.Man2,
                ePai.Man3,
                ePai.Man4,
                ePai.Man5,
                ePai.Man6,
                ePai.Man7,
                ePai.Man8,
                ePai.Man9,
                ePai.Man5b,
            }
        );
        pos.Show();

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Souzu 1-9");
        model.SetName($"[Model] Souzu 1-9");
        pos.Initialize(model, 10, 1);
        pos.AddModels();
        pos.SetPosition(-17.6f, 26.8f);
        pos.SetScale(0.5f);
        pos.SetGameBoard(false);
        pos.SetTypes(new ePai[]
            {
                ePai.Sou1,
                ePai.Sou2,
                ePai.Sou3,
                ePai.Sou4,
                ePai.Sou5,
                ePai.Sou6,
                ePai.Sou7,
                ePai.Sou8,
                ePai.Sou9,
                ePai.Sou5b,
            }
        );
        pos.Show();

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Pinzu 1-9");
        model.SetName($"[Model] Pinzu 1-9");
        pos.Initialize(model, 10, 1);
        pos.AddModels();
        pos.SetPosition(-17.6f, 22.9f);
        pos.SetScale(0.5f);
        pos.SetGameBoard(false);
        pos.SetTypes(new ePai[]
            {
                ePai.Pin1,
                ePai.Pin2,
                ePai.Pin3,
                ePai.Pin4,
                ePai.Pin5,
                ePai.Pin6,
                ePai.Pin7,
                ePai.Pin8,
                ePai.Pin9,
                ePai.Pin5b,
            }
        );
        pos.Show();

        pos   = Instantiate(PosGroup, this.transform);
        model = Instantiate(ModelGroup, this.transform);
        pos.SetName($"Ji 1-9");
        model.SetName($"[Model] Ji 1-9");
        pos.Initialize(model, 7, 1);
        pos.AddModels();
        pos.SetPosition(-17.6f, 19.0f);
        pos.SetScale(0.5f);
        pos.SetGameBoard(false);
        pos.SetTypes(new ePai[]
            {
                ePai.Ton,
                ePai.Nan,
                ePai.Sha,
                ePai.Pei,
                ePai.Haku,
                ePai.Hatsu,
                ePai.Chun,
            }
        );
        pos.Show();

        Selected(cursor, true);
    }

    /// <summary>
    /// ゲームボードの牌を選択。以前選択されていたものは非選択
    /// </summary>
    public void Selected(PaiPos pai, bool isSelect)
    {
        if (pai == null)
        {
            return;
        }

        if (isSelect == true)
        {
            if (cursor != null)
            {
                cursor.Selected(false);
            }
            cursor = pai;
        }
        else
        {
            cursor = null;
        }

        pai.Selected(isSelect);
    }

    /// <summary>
    /// カーソル位置にある牌を設定する
    /// </summary>
    public void SetType(ePai type)
    {
        if (cursor != null)
        {
            cursor.Model.SetType(type);
            Selected(cursor.Next, true);
        }
    }
}
