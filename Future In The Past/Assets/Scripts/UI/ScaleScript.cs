using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 scaleIncrease = new Vector3(1.1f, 1.1f, 1.1f); // ���������� �������
    public float scaleSpeed = 0.1f; // �������� ��������� �������

    private Vector3 originalScale; // �������� ������ ������

    private void Start()
    {
        // ��������� �������� ������ ������
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ����������� ������ ������
        StopAllCoroutines(); // ������������� ����� ������� ��������
        StartCoroutine(ScaleButton(originalScale, originalScale + scaleIncrease));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���������� ������ ������ � ���������
        StopAllCoroutines(); // ������������� ����� ������� ��������
        StartCoroutine(ScaleButton(transform.localScale, originalScale));
    }

    private IEnumerator ScaleButton(Vector3 from, Vector3 to)
    {
        float elapsedTime = 0f;

        while (elapsedTime < scaleSpeed)
        {
            transform.localScale = Vector3.Lerp(from, to, (elapsedTime / scaleSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = to; // ������������� ������������� ������
    }
}