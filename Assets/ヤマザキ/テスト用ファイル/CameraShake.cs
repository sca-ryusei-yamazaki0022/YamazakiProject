using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[PostProcess(typeof(CameraShakeRenderer), PostProcessEvent.AfterStack, "Custom/CameraShake")]
public class CameraShakeEffect : PostProcessEffectSettings
{
    [Range(0f, 1f)]
    public FloatParameter shakeAmount = new FloatParameter { value = 0.1f };
    public FloatParameter shakeSpeed = new FloatParameter { value = 1.0f };
}

public class CameraShakeRenderer : PostProcessEffectRenderer<CameraShakeEffect>
{
    private float elapsedTime = 0f;

    public override void Render(PostProcessRenderContext context)
    {
        if (Application.isPlaying)
        {
            elapsedTime += Time.deltaTime;
            float shakeOffset = Mathf.Sin(elapsedTime * settings.shakeSpeed) * settings.shakeAmount;

            PropertySheet sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/CameraShake"));
            sheet.properties.SetFloat("_ShakeOffset", shakeOffset);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
        else
        {
            context.command.BlitFullscreenTriangle(context.source, context.destination);
        }
    }
}