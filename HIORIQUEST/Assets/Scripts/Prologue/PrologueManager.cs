using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//プロローグ画面の管理
public class PrologueManager : MonoBehaviour
{
    [SerializeField] private GameObject black;
    [SerializeField] private Text prologueTextBox;
    private string normalText = "ある日、ツバサ村に突如として魔王めぐるが襲来した。\n\n荒らされる田畑、逃げ惑う人々……。\n\n被害者が出るのも時間の問題と誰もが思い始めた頃、\n\n颯爽と現れたのが──そう！　勇者灯織と僧侶真乃である！！！\n\n\n「えっと……、本当に私たちで良いんですか……？」\n\n「まだ旅を始めたばかりだけど……きっと大丈夫！むん！」\n\n「…………お守り、いっぱい持っていこう」";
    private string hardText = "";
    private bool allDisplayed = false;
    [SerializeField] private TMP_Text guideTextBox;

    void Start()
    {
        prologueTextBox.text = "";
        StartCoroutine(WaitText());
    }
    
    private IEnumerator WaitText()
    {
        yield return StartCoroutine(CommonSystem.FadeIn(black, 1));
        if (Manager.mode == 0)
        {
            yield return StartCoroutine(CommonSystem.DisplayText(prologueTextBox, normalText));
        }
        else
        {
            yield return StartCoroutine(CommonSystem.DisplayText(prologueTextBox, hardText));
        }
        allDisplayed = true;
        guideTextBox.text = "TOUCH TO START";
    }

    public void Touch()
    {
        if (!allDisplayed)
        {
            if (Manager.mode == 0)
            {
                prologueTextBox.text = normalText;
            }
            else
            {
                prologueTextBox.text = hardText;
            }
        }
        else
        {
            StartCoroutine(GoToBattle());
        }
    }
    private IEnumerator GoToBattle()
    {
        yield return StartCoroutine(CommonSystem.FadeOut(black, 1));
        if (Manager.mode == 0)
        {
            SceneManager.LoadScene("NormalScene");
        }
        else
        {
            SceneManager.LoadScene("HardScene");
        }
    }
}