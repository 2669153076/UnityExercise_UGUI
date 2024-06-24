using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : PanelBase
{
    public Button enterBtn;
    public Button backBtn;
    public Button changeServerBtn;

    public Text infoText;

    protected override void Init()
    {
        enterBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.HidePanel<LoginBkPanel>();
            //存储当前选择的服务器id
            LoginManager.Instance.SaveLoginData();

            SceneManager.LoadScene("GameScene");
        });

        backBtn.onClick.AddListener(() =>
        {
            if(LoginManager.Instance.LoginData.isAutoLogin)
            {
                LoginManager.Instance.LoginData.isAutoLogin = false;
            }
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.ShowPanel<LoginPanel>();
        });

        changeServerBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<ServerChoosePanel>();

            UIManager.Instance.HidePanel<ServerPanel>();
        });

        
    }

    public override void ShowSelf()
    {
        base.ShowSelf();

        //显示自己的时候 更新 当前所选服务器
        //通过记录的上一次登录的服务器ID 更新
        int id = LoginManager.Instance.LoginData.frontServerId;
        if (id <= 0)
        {
            infoText.text = "无";
        }
        else
        {
            ServerInfo serverInfo = LoginManager.Instance.ServerData[id - 1];
            infoText.text = serverInfo.id + "区   " + serverInfo.name;
        }
    }
}
