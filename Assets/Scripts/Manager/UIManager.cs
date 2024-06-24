using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance=>instance;


    //存储面板的容器
    public Dictionary<string, PanelBase> panelDic = new Dictionary<string, PanelBase>();

    //一开始获取Canvas对象
    private Transform canvasTransfrom;

    private UIManager()
    {
        canvasTransfrom = GameObject.Find("Canvas").transform;

        //过场景不删除Canvas对象
        //原因：通过动态创建和动态删除来显示隐藏面板，所以可以不删除Canvas对象
        GameObject.DontDestroyOnLoad(canvasTransfrom.gameObject);
    }

    //显示面板
    public T ShowPanel<T>() where T : PanelBase
    {
        //保证泛型T的类型 和面板名 一致
        string panelName = typeof(T).Name;
        //该面板是否已经显示 如果显示，则返回该面板 不用创建
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        //动态创建面板
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("Panel/" + panelName));
        panelObj.transform.SetParent(canvasTransfrom, false);

        //将得到的面板脚本存起来
        T panelData = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panelData);

        panelData.ShowSelf();

        return panelData;
    }
    /// <summary>
    /// 隐藏面板 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade">是否希望淡出面板</param>
    public void HidePanel<T>(bool isFade = true) where T:PanelBase
    {
        //根据泛型类型得到面板名
        string panelName=typeof(T).Name;
        //判断是否具有该面板
        if(panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideSelf(() =>
                {
                    //删除面板
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //删除面板后，从字典中移除
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                //删除面板后，从字典中移除
                panelDic.Remove(panelName);
            }
        }
    }

    //获得面板
    public T GetPanel<T>() where T : PanelBase
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }

        return null;
    }
}
