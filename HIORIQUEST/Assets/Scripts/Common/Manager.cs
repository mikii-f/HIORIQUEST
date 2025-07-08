using UnityEngine;

//全体の情報管理(特にセーブデータに関連するもの)
public class Manager : MonoBehaviour
{
    public static int mode = 0; //0がノーマル、1がハード
    public static int battleCount = 1;  //何バトル目か
    public static int currentHp = 5000;
    public static int currentMp = 1;

    public static void Initialize()
    {
        battleCount = 1;
        currentHp = 5000;
        currentMp = 1;
    }
}