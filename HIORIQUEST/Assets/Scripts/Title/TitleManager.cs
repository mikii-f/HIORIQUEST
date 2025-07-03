using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//タイトル画面の管理
public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject black;
    //[SerializeField] private GameObject howtoPanel;
    [SerializeField] private GameObject levelSelectPanel;

    void Start()
    {
        //howtoPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        StartCoroutine(CommonSystem.FadeIn(black, 1));
    }

    public void StartButtton()
    {
        levelSelectPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        levelSelectPanel.SetActive(false);
    }
    public void NormalMode()
    {
        Manager.mode = 0;
        StartCoroutine(GoToPrologue());
    }
    public void HardMode()
    {
        Manager.mode = 1;
        StartCoroutine(GoToPrologue());
    }
    private IEnumerator GoToPrologue()
    {
        yield return StartCoroutine(CommonSystem.FadeOut(black, 1));
        SceneManager.LoadScene("PrologueScene");
    }
}