using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : PanelBase
{
    public Button registerBtn;
    public Button loginBtn;

    public InputField usernameInputField;
    public InputField passwordInputField;

    public Toggle isRememberPwd;
    public Toggle isAutoLogin;

    protected override void Init()
    {
        registerBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<LoginPanel>();
            UIManager.Instance.ShowPanel<RegisterPanel>();
        });

        loginBtn.onClick.AddListener(() =>
        {
            if (usernameInputField.text.Length < 6 || passwordInputField.text.Length < 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("账号密码长度不小于6位");
                return;
            }

            if (LoginManager.Instance.CheckInfo(usernameInputField.text, passwordInputField.text))
            {
                //登录成功

                //记录数据
                LoginManager.Instance.LoginData.userName = usernameInputField.text;
                LoginManager.Instance.LoginData.passWord = passwordInputField.text;
                LoginManager.Instance.LoginData.isRememberPwd = isRememberPwd.isOn;
                LoginManager.Instance.LoginData.isAutoLogin = isAutoLogin.isOn;
                LoginManager.Instance.SaveLoginData();

                //根据服务器信息决定显示哪个面板
                if(LoginManager.Instance.LoginData.frontServerId <=0)
                {
                    UIManager.Instance.ShowPanel<ServerChoosePanel>();
                }
                else
                {
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                UIManager.Instance.HidePanel<LoginPanel>();
            }
            else
            {
                //登录失败
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码错误");

            }

            
        });

        isRememberPwd.onValueChanged.AddListener((isOn) =>
        {
            if(!isOn)
            {
                isAutoLogin.isOn = false;
            }
        });
        isAutoLogin.onValueChanged.AddListener((isOn) =>
        {
            if(isOn)
            {
                isRememberPwd.isOn = true;
            }
        });

        //usernameInputField.text = "";
        //passwordInputField.text = "";
    }

    public override void ShowSelf()
    {
        base.ShowSelf();
        //显示自己时 根据数据 更细面板上的内容

        LoginData loginData=LoginManager.Instance.LoginData;
        if(loginData!=null)
        {
            isRememberPwd.isOn = loginData.isRememberPwd;
            isAutoLogin.isOn = loginData.isAutoLogin;

            usernameInputField.text = loginData.userName;
            if (isRememberPwd.isOn)
            {
                passwordInputField.text = loginData.passWord;
            }
            if (isAutoLogin.isOn)
            {
                if(LoginManager.Instance.CheckInfo(usernameInputField.text,passwordInputField.text))
                {
                    //根据服务器信息决定显示哪个面板
                    if (LoginManager.Instance.LoginData.frontServerId <= 0)
                    {
                        UIManager.Instance.ShowPanel<ServerChoosePanel>();
                    }
                    else
                    {
                        UIManager.Instance.ShowPanel<ServerPanel>();
                    }

                    UIManager.Instance.HidePanel<LoginPanel>(false);
                }
                else
                {
                    TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                    panel.tipText.text = "账号或密码错误";
                }
            }

        }
    }

    public void SetInfo(string username,string password)
    {
        usernameInputField.text = username;
        passwordInputField.text = password;
    }
}
