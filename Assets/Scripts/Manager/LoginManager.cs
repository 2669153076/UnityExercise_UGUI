using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager
{
    private static LoginManager instance = new LoginManager();
    public static LoginManager Instance => instance;

    private LoginData loginData;
    private RegisterData registerData;
    private List<ServerInfo> serverData;
    public LoginData LoginData => loginData;
    public RegisterData RegisterData => registerData;
    public List<ServerInfo> ServerData => serverData;

    private LoginManager() {

        //通过Json管理器读取对应数据
        loginData= JsonFileMgr.Instance.LoadData<LoginData>("LoginData");
        registerData = JsonFileMgr.Instance.LoadData<RegisterData>("RegisterData");
        serverData = JsonFileMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region 登录数据相关
    public void SaveLoginData()
    {
        JsonFileMgr.Instance.SaveData(loginData,"LoginData",E_JsonType.LitJson);
    }

    public void ClearLoginData()
    {
        loginData.frontServerId = 0;
        loginData.isAutoLogin = false;
        loginData.isRememberPwd = false;
    }
    #endregion

    #region 注册数据相关
    public void SaveRegisterData()
    {
        JsonFileMgr.Instance.SaveData(registerData, "RegisterData", E_JsonType.LitJson);
    }
    //是否注册成功
    public bool isRegisterVictory(string username,string password)
    {
        if(registerData.registerDic.ContainsKey(username))
        {
            return false;
        }

        registerData.registerDic.Add(username, password);
        SaveRegisterData();
        return true;
    }

    //验证用户名和密码
    public bool CheckInfo(string username,string password)
    {
        if (registerData.registerDic.ContainsKey(username))
        {
            if (registerData.registerDic[username]==password)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

}
