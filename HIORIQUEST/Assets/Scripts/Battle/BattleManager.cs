using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//バトル全体の管理(担うタスクが増えないように注意)
public class BattleManager : MonoBehaviour
{
    //基本システム系
    [SerializeField] private GameObject black;
    [SerializeField] private Text battleNumberText;
    [SerializeField] private Text turnText;
    private int turn = 1;
    [SerializeField] private RectTransform enemyParent;
    [HideInInspector] public EnemyManager enemyManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private SkillPanelManager skillPanelManager;
    [SerializeField] private StunEnemyManager stunEnemyManager;
    [SerializeField] private GameObject buttonHide;

    //占いフェーズ系
    [SerializeField] private GameObject fortuneTellingPhase;
    [SerializeField] private RectTransform fortuneGageParent;
    private List<FortuneGageManager> fortuneGages = new();
    [HideInInspector] public int fortuneTellingResult = -1;
    private List<bool> fortunes = new();
    private List<int> enemyAction = new();

    //行動フェーズ系
    [SerializeField] private GameObject actionPhase;
    [SerializeField] private RectTransform cardParent;
    private List<CardManager> cards = new();
    [SerializeField] private GameObject enemyDetailPanel;
    [SerializeField] private GameObject hioriDetailPanel;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private GameObject skill4Icon;
    [SerializeField] private GameObject stunEnemyPanel;
    private List<int> hioriAction = new();
    private bool isSkillUse = false;

    //ゲーム進行系
    private bool isWin = false;
    private bool isLose = false;
    [SerializeField] private GameObject configPanel;

