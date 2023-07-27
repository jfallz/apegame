using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


namespace Oxeren.PostProcessing
{

    [Serializable, PostProcess(typeof(PixelateRenderer), PostProcessEvent.AfterStack, "Custom/Pixelate")]
    public sealed class Pixelate : PostProcessEffectSettings
    {

        [Range(1, 16)]
        public IntParameter DownscaleIterations = new IntParameter { value = 4 };
        public IntParameter ColorReduction = new IntParameter { value = 16 };

        public override bool IsEnabledAndSupported(PostProcessRenderContext context)
        {
            return enabled;
        }
    }

    public sealed class PixelateRenderer : PostProcessEffectRenderer<Pixelate>
    {

        PropertySheet sheet;

        public override void Render(PostProcessRenderContext context)
        {

            if (sheet == null)
                sheet = context.propertySheets.Get(Shader.Find("Custom/Pixelate"));

            sheet.properties.SetFloat("_ColorReductionIterations", settings.ColorReduction.value);
            int width = context.width / settings.DownscaleIterations;
            int height = context.height / settings.DownscaleIterations;
            var downsampled = RenderTexture.GetTemporary(width, height, 0, context.sourceFormat);
            downsampled.filterMode = FilterMode.Point;
            context.command.BlitFullscreenTriangle(context.source, downsampled);
            context.command.BlitFullscreenTriangle(downsampled, context.destination, sheet, 0);
            RenderTexture.ReleaseTemporary(downsampled);
        }
    }

}