using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//中断再開といったシステム面の管理
public class SystemManager : MonoBehaviour
{
    void Start()
    {
        //BGMやSEの大きさを取得
    }

    public void CloseConfig()
    {
        gameObject.SetActive(false);
    }
}