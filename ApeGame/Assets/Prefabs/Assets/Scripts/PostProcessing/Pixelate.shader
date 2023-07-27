Shader "Custom/Pixelate"
{
	HLSLINCLUDE

	#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	float _ColorReductionIterations;

	ENDHLSL
	
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass //0 Applying Box filtering
		{
			HLSLPROGRAM

			#pragma vertex VertDefault
			#pragma fragment Frag

			float4 Frag(VaryingsDefault i) : SV_Target
			{
				return round( SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord) * _ColorReductionIterations) / _ColorReductionIterations;
			}
			ENDHLSL
		}
	}

}