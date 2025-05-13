using UnityEngine;
using Cysharp.Threading.Tasks;
using MIDIFrogs.FutureInThePast;

public class ScreenManager : TimeChangeObserver
{
    public GameObject futureScreen;
    public GameObject pastScreen;
    public float transitionTime = 1.0f;

    private bool isEnabling;
    private bool isDisabling;

    private void Start()
    {
        pastScreen.transform.position = new Vector3(pastScreen.transform.position.x, pastScreen.transform.position.y, 1);
        SetAlpha(pastScreen, 0);
    }

    public override bool CanSwitchTime() => !isEnabling && !isDisabling;

    public override void OnEnterFuture() => FadeIn(futureScreen).Forget();

    public override void OnEnterPast() => FadeIn(pastScreen).Forget();

    public override void OnLeaveFuture() => FadeOut(futureScreen).Forget();

    public override void OnLeavePast() => FadeOut(pastScreen).Forget();
    
    private async UniTaskVoid FadeOut(GameObject obj)
    {
        isDisabling = true;
        float elapsedTime = 0;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            SetAlpha(obj, Mathf.Lerp(1, 0, t));
            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        SetAlpha(obj, 0);
        obj.SetActive(false);
        isDisabling = false;
    }

    private async UniTaskVoid FadeIn(GameObject obj)
    {
        isEnabling = true;
        obj.SetActive(true);
        float elapsedTime = 0;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            SetAlpha(obj, Mathf.Lerp(0, 1, t));
            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        SetAlpha(obj, 1);
        isEnabling = false;
    }

    private void SetAlpha(GameObject obj, float alpha)
    {
        SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha; // Устанавливаем альфа
            renderer.color = color;
        }
    }
}
