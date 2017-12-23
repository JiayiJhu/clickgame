using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFader : MonoBehaviour
{   //淡入淡出
    public Renderer[] fadeRenderers;
    public float speed = 1f; //淡入淡出的速度

    //------------------------------測試可不可以執行
    [ContextMenu("Test FadeIn")]
    public void TestFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    [ContextMenu("Test FadeOut")]
    public void TestFadeOut()
    {
        StartCoroutine(FadeOut());
    }
   // -------------------------------


    public IEnumerator FadeIn()
    {
        StopCoroutine(FadeOut());
        float alpha = 0;
        ChangeAlpha(alpha);
        while (alpha < 1)
        {
            alpha += Time.deltaTime * speed;
            alpha = Mathf.Min(1,alpha);   //自動從範圍取出最小值
            ChangeAlpha(alpha);
            yield return null;
        }        
    }

    public IEnumerator FadeOut()
    {
        StopCoroutine(FadeIn());
        float alpha = 1;
        ChangeAlpha(alpha);
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * speed;
            alpha = Mathf.Max(0, alpha);   //自動從範圍取出最大值
            ChangeAlpha(alpha);
            yield return null;
        }
    }

    private void ChangeAlpha(float alpha)   //都會用到 所以可以另外提出來寫 讓其他的呼叫ChangeAlpha(alpha);就好 
    {
        for (int i = 0; i < fadeRenderers.Length; i++)
        {
            Color color = fadeRenderers[i].material.color;
            color.a = alpha;
            fadeRenderers[i].material.color = color;
        }
    }
}
