using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Inital : MonoBehaviour
{
    public AssetReference per;

    private void Awake()
    {
        Addressables.LoadSceneAsync(per);
    }
}
