using System.Collections;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject screen1; 
    public GameObject screen2; 
    public float transitionTime = 1.0f; 

    private void Start()
    {
        screen2.transform.position = new Vector3(screen2.transform.position.x, screen2.transform.position.y, 1);
        SetAlpha(screen2, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(SwitchScreens());
        }
    }

    private IEnumerator SwitchScreens()
    {
        GameObject currentScreen = screen1.activeSelf ? screen1 : screen2;
        GameObject nextScreen = currentScreen == screen1 ? screen2 : screen1;

        nextScreen.SetActive(true);

        float elapsedTime = 0;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            SetAlpha(currentScreen, Mathf.Lerp(1, 0, t)); 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetAlpha(currentScreen, 0);
        currentScreen.SetActive(false); 

        elapsedTime = 0;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            SetAlpha(nextScreen, Mathf.Lerp(0, 1, t)); 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetAlpha(nextScreen, 1);
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
