using UnityEngine;

//勇者灯織の詳細情報表示を管理
public class HioriDetailManager : DetailPanelManager
{
    [SerializeField] private PlayerManager playerManager;

    protected override void ChangeToInfo()
    {
        DestroyContets(scrollViewContent);
        scrollViewContent.sizeDelta = new(scrollViewContent.sizeDelta.x, 0);
        if (scrollViewContent.sizeDelta.y > 600)
        {
            barSlideArea.SetActive(true);
        }
        else
        {
            barSlideArea.SetActive(false);
        }

    }
    protected override void ChangeToState()
    {
        DestroyContets(scrollViewContent);
        scrollViewContent.sizeDelta = new(scrollViewContent.sizeDelta.x, 150 * ((PlayerStateManager.buffDebuffs.Count + 1) / 2));
        if (scrollViewContent.sizeDelta.y > 600)
        {
            barSlideArea.SetActive(true);
        }
        else
        {
            barSlideArea.SetActive(false);
        }
        for (int i=0; i< PlayerStateManager.buffDebuffs.Count; i++)
        {
            GameObject box = Instantiate(Resources.Load<GameObject>("Prefabs/BuffDebuffBox"), scrollViewContent);
            if (i % 2 == 0)
            {
                //上端のボックスのy座標は、Content内に1行増えるほど75マイナス、以降のボックスは2個ごとに150プラス
                box.GetComponent<RectTransform>().anchoredPosition = new(-325, -75 * ((PlayerStateManager.buffDebuffs.Count - 1) / 2) + 150 * (i / 2));
                box.GetComponent<BuffDebuffBoxManager>().Set(i, PlayerStateManager.buffDebuffs[i]);
            }
            else
            {
                box.GetComponent<RectTransform>().anchoredPosition = new(325, -75 * ((PlayerStateManager.buffDebuffs.Count - 1) / 2) + 150 * (i / 2));
                box.GetComponent<BuffDebuffBoxManager>().Set(i, PlayerStateManager.buffDebuffs[i]);
            }
        }
    }

    void DestroyContets(RectTransform parent)
    {
        foreach (RectTransform child in parent)
        {
            DestroyContets(child);
            Destroy(child.gameObject);
        }
    }
}