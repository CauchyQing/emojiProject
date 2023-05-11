using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO> loadRequestEvent;

    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="locationToLoad">Ҫ���صĳ���</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad)
    {
        loadRequestEvent?.Invoke(locationToLoad);
    }
}
