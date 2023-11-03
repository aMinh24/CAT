using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StartGame : BaseScreen
{
    public Image img;
    public Image bg;
    public RectTransform rect;
    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        startGame();
        base.Show(data);
    }
    private void startGame()
    {
        Sequence sq = DOTween.Sequence();
        sq.Join(rect.DOScale(new Vector3(1, 1, 0), 1f));
        sq.Join(rect.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360));
        sq.AppendInterval(1f);
        sq.OnComplete(() =>
        {
            Sequence sqq = DOTween.Sequence();
            sqq.Join(rect.DOScale(new Vector3(15, 15, 0), 1f));
            //sqq.Join(rect.DORotate(Vector3.zero, 0.5f));
            sqq.Join(bg.DOFade(0f, 1f));
            sqq.Join(img.DOFade(0f, 1f));
            sqq.OnComplete(() =>
            {
                UIManager.Instance.ShowScreen<IngameUI>(null, true);
            });

        });

        //UIManager.Instance.ShowScreen<MainMenu>(null, true);
    }
}
