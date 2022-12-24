using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class PostEffectController : MonoBehaviour
{
    public Shader postShader;
    Material postEffectMaterial;

    public Color screenTint;

    private void Awake()
    {
        //postEffectMaterial = new Material(postShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (postEffectMaterial == null)
        {
            postEffectMaterial = new Material(postShader);
        }
        int width = source.width;
        int height = source.height;

        RenderTexture startRenderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);

        postEffectMaterial.SetColor("_ScreenTint", screenTint);

        Graphics.Blit(source, startRenderTexture, postEffectMaterial, 0);
        Graphics.Blit(startRenderTexture, destination);
        RenderTexture.ReleaseTemporary(startRenderTexture);
    }
}