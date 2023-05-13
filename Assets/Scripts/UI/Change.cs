using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndEnd()
    {
        SceneManager.LoadScene("RE");
    }

    public void Restart()
    {
        SceneManager.LoadScene("PermanentScene");
    }


    public void OnExitGame()//定义一个退出游戏的方法
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中
#else
        Application.Quit();//否则在打包文件中
#endif
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
