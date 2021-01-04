using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraShaderReplacementScript : MonoBehaviour
{
    public Shader ReplacementShader;

    void OnEnable()
    {
        if(ReplacementShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(ReplacementShader, "");
        }
        
    }

    void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
}
