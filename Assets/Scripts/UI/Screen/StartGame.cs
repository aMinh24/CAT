using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
        if (data is bool b)
        {
            if (b) startGame();
            else restartGame();
        }
        if (data is string s)
        {
            StartCoroutine(replay());
        }
        base.Show(data);
    }
    private IEnumerator replay()
    {
        this.UnregisterAll(EventID.saveData);
        this.UnregisterAll(EventID.LoadData);
        rect.DOScale(Vector3.one, 0f);
        Sequence sq = DOTween.Sequence();
        sq.Join(bg.DOFade(1f, 0.5f));
        sq.Join(img.DOFade(1f, 0.5f));
        yield return new WaitForSeconds(0.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(0);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        

    }
    private void restartGame()
    {
        rect.DOScale(Vector3.one,0f);
        Sequence sq= DOTween.Sequence();
        sq.Join(bg.DOFade(1f, 0.5f));
        sq.Join(img.DOFade(1f, 0.5f));
        sq.OnComplete(() =>
        {
            Sequence sqq = DOTween.Sequence();
            sqq.Join(bg.DOFade(0f, 0.5f));
            sqq.Join(img.DOFade(0f, 0.5f));
            sqq.OnComplete(() =>
            {
                UIManager.Instance.ShowScreen<IngameUI>(null, true);
            });
        });
    }
    private void startGame()
    {
        Sequence sq = DOTween.Sequence();
        sq.Join(rect.DOScale(new Vector3(1, 1, 0), 1f));
        sq.Join(rect.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360));
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
