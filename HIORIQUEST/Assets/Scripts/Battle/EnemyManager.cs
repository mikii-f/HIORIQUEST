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
    public int HP { set { hp = Mathf.Clamp(value, 0, (int)hpSlider.maxValue); UpdateHpSlider();  } get { return hp; } }

    //敵生成時にBattleManagerから呼び出す
    public void StatusSet(int mHp, int atk, BattleManager bManager)
    {
        hpSlider.maxValue = mHp;
        HP = mHp;
        attack = atk;
        attackText.text = attack.ToString();
        battleManager = bManager;
    }

    private void UpdateHpSlider()
    {
        hpSlider.value = hp;
        hpText.text = hp.ToString() + "/" + hpSlider.maxValue.ToString();
    }

    //バフやデバフの種類に応じた乗算加算の区別を敵味方で共有する場合、係数ごと引数に入れる
    public void Damage(int playerAttack)
    {
        HP -= CalculateDamage(playerAttack);
        if (hp == 0)
        {
            //敵撃破時の処理
            StartCoroutine(battleManager.Win());
        }
    }

    //受けるダメージの計算(行動前にダメージ予測を出すため)
    public int CalculateDamage(int playerAttack)
    {

        return playerAttack;
    }

    //勇者に与えるダメージの計算
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