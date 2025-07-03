using UnityEngine;

//ボタン自体に持たせる
public class ButtonManager : MonoBehaviour
{
    private RectTransform myRect;
    void Start()
    {
        myRect = GetComponent<RectTransform>();
    }
    //ボタンをタッチ
    public void ButtonTouch()
    {
        myRect.localScale = new(1.1f, 1.1f);
    }
    //ボタンから離れるまたはタッチが完了する
    public void ButtonUntouch()
    {
        myRect.localScale = Vector2.one;
    }
}