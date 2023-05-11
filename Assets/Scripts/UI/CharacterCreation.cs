using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private GameObject[] characterGameObjects;
    private int selectedIndex = 0;
    private int length;//所有可供选择的角色个数 
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

    void UpdateCharacterShow()//更新所有角色的显示
    {
        characterGameObjects[selectedIndex].SetActive(true);
        for (int i = 0; i < length; i++)
        {
            if (i != selectedIndex)
            {
                characterGameObjects[i].SetActive(false);//把未选择的角色隐藏起来
            }
        }
    }
    public void OnNextButtonClick()//当我们点击了下一个按钮
    {
        selectedIndex++;
        selectedIndex %= length;
        UpdateCharacterShow();
    }
    public void OnPreButtonClick()//当我们点击了上一个按钮
    {
        selectedIndex--;
        if (selectedIndex == -1)
        {
            selectedIndex = length - 1;

        }
        UpdateCharacterShow();
    }

    //创建一个OK按钮
    public void OnOkButtonClick()
    {
        //加载下一个场景
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);//存储选择的角色
        SceneManager.LoadScene(1);                                                            //加载下一个场景
    }
}

