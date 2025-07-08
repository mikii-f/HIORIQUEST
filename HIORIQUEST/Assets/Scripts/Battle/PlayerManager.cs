using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーの情報管理など
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    private const int baseHp = 5000;
    private const int baseAttack = 500;
    private int hp = baseHp;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private TMP_Text hpText;
    private const int baseMp = 5;
    private int mp = 1;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private TMP_Text mpText;
    public int HP { set { hp = Mathf.Clamp(value, 0, (int)hpSlider.maxValue); UpdateHpSlider(); } get { return hp; } }
    public int MP { set { mp = Mathf.Clamp(value, 0, (int)mpSlider.maxValue); UpdateMpSlider();  } get { return mp; } }
    [SerializeField] private RectTransform stateIconsParent;
    private List<Image> stateIcons = new();

    public class SkillInfo
    {
        public string name;
        public int useMP;
        public string detail;
    }

    [HideInInspector] public List<SkillInfo> skills;

    void Awake()
    {
        hpSlider.maxValue = baseHp + EnhanceManager.enhanceStatus.hp;
        HP = Manager.currentHp;
        mpSlider.maxValue = baseMp + EnhanceManager.enhanceStatus.mp;
        MP = Manager.currentMp;
        UpdateSkill();
        for (int i= stateIconsParent.childCount-1; i>=0; i--)
        {
            stateIcons.Add(stateIconsParent.GetChild(i).GetComponent<Image>());
        }
        UpdateStateIcon();
    }

    private void UpdateHpSlider()
    {
        hpSlider.value = hp;
        hpText.text = hp.ToString() + "/" + hpSlider.maxValue.ToString();
        if (hp * 100 / hpSlider.maxValue < 20)
        {
            sliderFill.color = Color.red;
        }
        else
        {
            sliderFill.color = Color.green;
        }
    }

    //バフやデバフの種類に応じた乗算加算の区別を敵味方で共有する場合、係数ごと引数に入れる
    public void Damage(int enemyAttack)
    {
        //トラブル守り発動の判定

        HP -= CalculateDamage(enemyAttack);
        if (hp == 0)
        {
            //ゲームオーバー
            StartCoroutine(battleManager.GameOver());
        }
    }

    //受けるダメージの計算(行動前にダメージ予測を出すため)
    public int CalculateDamage(int enemyAttack)
    {

        return enemyAttack * (100 - PlayerStateManager.damageCutFactor) / 100;
    }

    //相手に与えるダメージの計算
    public int Attack()
    {

        return CalculateAttack();
    }
    public int Break()
    {

        return CalculateAttack() * 3 / 2;
    }
    public int Guard()
    {

        return CalculateAttack() * 3 / 2;
    }
    public int CalculateAttack()
    {
        return (baseAttack + EnhanceManager.enhanceStatus.atk) * PlayerStateManager.attackFactor / 100;
    }

    public void UpdateMpSlider()
    {
        mpSlider.value = mp;
        mpText.text = mp.ToString() + " / " + mpSlider.maxValue.ToString();
    }

    public void UpdateSkill()
    {
        skills = new() { UpdateSkill1(), UpdateSkill2(), UpdateSkill3(), UpdateSkill4() };
    }

    private SkillInfo UpdateSkill1()
    {
        SkillInfo skillInfo = new();
        skillInfo.name = "ほわっとヒール";
        skillInfo.useMP = 1;
        if (!EnhanceManager.enhanceSkill.skill1)
        {
            skillInfo.detail = "勇者灯織のHPを、HP上限の20%(<color=yellow>" + ((int)(hpSlider.maxValue * 0.2f)).ToString()  + "</color>)回復する。";
        }
        else
        {
            skillInfo.detail = "勇者灯織のHPを、HP上限の35%<color=yellow>" + ((int)(hpSlider.maxValue * 0.35f)).ToString() + "</color>回復する。";
        }
        return skillInfo;
    }
    private SkillInfo UpdateSkill2()
    {
        SkillInfo skillInfo = new();
        skillInfo.name = "お願いぴーちゃん";
        skillInfo.useMP = 2;
        skillInfo.detail = "敵の行動を<color=yellow>1つ</color>選んでスタンさせる。";
        return skillInfo;
    }
    private SkillInfo UpdateSkill3()
    {
        SkillInfo skillInfo = new();
        skillInfo.name = "むんっ！";
        skillInfo.useMP = 2;
        skillInfo.detail = "勇者灯織の攻撃力を<color=yellow>30%</color>アップする&被ダメージを<color=yellow>20%</color>カットする。[<color=yellow>2ターン</color>]";
        return skillInfo;
    }
    private SkillInfo UpdateSkill4()
    {
        SkillInfo skillInfo = new();
        skillInfo.name = "まのびーむ";
        skillInfo.useMP = 5;
        skillInfo.detail = "[このターンの行動終了後に発動]\n敵に勇者灯織の攻撃力(<color=yellow>" + CalculateAttack().ToString()  + "</color>)+<color=yellow>2000</color>の確定ダメージを与える。";
        return skillInfo;
    }

    public void Skill1()
    {
        if (!EnhanceManager.enhanceSkill.skill1)
        {
            HP += (int)(hpSlider.maxValue * 0.2f);
        }
        else
        {
            HP += (int)(hpSlider.maxValue * 0.35f);
        }
    }
    public void UpdateStateIcon()
    {
        for (int i=0; i<stateIcons.Count; i++)
        {
            if (i >= PlayerStateManager.buffDebuffs.Count)
            {
                stateIcons[i].color = Color.clear;
            }
            else
            {
                //本来はimage情報を用いて画像を取得
                stateIcons[i].color = Color.white;
            }
        }
    }
}