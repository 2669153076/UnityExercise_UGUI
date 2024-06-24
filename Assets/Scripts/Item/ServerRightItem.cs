using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    public Button selfBtn;

    public Text infoText;
    public Image isNewImage;
    public Image stateImage;

    public ServerInfo nowServerInfo;    //当前按钮代表的服务器

    // Start is called before the first frame update
    void Start()
    {
        selfBtn.onClick.AddListener(() =>
        {
            //记录所选择的服务器
            LoginManager.Instance.LoginData.frontServerId = nowServerInfo.id;



            UIManager.Instance.ShowPanel<ServerPanel>();
            UIManager.Instance.HidePanel<ServerChoosePanel>();
        });


    }

    public void InitInfo(ServerInfo info)
    {
        nowServerInfo = info;

        //更新按钮信息
        infoText.text = info.id + "区   " + info.name;
        isNewImage.gameObject.SetActive(info.isNew);

        stateImage.gameObject.SetActive(true);
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
}
