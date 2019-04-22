using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour {

    public float speedMultiplier;

    private void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speedMultiplier);
    }
}
