using TMPro;
using UnityEngine;
using UnityEngine.UI;

//敵管理用の親クラス
public class EnemyManager : MonoBehaviour
{
    private int hp = 2000;
    private int attack = 500;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Text attackText;
    private BattleManager battleManager;

    //敵生成時にBattleManagerから呼び出す
    public void StatusSet(int mHp, int atk, BattleManager bManager)
    {
        hpSlider.maxValue = mHp;
        hp = mHp;
        UpdateHp();
        attack = atk;
        attackText.text = attack.ToString();
        battleManager = bManager;
    }

    private void UpdateHp()
    {
        hpSlider.value = hp;
        hpText.text = hp.ToString() + "/" + hpSlider.maxValue.ToString();
    }
    public void Damage(int damage)
    {
        hp -= Mathf.Min(hp, damage);
        UpdateHp();
        if (hp == 0)
        {
            //敵撃破時の処理
            StartCoroutine(battleManager.Win());
        }
    }

    //ダメージ計算系(本来は色々係数などがかかる)
    public int Attack()
    {
        return attack;
    }
    public int Break()
    {
        return attack * 3 / 2;
    }
    public int Guard()
    {
        return attack * 3 / 2;
    }
}