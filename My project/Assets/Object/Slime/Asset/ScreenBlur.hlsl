#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

TEXTURE2D(_CameraOpaqueTexture);
SAMPLER(sampler_CameraOpaqueTexture);

void ScreenBlur_float(float4 ScreenPos, int Blur, float Scale, out float3 Out)
{
    // Perspective divide for screen UV
    float2 pos = ScreenPos.xy / ScreenPos.w;

    // Texel size in screen space
    float2 texel = Scale * (1.0 / _ScreenParams.xy);

    // Ensure blur >= 1
    int blur_size = (Blur > 0) ? Blur : 1;

    float3 col = 0.0;

    // Convolution loop
    for (int i = -blur_size; i <= blur_size; i++)
    {
        for (int j = -blur_size; j <= blur_size; j++)
        {
            float2 offset = float2(i, j) * texel;

            float4 sample = SAMPLE_TEXTURE2D(
                _CameraOpaqueTexture,
                sampler_CameraOpaqueTexture,
                pos + offset
            );

            col += sample.rgb;
        }
    }

    // Normalize
    float count = (2 * blur_size + 1) * (2 * blur_size + 1);
    Out = col / count;
}
