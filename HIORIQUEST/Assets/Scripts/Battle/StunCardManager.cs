using UnityEngine;
using UnityEngine.UI;

//敵の行動をスタンさせる時に表示されるカード
public class StunCardManager : MonoBehaviour
{
    private StunEnemyManager stunEnemyManager;
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject outline;
    [SerializeField] private Sprite unknownCard;
    [SerializeField] private Sprite attackCard;
    [SerializeField] private Sprite breakCard;
    [SerializeField] private Sprite guardCard;
    [SerializeField] private Sprite unactionCard;
    [HideInInspector] public bool isSelect = false;
    private bool stun = false;

    void Awake()
    {
        stunEnemyManager = GameObject.Find("StunEnemy").GetComponent<StunEnemyManager>();
        outline.SetActive(false);
    }

    public void SetCard(int card)
    {
        switch (card)
        {
            case 0:
                cardImage.sprite = unknownCard;
                break;
            case 1:
                cardImage.sprite = attackCard;
                break;
            case 2:
                cardImage.sprite = breakCard;
                break;
            case 3:
                cardImage.sprite = guardCard;
                break;
            case 4:
                cardImage.sprite = unactionCard;
                cardImage.color = new(100f / 255, 100f / 255, 100f / 255, 1);
                stun = true;
                break;
            default:
                cardImage.sprite = null;
                Debug.Log("Card index error");
                break;
        }
    }

    public void Touch()
    {
        if (!stun && stunEnemyManager.JudgeSelect(!isSelect))
        {
            isSelect = !isSelect;
            outline.SetActive(isSelect);
        }
    }

    public void Stun()
    {
        cardImage.sprite = unactionCard;
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}