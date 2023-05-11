using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    // Start is called before the first frame update
    public void Scene1()
    {
        Debug.Log("该按钮为开始游戏！");
        SceneManager.LoadScene(1);//可以输入场景序号或者是场景名
                                        // SceneManager.LoadScene("1");
    }
    public void Scene2()//返回主界面
    {
        Debug.Log("返回主界面");
        SceneManager.LoadScene(0);
    }
    public void Developer()
    {
        Debug.Log("制作者");
        SceneManager.LoadScene("Develop");
    }
    public void Selectplayer()
    {
        Debug.Log("选人界面");
        SceneManager.LoadScene(1);
    }
   
}
