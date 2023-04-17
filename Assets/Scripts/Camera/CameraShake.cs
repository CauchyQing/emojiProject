using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private readonly List<ShakeRequest> _request = new();

    public void RequestShake(float amount, float time)
    {
        _request.Add(new ShakeRequest
        {
            shakeAmount = amount,
            shakeTime = time
        });

    }

    private class ShakeRequest
    {
        public float shakeAmount { get; set; }
        public float shakeTime { get; set; }
    }
}
