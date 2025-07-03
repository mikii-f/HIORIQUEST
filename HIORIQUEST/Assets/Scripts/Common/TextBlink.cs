using System.Collections;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    private TMP_Text t;
    void Start()
    {
        t = GetComponent<TMP_Text>();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            while (t.color.a > 0.1f)
            {
                t.color -= new Color(0, 0, 0, Time.deltaTime * 1.5f);
                yield return null;
            }
            while (t.color.a < 1)
            {
                t.color += new Color(0, 0, 0, Time.deltaTime * 2);
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
