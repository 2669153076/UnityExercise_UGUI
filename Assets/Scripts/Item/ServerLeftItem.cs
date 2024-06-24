using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    public Button selfBtn;
    public Text infoText;

    //服务器区间值
    private int beginIndex;
    private int endIndex;
    // Start is called before the first frame update
    void Start()
    {
        selfBtn.onClick.AddListener(() =>
        {
            //通知选服面板 改变右侧区间内容
            ServerChoosePanel panel = UIManager.Instance.GetPanel<ServerChoosePanel>();
            panel.UpdatePanel(beginIndex,endIndex);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitInfo(int  beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        infoText.text = beginIndex + " - " + endIndex + "区";
    }
}
