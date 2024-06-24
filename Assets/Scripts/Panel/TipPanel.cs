using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : PanelBase
{
    public Text tipText;
    public Button enterBtn;

    
    protected override void Init()
    {
        enterBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>(false);
        });
    }

    /// <summary>
    /// 改变提示的内容
    /// </summary>
    /// <param name="info"></param>
    public void ChangeInfo(string info)
    {
        tipText.text = info;
    }
}
