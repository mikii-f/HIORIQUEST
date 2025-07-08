using UnityEngine;

//占いゲージに持たせるスクリプト
public class FortuneGageManager : MonoBehaviour
{
    [SerializeField] private float perfectLow = 72.5f;
    [SerializeField] private float perfectHigh = 87.5f;
    [SerializeField] private float goodLow = 30;
    [SerializeField] private float goodHigh = 130;
    [SerializeField] private float normalLow = -120;
    [SerializeField] private float normalHigh = 80;
    [SerializeField] private RectTransform barRect;
    private bool isMove = false;
    private BattleManager battleManager;

    void Awake()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    void Update()
    {
        if (isMove)
        {
            barRect.anchoredPosition += new Vector2(0, Time.deltaTime * 1.5f * 600);
            if (barRect.anchoredPosition.y > 300)
            {
                barRect.anchoredPosition = new(0, -300);
            }
            if (Input.GetMouseButtonDown(0))
            {
                StopMove();
            }
        }
    }

    public void StartMove()
    {
        isMove = true;
    }
    private void StopMove()
    {
        isMove = false;
        float y = barRect.anchoredPosition.y;
        int result;
        if (perfectLow <= y && y <= perfectHigh)
        {
            result = 0;
        }
        else if (goodLow <= y && y <= goodHigh)
        {
            result = 1;
        }
        else if (normalLow <= y && y <= normalHigh)
        {
            result = 2;
        }
        else
        {
            result = 3;
        }
        battleManager.fortuneTellingResult = result;
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}