using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    // Start is called before the first frame update
    public void Scene1()
    {
        Debug.Log("�ð�ťΪ��ʼ��Ϸ��");
        SceneManager.LoadScene(1);//�������볡����Ż����ǳ�����
                                        // SceneManager.LoadScene("1");
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
