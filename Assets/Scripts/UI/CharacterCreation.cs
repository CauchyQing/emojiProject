using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private GameObject[] characterGameObjects;
    private int selectedIndex = 0;
    private int length;//���пɹ�ѡ��Ľ�ɫ���� 
    void Start()
    {
        length = characterPrefabs.Length;
        characterGameObjects = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            characterGameObjects[i] = GameObject.Instantiate(characterPrefabs[i], transform.position, transform.rotation) as GameObject;
        }
        UpdateCharacterShow();
    }

    void UpdateCharacterShow()//�������н�ɫ����ʾ
    {
        characterGameObjects[selectedIndex].SetActive(true);
        for (int i = 0; i < length; i++)
        {
            if (i != selectedIndex)
            {
                characterGameObjects[i].SetActive(false);//��δѡ��Ľ�ɫ��������
            }
        }
    }
    public void OnNextButtonClick()//�����ǵ������һ����ť
    {
        selectedIndex++;
        selectedIndex %= length;
        UpdateCharacterShow();
    }
    public void OnPreButtonClick()//�����ǵ������һ����ť
    {
        selectedIndex--;
        if (selectedIndex == -1)
        {
            selectedIndex = length - 1;

        }
        UpdateCharacterShow();
    }

    //����һ��OK��ť
    public void OnOkButtonClick()
    {
        //������һ������
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);//�洢ѡ��Ľ�ɫ
        SceneManager.LoadScene(1);                                                            //������һ������
    }
}

