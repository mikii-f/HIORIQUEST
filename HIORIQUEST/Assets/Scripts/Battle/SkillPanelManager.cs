using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//スキル選択の管理
public class SkillPanelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private RectTransform skillPanel;
    [SerializeField] private GameObject detailPanel;
    private RectTransform detailPanelRect;
    [SerializeField] private Text detailText;
    public GameObject skillButtonMask;
    private int skill = -1;

    void Start()
    {
        detailPanelRect = detailPanel.GetComponent<RectTransform>();
        skillButtonMask.SetActive(false);
    }

    public void SelectSkill1()
    {
        detailPanelRect.anchoredPosition = new(40, 0);
        detailText.text = playerManager.skills[0].detail;
        detailPanel.SetActive(true);
        skill = 0;
    }
    public void SelectSkill2()
    {
        detailPanelRect.anchoredPosition = new(40, -80);
        detailText.text = playerManager.skills[1].detail;
        detailPanel.SetActive(true);
        skill = 1;
    }
    public void SelectSkill3()
    {
        detailPanelRect.anchoredPosition = new(40, -160);
        detailText.text = playerManager.skills[2].detail;
        detailPanel.SetActive(true);
        skill = 2;
    }
    public void SelectSkill4()
    {
        detailPanelRect.anchoredPosition = new(40, -240);
        detailText.text = playerManager.skills[3].detail;
        detailPanel.SetActive(true);
        skill = 3;
    }

    public void DecideSkill()
    {
        detailPanel.SetActive(false);
        skillButtonMask.SetActive(true);
        StartCoroutine(UseSkill());
    }
    private IEnumerator UseSkill()
    {
        gameObject.SetActive(false);    //非アクティブなオブジェクトのコルーチンは呼び出せない？
        //スキルのアニメーション呼び出し(プレハブで作成？)とBattleManagerへの通知
        switch (skill)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            default:
                break;
        }
        yield return null;
    }

    public void ClosePanel()
    {
        if (detailPanel.activeSelf)
        {
            detailPanel.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        playerManager.UpdateSkill();
    }
    private void OnDisable()
    {
        skill = -1;
    }
}