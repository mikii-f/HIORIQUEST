using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//スキル選択の管理
public class SkillPanelManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private GameObject detailPanel;
    private RectTransform detailPanelRect;
    [SerializeField] private Text detailText;
    public GameObject skillButtonMask;
    private int skill = -1;
    [SerializeField] private GameObject skill1Mask;
    [SerializeField] private GameObject skill2Mask;
    [SerializeField] private GameObject skill3Mask;
    [SerializeField] private GameObject skill4Mask;
    [SerializeField] private GameObject skillUseMask;

    void Start()
    {
        detailPanelRect = detailPanel.GetComponent<RectTransform>();
        skillButtonMask.SetActive(false);
    }

    public void SelectSkill1()
    {
        DisplayDetail(0);
    }
    public void SelectSkill2()
    {
        DisplayDetail(1);
    }
    public void SelectSkill3()
    {
        DisplayDetail(2);
    }
    public void SelectSkill4()
    {
        DisplayDetail(3);
    }
    private void DisplayDetail(int n)
    {
        detailPanelRect.anchoredPosition = new(40, -80 * n);
        detailText.text = playerManager.skills[n].detail;
        detailPanel.SetActive(true);
        skill = n;
        if (playerManager.MP < playerManager.skills[n].useMP)
        {
            skillUseMask.SetActive(true);
        }
        else
        {
            skillUseMask.SetActive(false);
        }
    }
    public void DecideSkill()
    {
        detailPanel.SetActive(false);
        skillButtonMask.SetActive(true);
        StartCoroutine(UseSkill());
    }
    private IEnumerator UseSkill()
    {
        skillPanel.SetActive(false);
        //スキルのアニメーション呼び出し(プレハブで作成？)とBattleManagerへの通知
        switch (skill)
        {
            case 0:

                battleManager.Skill1();
                break;
            case 1:

                battleManager.Skill2();
                break;
            case 2:

                battleManager.Skill3();
                break;
            case 3:

                battleManager.Skill4();
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);    //非アクティブなオブジェクトのコルーチンは止まってしまう
    }

    private void ManageMasks()
    {
        if (playerManager.MP < playerManager.skills[0].useMP)
        {
            skill1Mask.SetActive(true);
        }
        else
        {
            skill1Mask.SetActive(false);
        }
        if (playerManager.MP < playerManager.skills[1].useMP)
        {
            skill2Mask.SetActive(true);
        }
        else
        {
            skill2Mask.SetActive(false);
        }
        if (playerManager.MP < playerManager.skills[2].useMP)
        {
            skill3Mask.SetActive(true);
        }
        else
        {
            skill3Mask.SetActive(false);
        }
        if (playerManager.MP < playerManager.skills[3].useMP)
        {
            skill4Mask.SetActive(true);
        }
        else
        {
            skill4Mask.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        detailPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        skillPanel.SetActive(true);
        playerManager.UpdateSkill();
        ManageMasks();
    }
    private void OnDisable()
    {
        skill = -1;
    }
}