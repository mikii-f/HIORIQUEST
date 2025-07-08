using UnityEngine;
using UnityEngine.UI;

//行動選択時のカード一対を管理
public class CardManager : MonoBehaviour
{
    [SerializeField] private Image enemyCard;
    [SerializeField] private Image playerCard;
    [SerializeField] private Sprite unknownCard;
    [SerializeField] private Sprite attackCard;
    [SerializeField] private Sprite breakCard;
    [SerializeField] private Sprite guardCard;
    [SerializeField] private Sprite unactionCard;
    [HideInInspector] public int currentCard = 1;

    public void CardSet(int enemyAction)
    {
        switch (enemyAction)
        {
            case 0:
                enemyCard.sprite = unknownCard;
                break;
            case 1:
                enemyCard.sprite = attackCard;
                break;
            case 2:
                enemyCard.sprite = breakCard;
                break;
            case 3:
                enemyCard.sprite = guardCard;
                break;
            case 4:
                enemyCard.sprite = unactionCard;
                break;
            default:
                enemyCard.sprite = null;
                Debug.Log("Card index error");
                break;
        }
    }

    public void CardChange()
    {
        switch (currentCard)
        {
            case 1:
                playerCard.sprite = breakCard;
                currentCard++;
                break;
            case 2:
                playerCard.sprite = guardCard;
                currentCard++;
                break;
            case 3:
                playerCard.sprite = attackCard;
                currentCard = 1;
                break;
            default:
                playerCard.sprite = null;
                Debug.Log("Card index error");
                break;
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}