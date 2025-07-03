using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//フェードなど基本的なシステム
public class CommonSystem : MonoBehaviour
{
    //テキストの表示
    public static IEnumerator DisplayText(Text textBox, string s)
    {
        int pos = 0;
        while (pos < s.Length && textBox.text.Length < s.Length)
        {
            textBox.text += s[pos];
            if (s[pos] == '\n')
            {
                yield return new WaitForSeconds(0.33f);
            }
            yield return new WaitForSeconds(0.05f);
            pos++;
        }
    }

    public static IEnumerator FadeIn(GameObject black, float t)
    {
        Image b = black.GetComponent<Image>();
        while (b.color.a > 0)
        {
            b.color -= new Color(0, 0, 0, Time.deltaTime / t);
            yield return null;
        }
        b.color = Color.clear;
        black.SetActive(false);
    }
    public static IEnumerator FadeOut(GameObject black, float t)
    {
        black.SetActive(true);
        Image b = black.GetComponent<Image>();
        while (b.color.a < 1)
        {
            b.color += new Color(0, 0, 0, Time.deltaTime / t);
            yield return null;
        }
        b.color = Color.black;
    }
}