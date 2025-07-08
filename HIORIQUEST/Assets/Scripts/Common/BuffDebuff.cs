//ターン制のバフデバフを表すクラス
public abstract class BuffDebuff
{
    public int turn = 0;
    public string title = "";
    public string description = "";
    public string image = "";

    //そのバフ(デバフ)が付与された時に発動する効果
    public abstract void Apply();
    //そのバフ(デバフ)の有効ターンが終了した時に発動する効果
    public abstract void Remove();
}