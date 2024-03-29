﻿using UnityEngine;
using System.Collections;
 
//设置在编辑模式下也执行该脚本
[ExecuteInEditMode]
public class RaysScript : MonoBehaviour
{
    public Material CurMaterial;

    RenderTexture renderBuffer = null;
    RenderTexture tempBuffer = null;

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        //着色器实例不为空，就进行参数设置
        if (CurMaterial != null)
        {
            if(renderBuffer == null)
            {
                int renderWidth = sourceTexture.width;
                int renderHeight = sourceTexture.height;
                renderBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
                tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
            }
            
            //CurMaterial.SetTexture("_OriginTex", sourceTexture);
            
            //Graphics.Blit(sourceTexture, destTexture, CurMaterial, 0);
            Graphics.Blit(sourceTexture, renderBuffer, CurMaterial, 0);

            Graphics.Blit(renderBuffer, tempBuffer, CurMaterial, 1);
            Graphics.Blit(tempBuffer, destTexture, CurMaterial, 2);
        }
 
        //着色器实例为空，直接拷贝屏幕上的效果。此情况下是没有实现屏幕特效的
        else
        {
            //直接拷贝源纹理到目标渲染纹理
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void OnDisable()
    {
        if(renderBuffer != null)
        {
            RenderTexture.ReleaseTemporary(renderBuffer);
            RenderTexture.ReleaseTemporary(tempBuffer);
        }
    }
 
}
