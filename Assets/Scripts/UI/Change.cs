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


    public void OnExitGame()//����һ���˳���Ϸ�ķ���
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�������unity��������
#else
        Application.Quit();//�����ڴ���ļ���
#endif
    }


    public void Scene2()//����������
    {
        Debug.Log("����������");
        SceneManager.LoadScene(0);
    }
    public void Developer()
    {
        Debug.Log("������");
        SceneManager.LoadScene("Develop");
    }
    public void Selectplayer()
    {
        Debug.Log("ѡ�˽���");
        SceneManager.LoadScene(1);
    }

}
