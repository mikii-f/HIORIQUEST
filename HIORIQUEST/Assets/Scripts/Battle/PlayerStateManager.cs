using System.Collections.Generic;
using UnityEngine;

//プレイヤーの状態変化を管理(戦闘を跨ぐことも想定)
public class PlayerStateManager : MonoBehaviour
{
    public static int attackFactor = 100;     //%単位で攻撃力の係数を保持
    public static int damageCutFactor = 0;

    public static List<BuffDebuff> buffDebuffs = new();

    public class AttackBD : BuffDebuff
    {
        public int value = 0;
        public override void Apply()
        {
            image = "";
            attackFactor += value;
            if (value >= 0)
            {
                title = "攻撃力UP";
                description = "攻撃力が" + value.ToString() + "%UPする効果";
            }
            else
            {
                title = "攻撃力DOWN";
                description = "攻撃力が" + value.ToString() + "%DOWNする効果";
            }
        }
        public override void Remove()
        {
            attackFactor -= value;
        }
    }
    public class DamageCutBD : BuffDebuff
    {
        public int value = 0;
        public override void Apply()
        {
            image = "";
            damageCutFactor += value;
            if (value >= 0)
            {
                title = "被ダメージCUT";
                description = "受けるダメージを" + value.ToString() + "%CUTする効果";
            }
            else
            {
                description = "受けるダメージを" + value.ToString() + "%UPする効果";
            }
        }
        public override void Remove()
        {
            damageCutFactor -= value;
        }
    }

    public static void PassTurn()
    {
        int bdNumber = buffDebuffs.Count;
        for (int i=0; i<bdNumber; i++)
        {
            buffDebuffs[i].turn--;
            if (buffDebuffs[i].turn == 0)
            {
                buffDebuffs[i].Remove();
                buffDebuffs.RemoveAt(i);
                i--;
                bdNumber--;
            }
        }
    }
}