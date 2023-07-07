// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Plume Volumetric Clouds"
{
	Properties
	{
		_CloudTexture("Cloud Texture", 2D) = "white" {}
		[HDR]_NearColor("Near Color", Color) = (1,1,1,1)
		[HDR]_FarColor("Far Color", Color) = (1,1,1,1)
		_Smoothness("Smoothness", Range( 0 , 5)) = 0
		_Offset("Offset", Float) = 0
		_CloudBendRadius("Cloud Bend Radius", Float) = 0
		[PerRendererData]_CenterPoint("CenterPoint", Vector) = (0,0,0,0)
		_MaxDistance("Max Distance", Float) = 5000
		_BalanceOffset("Balance Offset", Float) = 0
		_BalanceWidth("Balance Width", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _CloudBendRadius;
		uniform float PLUME_CloudBendMultiplier;
		uniform float3 _CenterPoint;
		uniform float _BalanceOffset;
		uniform float _BalanceWidth;
		uniform float4 PLUME_CloudShadowColor;
		uniform float4 PLUME_MainCloudColor;
		uniform float3 CZY_SunDirection;
		uniform float _Offset;
		uniform float _Smoothness;
		uniform sampler2D _CloudTexture;
		uniform float4 _CloudTexture_ST;
		uniform float4 _NearColor;
		uniform float4 _FarColor;
		uniform float _MaxDistance;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 break51 = ( _WorldSpaceCameraPos - ase_worldPos );
			float2 appendResult50 = (float2(break51.x , break51.z));
			float temp_output_44_0 = length( appendResult50 );
			float temp_output_60_0 = max( ( temp_output_44_0 - _CloudBendRadius ) , 0.0 );
			float3 BentPosition110 = ( ( temp_output_60_0 * temp_output_60_0 * PLUME_CloudBendMultiplier * 0.001 ) * float3(0,-1,0) );
			v.vertex.xyz += BentPosition110;
			v.vertex.w = 1;
			float3 CenterPoint116 = _CenterPoint;
			float3 break86 = ( ase_worldPos - ( CenterPoint116 + ( _BalanceOffset * float3(0,-1,0) ) + BentPosition110 ) );
			float3 appendResult88 = (float3(break86.x , ( break86.y * _BalanceWidth ) , break86.z));
			float3 normalizeResult80 = normalize( appendResult88 );
			float3 Normal77 = normalizeResult80;
			v.normal = Normal77;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 CenterPoint116 = _CenterPoint;
			float3 break51 = ( _WorldSpaceCameraPos - ase_worldPos );
			float2 appendResult50 = (float2(break51.x , break51.z));
			float temp_output_44_0 = length( appendResult50 );
			float temp_output_60_0 = max( ( temp_output_44_0 - _CloudBendRadius ) , 0.0 );
			float3 BentPosition110 = ( ( temp_output_60_0 * temp_output_60_0 * PLUME_CloudBendMultiplier * 0.001 ) * float3(0,-1,0) );
			float3 break86 = ( ase_worldPos - ( CenterPoint116 + ( _BalanceOffset * float3(0,-1,0) ) + BentPosition110 ) );
			float3 appendResult88 = (float3(break86.x , ( break86.y * _BalanceWidth ) , break86.z));
			float3 normalizeResult80 = normalize( appendResult88 );
			float3 Normal77 = normalizeResult80;
			float dotResult92 = dot( Normal77 , CZY_SunDirection );
			float dotResult153 = dot( Normal77 , float3(0,1,0) );
			float clampResult97 = clamp( ( ( dotResult92 - _Offset ) * ( 0.5 / _Smoothness ) * saturate( ( dotResult153 + 0.9 ) ) ) , 0.0 , 1.0 );
			float CloudColor31 = clampResult97;
			float4 lerpResult30 = lerp( PLUME_CloudShadowColor , PLUME_MainCloudColor , CloudColor31);
			float2 uv_CloudTexture = i.uv_texcoord * _CloudTexture_ST.xy + _CloudTexture_ST.zw;
			float4 tex2DNode3 = tex2D( _CloudTexture, uv_CloudTexture );
			clip( tex2DNode3.a - 0.9);
			float4 lerpResult115 = lerp( _NearColor , _FarColor , saturate( ( length( ( ase_worldPos - CenterPoint116 ) ) / _MaxDistance ) ));
			o.Emission = ( ( lerpResult30 * tex2DNode3 ) * lerpResult115 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
0;1080;2194.286;607.5715;2004.407;286.6047;1;True;False
Node;AmplifyShaderEditor.WorldSpaceCameraPos;138;-1914.226,-848.8325;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;137;-1920,-704;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;49;-1664,-800;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;51;-1536,-800;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;50;-1408,-800;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LengthOpNode;44;-1280,-800;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-1312,-704;Inherit;False;Property;_CloudBendRadius;Cloud Bend Radius;5;0;Create;True;0;0;0;False;0;False;0;700;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;58;-1072,-800;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1088,-592;Inherit;False;Global;PLUME_CloudBendMultiplier;PLUME_CloudBendMultiplier;4;0;Create;True;0;0;0;False;0;False;0;0.049;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-992,-512;Inherit;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;0;False;0;False;0.001;0.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;60;-928,-800;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;42;-720,-544;Inherit;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;0;False;0;False;0,-1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-736,-800;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;78;-2496,-1888;Inherit;False;Property;_CenterPoint;CenterPoint;6;1;[PerRendererData];Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-592,-800;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-1984,-1440;Inherit;False;Property;_BalanceOffset;Balance Offset;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;83;-1984,-1360;Inherit;False;Constant;_Down;Down;6;0;Create;True;0;0;0;False;0;False;0,-1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;110;-464,-800;Inherit;False;BentPosition;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;116;-2336,-1888;Inherit;False;CenterPoint;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-1792,-1440;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;117;-1984,-1520;Inherit;False;116;CenterPoint;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;112;-1792,-1344;Inherit;False;110;BentPosition;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldPosInputsNode;93;-1632,-1664;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;81;-1616,-1456;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;79;-1392,-1600;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;86;-1232,-1600;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;87;-1280,-1472;Inherit;False;Property;_BalanceWidth;Balance Width;9;0;Create;True;0;0;0;False;0;False;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;-1104,-1488;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;88;-960,-1600;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;80;-816,-1600;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;77;-657,-1600;Inherit;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;155;-864,-1024;Inherit;False;Constant;_Up;Up;6;0;Create;True;0;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;154;-864,-1104;Inherit;False;77;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;69;-800,-1392;Inherit;False;Global;CZY_SunDirection;CZY_SunDirection;12;0;Create;True;0;0;0;False;0;False;0,0,0;-0.7515306,0.5393735,0.3798397;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;153;-672,-1088;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;91;-800,-1472;Inherit;False;77;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-800,-1248;Inherit;False;Property;_Offset;Offset;4;0;Create;True;0;0;0;False;0;False;0;-0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;92;-560,-1408;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-592,-1184;Inherit;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;0;False;0;False;0;1.79;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;157;-448,-1088;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;13;-320,-1184;Inherit;False;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;15;-400,-1408;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;156;-320,-1088;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-144,-1408;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;97;16,-1408;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;149;-1632,384;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;120;-1640,536;Inherit;False;116;CenterPoint;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;150;-1375,480;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;31;176,-1408;Inherit;False;CloudColor;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-1712,-304;Inherit;False;Global;PLUME_CloudShadowColor;PLUME_CloudShadowColor;0;1;[HDR];Create;True;0;0;0;False;0;False;0.678991,0.745283,0.745283,1;0.8798228,0.9875755,1.029019,1.169;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-1712,-128;Inherit;False;Global;PLUME_MainCloudColor;PLUME_MainCloudColor;0;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1.211732,1.360134,1.417212,1.61;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LengthOpNode;152;-1232,480;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;126;-1312,672;Inherit;False;Property;_MaxDistance;Max Distance;7;0;Create;True;0;0;0;False;0;False;5000;1103.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;32;-1712,48;Inherit;False;31;CloudColor;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;30;-1408,-192;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;3;-1408,-80;Inherit;True;Property;_CloudTexture;Cloud Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;9198a05981c7daf45a2eeb2e9867953b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;123;-1104,592;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;122;-1168,320;Inherit;False;Property;_FarColor;Far Color;2;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;127;-992,592;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-1056,-192;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;121;-1168,144;Inherit;False;Property;_NearColor;Near Color;1;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClipNode;5;-912,-192;Inherit;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0.9;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;115;-816,288;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-640.3967,-176.1984;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;111;-512,64;Inherit;False;110;BentPosition;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;113;-389.74,217.092;Inherit;False;77;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;119;-1072,-880;Inherit;False;Distance;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;109;234,-167;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Plume Volumetric Clouds;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;49;0;138;0
WireConnection;49;1;137;0
WireConnection;51;0;49;0
WireConnection;50;0;51;0
WireConnection;50;1;51;2
WireConnection;44;0;50;0
WireConnection;58;0;44;0
WireConnection;58;1;41;0
WireConnection;60;0;58;0
WireConnection;52;0;60;0
WireConnection;52;1;60;0
WireConnection;52;2;56;0
WireConnection;52;3;57;0
WireConnection;53;0;52;0
WireConnection;53;1;42;0
WireConnection;110;0;53;0
WireConnection;116;0;78;0
WireConnection;82;0;84;0
WireConnection;82;1;83;0
WireConnection;81;0;117;0
WireConnection;81;1;82;0
WireConnection;81;2;112;0
WireConnection;79;0;93;0
WireConnection;79;1;81;0
WireConnection;86;0;79;0
WireConnection;89;0;86;1
WireConnection;89;1;87;0
WireConnection;88;0;86;0
WireConnection;88;1;89;0
WireConnection;88;2;86;2
WireConnection;80;0;88;0
WireConnection;77;0;80;0
WireConnection;153;0;154;0
WireConnection;153;1;155;0
WireConnection;92;0;91;0
WireConnection;92;1;69;0
WireConnection;157;0;153;0
WireConnection;13;1;14;0
WireConnection;15;0;92;0
WireConnection;15;1;16;0
WireConnection;156;0;157;0
WireConnection;10;0;15;0
WireConnection;10;1;13;0
WireConnection;10;2;156;0
WireConnection;97;0;10;0
WireConnection;150;0;149;0
WireConnection;150;1;120;0
WireConnection;31;0;97;0
WireConnection;152;0;150;0
WireConnection;30;0;6;0
WireConnection;30;1;2;0
WireConnection;30;2;32;0
WireConnection;123;0;152;0
WireConnection;123;1;126;0
WireConnection;127;0;123;0
WireConnection;4;0;30;0
WireConnection;4;1;3;0
WireConnection;5;0;4;0
WireConnection;5;1;3;4
WireConnection;115;0;121;0
WireConnection;115;1;122;0
WireConnection;115;2;127;0
WireConnection;114;0;5;0
WireConnection;114;1;115;0
WireConnection;119;0;44;0
WireConnection;109;2;114;0
WireConnection;109;11;111;0
WireConnection;109;12;113;0
ASEEND*/
//CHKSM=3DA68B2DD4AD07C3DB1036157561C347B0C108A0