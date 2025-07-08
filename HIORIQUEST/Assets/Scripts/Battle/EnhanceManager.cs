using UnityEngine;

//強化状態を一括管理
public class EnhanceManager
{
    public class EnhanceStatus
    {
        public int hp = 0;
        public int atk = 0;
        public int damageCut = 0;
        public int mp = 0;
    }
    public static EnhanceStatus enhanceStatus = new();

    public class EnhanceSkill
    {
        public bool skill1 = false;

    }
    public static EnhanceSkill enhanceSkill = new();

    public class EnhanceAction
    {
        public bool attack = false;

    }
    public static EnhanceAction enhanceAction = new();

    public class EnhanceOther
    {
        
    }
    public static EnhanceOther enhanceOther = new();

    public static void Enhance(int n)
    {
        switch (n)
        {
            case 0:
                Enhance0();
                break;
            case 1:

                break;

            default:
                Debug.Log("Enhance index error");
                break;
        }
    }
    private static void Enhance0()
    {
        enhanceStatus.hp += 2000;
    }

    public static void Initialize()
    {
        enhanceStatus = new();
        enhanceSkill = new();
        enhanceAction = new();
        enhanceOther = new();
    }
}