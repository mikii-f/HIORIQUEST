using UnityEngine;
using UnityEngine.UI;

//詳細画面でバフデバフの内容を表示するボックス
public class BuffDebuffBoxManager : MonoBehaviour
{
    [SerializeField] private Image box;
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text turn;
    [SerializeField] private Text description;
    
    public void Set(int id, BuffDebuff buffDebuff)
    {
        if (id % 4 == 1 || id % 4 == 2)
        {
            box.color = new(235f / 255, 235f / 255, 235f / 255, 1);
        }
        title.text = buffDebuff.title;
        turn.text = "残り" + buffDebuff.turn.ToString() + "ターン";
        description.text = buffDebuff.description;
    }
}