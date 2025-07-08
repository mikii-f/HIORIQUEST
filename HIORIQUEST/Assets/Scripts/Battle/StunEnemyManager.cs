using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//敵をスタンさせる時に表示される画面の管理
public class StunEnemyManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private RectTransform cardParent;
    [SerializeField] private Text guideText;
    private List<StunCardManager> stunCards = new();
    private int stunNumber = 1;

    public void SetEnemyCard(int card, int stun, List<int> enemyAction, List<bool> fortunes)
    {
        guideText.text = "あと <color=yellow>" + stun.ToString() + "</color> 枚選択可能";
        stunNumber = stun;
        for (int i = 0; i < card; i++)
        {
            GameObject stunCard = Instantiate(Resources.Load<GameObject>("Prefabs/StunCard"), cardParent);
            stunCard.GetComponent<RectTransform>().anchoredPosition = new(-600 + 1200 * i / (card - 1), 0);
            StunCardManager stunCardManager = stunCard.GetComponent<StunCardManager>();
            if (fortunes[i])
            {
                stunCardManager.SetCard(enemyAction[i]);
            }
            else
            {
                stunCardManager.SetCard(0);
            }
            stunCards.Add(stunCardManager);
        }
    }

    public bool JudgeSelect(bool request)
    {
        int select = CountSelectCard();
        if (!request)
        {
            guideText.text = "あと <color=yellow>" + (stunNumber - select + 1).ToString() + "</color> 枚選択可能";
            return true;
        }
        else
        {
            if (select < stunNumber)
            {
                guideText.text = "あと <color=yellow>" + (stunNumber - select - 1).ToString() + "</color> 枚選択可能";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private int CountSelectCard()
    {
        int select = 0;
        foreach (StunCardManager sc in stunCards)
        {
            if (sc.isSelect)
            {
                select++;
            }
        }
        return select;
    }

    public void Decide()
    {
        //本来はスタン対象が最大まで選択されているかで分岐
        StartCoroutine(StunAnimation());
    }
    private IEnumerator StunAnimation()
    {
        for(int i=0; i<stunCards.Count; i++)
        {
            if (stunCards[i].isSelect)
            {
                stunCards[i].Stun();
                battleManager.StunEnemy(i);
            }
        }
        yield return new WaitForSeconds(1);
        foreach (StunCardManager sc in stunCards)
        {
            Destroy(sc);
        }
        stunCards.Clear();
        gameObject.SetActive(false);
    }
}