    void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        battleNumberText.text = Manager.battleCount.ToString() + " / 6";
        //本来はMamagerなどを元に敵を生成
        GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Debitaro"), enemyParent);
        enemyManager = enemy.GetComponent<EnemyManager>();
        enemyManager.StatusSet(2000, 500, this);
    }

    void Start()
    {
        fortuneTellingPhase.SetActive(false);
        actionPhase.SetActive(false);
        enemyDetailPanel.SetActive(false);
        hioriDetailPanel.SetActive(false);
        skillPanel.SetActive(false);
        stunEnemyPanel.SetActive(false);
        configPanel.SetActive(false);
        StartCoroutine(StartBattle());
    }
    private IEnumerator StartBattle()
    {
        yield return StartCoroutine(CommonSystem.WipeIn(black));
        yield return new WaitForSeconds(1);
        fortuneTellingPhase.SetActive(true);
        StartCoroutine(FortuneTelling(3)); //本来は敵に応じてゲージの本数を変更
    }

    private IEnumerator FortuneTelling(int n)
    {
        List<int> ftResults = new();
        for (int i=0; i<n; i++)
        {
            GameObject gage = Instantiate(Resources.Load<GameObject>("Prefabs/FortuneGage1"), fortuneGageParent);
            gage.GetComponent<RectTransform>().anchoredPosition = new(-600 + 1200 * i / (n - 1), 0);
            fortuneGages.Add(gage.GetComponent<FortuneGageManager>());
        }
        yield return new WaitForSeconds(0.25f);
        for (int i=0; i<n; i++)
        {
            fortuneGages[i].StartMove();
            yield return new WaitUntil(() => fortuneTellingResult != -1);
            ftResults.Add(fortuneTellingResult);
            fortuneTellingResult = -1;
        }
        yield return new WaitForSeconds(1);
        //確率に応じて敵の行動開示
        for (int i=0; i<n; i++)
        {
            enemyAction.Add(UnityEngine.Random.Range(1, 4));    //敵によっては重みを変える場合も検討
            int x = UnityEngine.Random.Range(0, 10);
            switch (ftResults[i])
            {
                case 0:
                    fortunes.Add(true);
                    break;
                case 1:
                    if (x < 5)
                    {
                        fortunes.Add(true);
                    }
                    else
                    {
                        fortunes.Add(false);
                    }
                    break;
                case 2:
                    if (x < 2)
                    {
                        fortunes.Add(true);
                    }
                    else
                    {
                        fortunes.Add(false);
                    }
                    break;
                case 3:
                    fortunes.Add(false);
                    break;
                default:
                    Debug.Log("Fortune number error");
                    break;
            }
        }
        CardSet(n);
        fortuneTellingPhase.SetActive(false);
        for (int i=0; i<n; i++)
        {
            Destroy(fortuneGages[i]);
        }
        fortuneGages.Clear();


        //敵のスキル使用など？

        buttonHide.SetActive(false);
    }

    private void CardSet(int n)
    {
        int[] left = {-350, -500, -550, -700};
        int[] space = {350, 300, 250, 240};
        for (int i = 0; i < n; i++)
        {
            GameObject card = Instantiate(Resources.Load<GameObject>("Prefabs/Cards"), cardParent);
            card.GetComponent<RectTransform>().anchoredPosition = new(left[n-3] + space[n-3] * i, 0);
            CardManager cardManager = card.GetComponent<CardManager>();
            if (fortunes[i])
            {
                cardManager.CardSet(enemyAction[i]);
            }
            else
            {
                cardManager.CardSet(0);
            }
            cards.Add(cardManager);
        }
    }
    public void DisplayEnemyDetail()
    {
        enemyDetailPanel.SetActive(true);
    }
    public void DisplayHioriDetail()
    {
        hioriDetailPanel.SetActive(true);
    }
    public void DisplaySkillPanel()
    {
        skillPanel.SetActive(true);
    }

    //スキルの内部処理およびUI操作が必要な部分(演出はSkillPanelManagerが担当)
    public void Skill1()
    {
        playerManager.MP -= playerManager.skills[0].useMP;
        isSkillUse = true;
        playerManager.Skill1();
    }
    public void Skill2()
    {
        playerManager.MP -= playerManager.skills[1].useMP;
        isSkillUse = true;
        stunEnemyPanel.SetActive(true);
        stunEnemyManager.SetEnemyCard(enemyAction.Count, 1, enemyAction, fortunes);
    }
    public void Skill3()
    {
        playerManager.MP -= playerManager.skills[2].useMP;
        isSkillUse = true;
        PlayerStateManager.AttackBD attackBD = new()
        {
            turn = 2,
            value = 30
        };
        attackBD.Apply();
        PlayerStateManager.buffDebuffs.Add(attackBD);
        PlayerStateManager.DamageCutBD damageCutBD = new()
        {
            turn = 2,
            value = 20
        };
        damageCutBD.Apply();
        PlayerStateManager.buffDebuffs.Add(damageCutBD);
        playerManager.UpdateStateIcon();
    }
    public void Skill4()
    {
        isSkillUse = true;
        skill4Icon.SetActive(true);
    }
    public void StunEnemy(int n)
    {
        enemyAction[n] = 4;
        cards[n].CardSet(4);
    }

    public void ActionPhaseOnOff()
    {
        actionPhase.SetActive(!actionPhase.activeSelf);
    }
    public void DecideAction()
    {
        for (int i=0; i<cards.Count; i++)
        {
            hioriAction.Add(cards[i].currentCard);
            Destroy(cards[i]);
        }
        cards.Clear();
        buttonHide.SetActive(true);
        actionPhase.SetActive(false);
        StartCoroutine(Action());
    }
    private IEnumerator Action()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i=0; i<enemyAction.Count; i++)
        {
            OneAction(i);
            yield return new WaitForSeconds(0.5f);
            if (isWin || isLose)
            {
                break;
            }
        }
        //まのびーむ発動
        if (skill4Icon.activeSelf && (!isWin && !isLose))
        {
            skill4Icon.SetActive(false);
            playerManager.MP -= playerManager.skills[3].useMP;
            enemyManager.Damage(playerManager.CalculateAttack() + 2000);
            yield return new WaitForSeconds(0.5f);
        }
        //暫定 負けた場合はプラスしない その他演出的に呼び出しタイミングを変えるべき場合も想定
        if (!isSkillUse)
        {
            playerManager.MP++;
        }
        if (!isWin && !isLose)
        {
            yield return new WaitForSeconds(1);
            turn++;
            turnText.text = turn.ToString() + "ターン目";
            fortunes.Clear();
            enemyAction.Clear();
            hioriAction.Clear();
            isSkillUse = false;
            skillPanelManager.skillButtonMask.SetActive(false);
            PlayerStateManager.PassTurn();
            playerManager.UpdateStateIcon();
            yield return new WaitForSeconds(1);
            fortuneTellingPhase.SetActive(true);
            StartCoroutine(FortuneTelling(3)); //本来は敵に応じてゲージの本数を変更
        }
    }
    private void OneAction(int n)
    {
        if (enemyAction[n] == 1)
        {
            if (hioriAction[n] == 1)
            {
                enemyManager.Damage(playerManager.Attack());
                playerManager.Damage(enemyManager.Attack());
            }
            else if (hioriAction[n] == 2)
            {
                playerManager.Damage(enemyManager.Attack() * 3 / 2);
            }
            else if (hioriAction[n] == 3)
            {
                enemyManager.Damage(playerManager.Guard());
            }
        }
        else if (enemyAction[n] == 2)
        {
            if (hioriAction[n] == 1)
            {
                enemyManager.Damage(playerManager.Attack() * 3 / 2);
            }
            else if (hioriAction[n] == 2)
            {
                enemyManager.Damage(playerManager.Break() * 3 / 2);
                playerManager.Damage(enemyManager.Break() * 3 / 2);
            }
            else if (hioriAction[n] == 3)
            {
                playerManager.Damage(enemyManager.Break());
            }
        }
        else if (enemyAction[n] == 3)
        {
            if (hioriAction[n] == 1)
            {
                playerManager.Damage(enemyManager.Guard());
            }
            else if (hioriAction[n] == 2)
            {
                enemyManager.Damage(playerManager.Break());
            }
            else if (hioriAction[n] == 3)
            {
                
            }
        }
        else if (enemyAction[n] == 4)
        {
            if (hioriAction[n] == 1)
            {
                enemyManager.Damage(playerManager.Attack());
            }
            else if (hioriAction[n] == 2)
            {
                enemyManager.Damage(playerManager.Break());
            }
            else if (hioriAction[n] == 3)
            {
                
            }
        }
    }
    public IEnumerator Win()
    {
        isWin = true;
        yield return new WaitForSeconds(1);
        Manager.battleCount++;
        Manager.currentHp = playerManager.HP;
        Manager.currentMp = playerManager.MP;
        skill4Icon.SetActive(false);
        if (Manager.battleCount == 7)
        {
            //暫定
            Image b = black.GetComponent<Image>();
            RectTransform rect = black.GetComponent<RectTransform>();
            b.color = Color.clear;
            rect.anchoredPosition = Vector2.zero;
            yield return StartCoroutine(CommonSystem.FadeOut(black, 1));
            Manager.Initialize();
            SceneManager.LoadScene("TitleScene");
        }
        else
        {
            PlayerStateManager.PassTurn();
            playerManager.UpdateStateIcon();
            yield return StartCoroutine(CommonSystem.WipeOut(black));
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("NormalScene");
        }
    }
    public IEnumerator GameOver()
    {
        isLose = true;
        yield return new WaitForSeconds(1);

        //暫定(負けてもリザルト画面に進めるようにする)
        Image b = black.GetComponent<Image>();
        RectTransform rect = black.GetComponent<RectTransform>();
        b.color = Color.clear;
        rect.anchoredPosition = Vector2.zero;
        yield return StartCoroutine(CommonSystem.FadeOut(black, 1));
        Manager.Initialize();
        SceneManager.LoadScene("TitleScene");
    }

    public void DisplayConfig()
    {
        configPanel.SetActive(true);
    }
}