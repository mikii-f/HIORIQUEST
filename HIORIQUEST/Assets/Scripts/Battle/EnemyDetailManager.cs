using UnityEngine;
using UnityEngine.UI;

//敵の詳細情報表示を管理
public class EnemyDetailManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private Text infoText;
    [SerializeField] private Text stateText;
    
    public void InfoTab()
    {
        infoText.color = Color.white;
        stateText.color = new(100f/255, 100f/255, 100f/255, 1);

        //敵の基本情報をContentに表示し、スクロール領域の幅を設定

    }
    public void StateTab()
    {
        stateText.color = Color.white;
        infoText.color = new(100f / 255, 100f / 255, 100f / 255, 1);

        //敵の状態をContentに表示し、スクロール領域の幅を設定

    }

    public void CloseEnemyDetail()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //BattleManager経由で敵の情報を取得して表示内容を更新

    }
}