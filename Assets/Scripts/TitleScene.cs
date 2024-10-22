using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    void Start()
    {
        StartCoroutine(HideCanvasAfterDelay(5f));
    }

    private IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
