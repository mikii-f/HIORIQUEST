using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーの情報管理など
public class PlayerManager : MonoBehaviour
{
    private const int baseHp = 5000;
    private const int baseAttack = 500;
    [HideInInspector] public int hp = baseHp;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private TMP_Text hpText;

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
        hp = Manager.currentHp;
        UpdateHp();
        UpdateSkill();
    }

    private void UpdateHp()
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

    public void Damage(int damage)
    {
        hp -= Mathf.Min(hp, damage);
        UpdateHp();
        if (hp == 0)
        {
            //トラブル守り発動orゲームオーバー

        }
    }

    //ダメージ計算系(本来は色々係数などがかかる)
    public int Attack()
    {
        return baseAttack;
    }
    public int Break()
    {
        return baseAttack * 3 / 2;
    }
    public int Guard()
    {
        return baseAttack * 3 / 2;
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
            skillInfo.detail = "勇者灯織のHPを、HP上限の<color=yellow>20%</color>回復する。";
        }
        else
        {
            skillInfo.detail = "勇者灯織のHPを、HP上限の<color=yellow>35%</color>回復する。";
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
        skillInfo.detail = "勇者灯織の攻撃力を<color=yellow>30%</color>アップする&被ダメージを<color=yellow>20%</color>カットする。(<color=yellow>2ターン</color>)";
        return skillInfo;
    }
    private SkillInfo UpdateSkill4()
    {
        SkillInfo skillInfo = new();
        skillInfo.name = "まのびーむ";
        skillInfo.useMP = 5;
        skillInfo.detail = "[このターンの行動終了後に発動]\n敵に<color=yellow>勇者灯織の攻撃力+1000</color>の確定ダメージを与える。";
        return skillInfo;
    }
}