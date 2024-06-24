using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : PanelBase
{
    public Button registerBtn;
    public Button cancelBtn;

    public InputField usernameInputField;
    public InputField passwordInputField;


    protected override void Init()
    {
        registerBtn.onClick.AddListener(() =>
        {
            if (usernameInputField.text.Length < 6 || passwordInputField.text.Length < 6)
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("账号密码长度不小于6位");
                return;
            }

            if(LoginManager.Instance.isRegisterVictory(usernameInputField.text, passwordInputField.text))
            {
                //清理上一个账号的信息
                LoginManager.Instance.ClearLoginData();
                //初始化登录面板
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                loginPanel.SetInfo(usernameInputField.text, passwordInputField.text);

                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("用户名已存在");

                usernameInputField.text = "";
                passwordInputField.text = "";
            }
        });

        cancelBtn.onClick.AddListener(() =>
        {
            usernameInputField.text = "";
            passwordInputField.text = "";

            UIManager.Instance.HidePanel<RegisterPanel>();
            UIManager.Instance.ShowPanel<LoginPanel>();

        });

        usernameInputField.text = "";
        passwordInputField.text = "";
    }

}
