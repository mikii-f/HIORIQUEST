using UnityEngine;
using UnityEngine.UI;

//敵および味方の詳細画面
public abstract class DetailPanelManager : MonoBehaviour
{
    [SerializeField] private Image infoTab;
    [SerializeField] private Text infoText;
    [SerializeField] private Image stateTab;
    [SerializeField] private Text stateText;
    [SerializeField] protected GameObject barSlideArea;
    [SerializeField] protected RectTransform scrollViewContent;

    public void InfoTab()
    {
        infoTab.color = new(225f / 255, 60f / 255, 130f / 255, 1);
        infoText.color = Color.white;
        stateTab.color = Color.white;
        stateText.color = new(60f / 255, 60f / 255, 60f / 255, 1);
        //基本情報をContentに表示し、スクロール領域の幅を設定
        ChangeToInfo();
    }
    protected abstract void ChangeToInfo();
    public void StateTab()
    {
        stateTab.color = new(225f / 255, 60f / 255, 130f / 255, 1);
        stateText.color = Color.white;
        infoTab.color = Color.white;
        infoText.color = new(60f / 255, 60f / 255, 60f / 255, 1);
        //状態をContentに表示し、スクロール領域の幅を設定
        ChangeToState();
    }
    protected abstract void ChangeToState();
    public void CloseDetailPanel()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (infoText.color == Color.white)
        {
            ChangeToInfo();
        }
        else
        {
            ChangeToState();
        }
    }
}