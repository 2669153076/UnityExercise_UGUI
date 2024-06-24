using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerChoosePanel : PanelBase
{
    //左右滚动视图
    public ScrollRect leftViewRect;
    public ScrollRect rightViewRect;

    //上一次登录的服务器相关信息
    public Text nameText;
    public Image stateImage;

    //当前选择的区间范围
    public Text rangeText;

    //存储右侧按钮
    private List<GameObject> itemList = new List<GameObject>();

    protected override void Init()
    {
        //动态创建左侧按钮

        //获取服务器列表数据
        List<ServerInfo> infoList = LoginManager.Instance.ServerData;

        //共有num个区间按钮 
        int num = infoList.Count / 5 + 1;
        for (int i = 0; i < num; i++)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>("Item/ServerChooseBtn"));
            item.transform.SetParent(leftViewRect.content, false);

            //初始化
            ServerLeftItem serverLeft = item.GetComponent<ServerLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = (i + 1) * 5;
            if (endIndex > infoList.Count)
                endIndex = infoList.Count;
            serverLeft.InitInfo(beginIndex, endIndex);
        }
    }

    public override void ShowSelf()
    {
        base.ShowSelf();
        //初始化上一次选择的服务器
        int id = LoginManager.Instance.LoginData.frontServerId;
        if (id <= 0)
        {
            nameText.text = "无";
            stateImage.gameObject.SetActive(false);
        }
        else
        {
            ServerInfo info = LoginManager.Instance.ServerData[id - 1];
            nameText.text = info.name;
            SpriteAtlas stateAtlas = Resources.Load<SpriteAtlas>("Image/ServerState");
            switch (info.state)
            {
                case E_ServerStateType.Busy:
                    stateImage.sprite = stateAtlas.GetSprite("ui_DL_fanhua_01");
                    break;
                case E_ServerStateType.Hot:
                    stateImage.sprite = stateAtlas.GetSprite("ui_DL_huobao_01");
                    break;
                case E_ServerStateType.Smooth:
                    stateImage.sprite = stateAtlas.GetSprite("ui_DL_liuchang_01");
                    break;
                case E_ServerStateType.Fixed:
                    stateImage.sprite = stateAtlas.GetSprite("ui_DL_weihu_01");
                    break;
                case E_ServerStateType.None:
                    stateImage.gameObject.SetActive(false);
                    break;
            }
        }

        UpdatePanel(1, 5 > LoginManager.Instance.ServerData.Count ? LoginManager.Instance.ServerData.Count : 5);

    }

    /// <summary>
    /// 更新当前选择区间右侧按钮
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void UpdatePanel(int beginIndex, int endIndex)
    {
        rangeText.text = beginIndex + " - " + endIndex + "区";

        //删除之前的单个按钮
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i].gameObject);
            
        }
        itemList.Clear();
        //创建新的按钮
        for (int i = beginIndex; i <= endIndex; i++)
        {
            //获取服务器信息
            ServerInfo nowInfo = LoginManager.Instance.ServerData[i-1];
            //动态创建预设体
            GameObject obj = Instantiate(Resources.Load<GameObject>("Item/ServerInfoBtn"));
            obj.transform.SetParent(rightViewRect.content,false);
            //更新右侧服务器按钮信息
            ServerRightItem serverRightItem = obj.GetComponent<ServerRightItem>();
            serverRightItem.InitInfo(nowInfo);
            //添加到列表中
            itemList.Add(obj);

        
        }
    }

}
