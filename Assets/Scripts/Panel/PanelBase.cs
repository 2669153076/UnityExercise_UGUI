using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PanelBase : MonoBehaviour
{

    //private static PanelBase instance;
    //public static PanelBase Instance=>instance;

    private UnityAction hideCallBack;  //淡出时隐藏自己的委托

    private CanvasGroup canvasGroup;    //控制淡入淡出组件
    private float alphaSpeed = 10;  //淡入淡出速度

    private bool isShow;


    protected virtual void Awake()
    {
        //instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null )
        {
            canvasGroup=gameObject.AddComponent<CanvasGroup>();
        }
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if(!isShow)   //淡出
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //管理器删除自己
                hideCallBack?.Invoke();
            }

        }
    }

    /// <summary>
    /// 子类进行初始化
    /// </summary>
    protected abstract void Init();

    public virtual void ShowSelf()
    {
        isShow = true;
        canvasGroup.alpha = 0;  //淡入 从0开始
    }

    public virtual void HideSelf(UnityAction callBack)
    {
        isShow=false;
        canvasGroup.alpha = 1;
        hideCallBack = callBack;
    }
}
