// Toony Colors Pro+Mobile 2
// (c) 2014-2023 Jean Moreno

Shader "Faded/Base"
{
	Properties
	{
		[Enum(Front, 2, Back, 1, Both, 0)] _Cull ("Render Face", Float) = 2.0
		[TCP2ToggleNoKeyword] _ZWrite ("Depth Write", Float) = 1.0
		[HideInInspector] _RenderingMode ("rendering mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("blending source", Float) = 1.0
		[HideInInspector] _DstBlend ("blending destination", Float) = 0.0
		[TCP2Separator]

		[TCP2HeaderHelp(Base)]
		_BaseColor ("Color", Color) = (1,1,1,1)
		[TCP2ColorNoAlpha] [HDR] _HColor ("Highlight Color", Color) = (0.75,0.75,0.75,1)
		[TCP2ColorNoAlpha] [HDR] _SColor ("Shadow Color", Color) = (0.2,0.2,0.2,1)
		[MainTexture] _BaseMap ("Albedo", 2D) = "white" {}
		_HSV_S ("Albedo Saturation", Range(0,1)) = 1
		[TCP2Separator]

		[TCP2Header(Ramp Shading)]
		
		[TCP2Gradient] _Ramp ("Ramp Texture (RGB)", 2D) = "gray" {}
		[TCP2Separator]
		
		[TCP2HeaderHelp(Specular)]
		[Toggle(TCP2_SPECULAR)] _UseSpecular ("Enable Specular", Float) = 0
		[TCP2ColorNoAlpha] [HDR] _SpecularColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_SpecularRoughnessPBR ("Roughness", Range(0,1)) = 0.5
		[TCP2Separator]
		
		[TCP2HeaderHelp(Rim Lighting)]
		[Toggle(TCP2_RIM_LIGHTING)] _UseRim ("Enable Rim Lighting", Float) = 0
		[TCP2ColorNoAlpha] [HDR] _RimColor ("Rim Color", Color) = (0.8,0.8,0.8,0.5)
		_RimMin ("Rim Min", Range(0,2)) = 0.5
		_RimMax ("Rim Max", Range(0,2)) = 1
		[TCP2Separator]
		
		[TCP2HeaderHelp(MatCap)]
		[Toggle(TCP2_MATCAP)] _UseMatCap ("Enable MatCap", Float) = 0
		[NoScaleOffset] [NoScaleOffset] _MatCapTex ("MatCap (RGB)", 2D) = "gray" {}
		[TCP2ColorNoAlpha] [HDR] _MatCapColor ("MatCap Color", Color) = (0.5,0.5,0.5,1)
		[TCP2Separator]
		
		[TCP2HeaderHelp(Vertical Fog)]
		[Toggle(TCP2_VERTICAL_FOG)] _UseVerticalFog ("Enable Vertical Fog", Float) = 0
		_VerticalFogThreshold ("Y Threshold", Float) = 0
		_VerticalFogSmoothness ("Smoothness", Float) = 0.5
		_VerticalFogColor ("Fog Color", Color) = (0.5,0.5,0.5,1)
		[TCP2Separator]
		
		[Enum(ToonyColorsPro.ShaderGenerator.Culling)] _faceCulling ("Face Culling", Float) = 2
		
		// Custom Material Properties
		 _FlashColor ("Flash Color", Color) = (1,1,1,1)
		 _FlashRange ("Flash Range", Range(0,1)) = 0
		
		[TCP2Separator]
		[TCP2HeaderHelp(MATERIAL LAYERS)]

		[TCP2Separator]
		[TCP2Header(Flash)]

		[ToggleOff(_RECEIVE_SHADOWS_OFF)] _ReceiveShadowsOff ("Receive Shadows", Float) = 1

		// Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType"="Opaque"
		}

		HLSLINCLUDE
		#define fixed half
		#define fixed2 half2
		#define fixed3 half3
		#define fixed4 half4

		#if UNITY_VERSION >= 202020
			#define URP_10_OR_NEWER
		#endif
		#if UNITY_VERSION >= 202120
			#define URP_12_OR_NEWER
		#endif
		#if UNITY_VERSION >= 202220
			#define URP_14_OR_NEWER
		#endif

		// Texture/Sampler abstraction
		#define TCP2_TEX2D_WITH_SAMPLER(tex)						TEXTURE2D(tex); SAMPLER(sampler##tex)
		#define TCP2_TEX2D_NO_SAMPLER(tex)							TEXTURE2D(tex)
		#define TCP2_TEX2D_SAMPLE(tex, samplertex, coord)			SAMPLE_TEXTURE2D(tex, sampler##samplertex, coord)
		#define TCP2_TEX2D_SAMPLE_LOD(tex, samplertex, coord, lod)	SAMPLE_TEXTURE2D_LOD(tex, sampler##samplertex, coord, lod)

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

		// Uniforms

		// Custom Material Properties

		// Shader Properties
		TCP2_TEX2D_WITH_SAMPLER(_BaseMap);

		TCP2_TEX2D_WITH_SAMPLER(_MatCapTex);
		sampler2D _Ramp;

		CBUFFER_START(UnityPerMaterial)
			
			// Custom Material Properties
			fixed4 _FlashColor;
			float _FlashRange;

			// Shader Properties
			float4 _BaseMap_ST;

			float _HSV_S;
			fixed4 _BaseColor;

			half4 _MatCapColor;
			float _RimMin;
			float _RimMax;
			half4 _RimColor;
			float _SpecularRoughnessPBR;
			half4 _SpecularColor;
			half4 _SColor;
			half4 _HColor;
			float _VerticalFogThreshold;
			float _VerticalFogSmoothness;
			fixed4 _VerticalFogColor;
		CBUFFER_END

		//--------------------------------
		// HSV HELPERS
		// source: http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
		
		float3 rgb2hsv(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
			float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
		
			float d = q.x - min(q.w, q.y);
			float e = 1.0e-10;
			return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}
		
		float3 hsv2rgb(float3 c)
		{
			c.g = max(c.g, 0.0); //make sure that saturation value is positive
			float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
		}
		
		#ifdef UNITY_COLORSPACE_GAMMA
			#define ColorSpaceLuminance half4(0.22, 0.707, 0.071, 0.0) // Legacy: alpha is set to 0.0 to specify gamma mode
		#else // Linear values
			#define ColorSpaceLuminance half4(0.0396819152, 0.458021790, 0.00609653955, 1.0) // Legacy: alpha is set to 1.0 to specify linear mode
		#endif
		
		float3 ApplyHSVGrayscale(float3 color, float s)
		{
			return lerp(dot(color, ColorSpaceLuminance.rgb).xxx, color, s);
		}
		
		float4 ApplyHSVGrayscale(float4 color, float s)
		{
			return float4(lerp(dot(color.rgb, ColorSpaceLuminance.rgb).xxx, color.rgb, s), color.a);
		}
		
		//Specular help functions (from UnityStandardBRDF.cginc)
		inline half3 SpecSafeNormalize(half3 inVec)
		{
			half dp3 = max(0.001f, dot(inVec, inVec));
			return inVec * rsqrt(dp3);
		}
		
		//GGX
		#define TCP2_PI			3.14159265359
		#define TCP2_INV_PI		0.31830988618f
		#if defined(SHADER_API_MOBILE)
			#define TCP2_EPSILON 1e-4f
		#else
			#define TCP2_EPSILON 1e-7f
		#endif
		inline half GGX(half NdotH, half roughness)
		{
			half a2 = roughness * roughness;
			half d = (NdotH * a2 - NdotH) * NdotH + 1.0f;
			return TCP2_INV_PI * a2 / (d * d + TCP2_EPSILON);
		}
		
		// Built-in renderer (CG) to SRP (HLSL) bindings
		#define UnityObjectToClipPos TransformObjectToHClip
		#define _WorldSpaceLightPos0 _MainLightPosition
		
		ENDHLSL

		Pass
		{
			Name "Main"
			Tags
			{
				"LightMode"="UniversalForward"
			}
		Blend [_SrcBlend] [_DstBlend]
		Cull [_Cull]
		ZWrite [_ZWrite]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard SRP library
			// All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.5

			// -------------------------------------
			// Material keywords
			#pragma shader_feature_local _ _RECEIVE_SHADOWS_OFF

			// -------------------------------------
			// Universal Render Pipeline keywords
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile_fragment _ _SHADOWS_SOFT
			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK

			// -------------------------------------
			#pragma multi_compile_fog

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex Vertex
			#pragma fragment Fragment

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local_fragment TCP2_SPECULAR
			#pragma shader_feature_local_fragment TCP2_RIM_LIGHTING
		#pragma shader_feature_local _ _ALPHAPREMULTIPLY_ON
			#pragma shader_feature_local TCP2_MATCAP
			#pragma shader_feature_local_fragment TCP2_VERTICAL_FOG

			// vertex input
			struct Attributes
			{
				float4 vertex       : POSITION;
				float3 normal       : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			// vertex output / fragment input
			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float3 normal         : NORMAL;
				float4 worldPosAndFog : TEXCOORD0;
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord    : TEXCOORD1; // compute shadow coord per-vertex for the main light
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				half3 vertexLights : TEXCOORD2;
			#endif
				float4 pack0 : TEXCOORD3; /* pack0.xy = texcoord0  pack0.zw = matcap */
				float pack1 : TEXCOORD4; /* pack1.x = fogFactor */
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings Vertex(Attributes input)
			{
				Varyings output = (Varyings)0;

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				// Texture Coordinates
				output.pack0.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;

				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				output.shadowCoord = GetShadowCoord(vertexInput);
			#endif

				VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(input.normal);
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				// Vertex lighting
				output.vertexLights = VertexLighting(vertexInput.positionWS, vertexNormalInput.normalWS);
			#endif

				// world position
				output.worldPosAndFog = float4(vertexInput.positionWS.xyz, 0);

				// Computes fog factor per-vertex
				output.worldPosAndFog.w = ComputeFogFactor(vertexInput.positionCS.z);

				// normal
				output.normal = normalize(vertexNormalInput.normalWS);

				// clip position
				output.positionCS = vertexInput.positionCS;

				#if defined(TCP2_MATCAP)
				//MatCap
				float3 worldNorm = normalize(unity_WorldToObject[0].xyz * input.normal.x + unity_WorldToObject[1].xyz * input.normal.y + unity_WorldToObject[2].xyz * input.normal.z);
				worldNorm = mul((float3x3)UNITY_MATRIX_V, worldNorm);
				output.pack0.zw = worldNorm.xy * 0.5 + 0.5;
				#endif

				return output;
			}

			half4 Fragment(Varyings input
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				float3 positionWS = input.worldPosAndFog.xyz;
				float3 normalWS = normalize(input.normal);
				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - positionWS);

				// Shader Properties Sampling
				float4 __albedo = ( TCP2_TEX2D_SAMPLE(_BaseMap, _BaseMap, input.pack0.xy).rgba );
				float4 __albedo_flash = ( _FlashColor.rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float4 __mainColor_flash = ( _FlashColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );
				float __albedoSaturationOnly = ( _HSV_S );
				float __ambientIntensity = ( 1.0 );
				float3 __matcapColor = ( _MatCapColor.rgb );
				float __rimMin = ( _RimMin );
				float __rimMax = ( _RimMax );
				float3 __rimColor = ( _RimColor.rgb );
				float __rimStrength = ( 1.0 );
				float __specularRoughnessPbr = ( _SpecularRoughnessPBR );
				float3 __specularColor = ( _SpecularColor.rgb );
				float3 __shadowColor = ( _SColor.rgb );
				float3 __highlightColor = ( _HColor.rgb );
				float __verticalFogThreshold = ( _VerticalFogThreshold );
				float __verticalFogSmoothness = ( _VerticalFogSmoothness );
				float4 __verticalFogColor = ( _VerticalFogColor.rgba );
				float __layer_flash = saturate( _FlashRange.x );

				// Material Layers Blending
				 __albedo = lerp(__albedo, __albedo_flash, __layer_flash);
				 __mainColor = lerp(__mainColor, __mainColor_flash, __layer_flash);

				half ndv = abs(dot(viewDirWS, normalWS));
				half ndvRaw = ndv;

				// main texture
				half3 albedo = __albedo.rgb;
				half alpha = __alpha;

				half3 emission = half3(0,0,0);
							
				//Albedo Saturation
				float3 albedoLuminance = Luminance(albedo);
				albedo = ApplyHSVGrayscale(albedo, __albedoSaturationOnly);
				
				albedo *= __mainColor.rgb;

				// main light: direction, color, distanceAttenuation, shadowAttenuation
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord = input.shadowCoord;
			#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
				float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
			#else
				float4 shadowCoord = float4(0, 0, 0, 0);
			#endif

			#if defined(URP_10_OR_NEWER)
				#if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
					half4 shadowMask = SAMPLE_SHADOWMASK(input.uvLM);
				#elif !defined (LIGHTMAP_ON)
					half4 shadowMask = unity_ProbesOcclusion;
				#else
					half4 shadowMask = half4(1, 1, 1, 1);
				#endif

				Light mainLight = GetMainLight(shadowCoord, positionWS, shadowMask);
			#else
				Light mainLight = GetMainLight(shadowCoord);
			#endif

				// ambient or lightmap
				// Samples SH fully per-pixel. SampleSHVertex and SampleSHPixel functions
				// are also defined in case you want to sample some terms per-vertex.
				half3 bakedGI = SampleSH(normalWS);
				half occlusion = 1;

				half3 indirectDiffuse = bakedGI;
				indirectDiffuse *= occlusion * albedo * __ambientIntensity;

				//MatCap
				#if defined(TCP2_MATCAP)
				half2 capCoord = input.pack0.zw;
				half3 matcap = ( TCP2_TEX2D_SAMPLE(_MatCapTex, _MatCapTex, capCoord).rgb ) * __matcapColor;
				emission += matcap;
				#endif

				half3 lightDir = mainLight.direction;
				half3 lightColor = mainLight.color.rgb;

				half atten = mainLight.shadowAttenuation * mainLight.distanceAttenuation;

				half ndl = dot(normalWS, lightDir);
				half3 ramp;
				
				half2 rampUv = ndl.xx * 0.5 + 0.5;
				ramp = tex2D(_Ramp, rampUv).rgb;

				// apply attenuation
				ramp *= atten;

				half3 color = half3(0,0,0);
				// Rim Lighting
				#if defined(TCP2_RIM_LIGHTING)
				half rim = 1 - ndvRaw;
				rim = ( rim );
				half rimMin = __rimMin;
				half rimMax = __rimMax;
				rim = smoothstep(rimMin, rimMax, rim);
				half3 rimColor = __rimColor;
				half rimStrength = __rimStrength;
				emission.rgb += rim * rimColor * rimStrength;
				#endif
				half3 accumulatedRamp = ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
				half3 accumulatedColors = ramp * lightColor.rgb;

				#if defined(TCP2_SPECULAR)
				//Specular: GGX
				half3 halfDir = SpecSafeNormalize(lightDir + viewDirWS);
				half roughness = __specularRoughnessPbr*__specularRoughnessPbr;
				half nh = saturate(dot(normalWS, halfDir));
				half spec = GGX(nh, saturate(roughness));
				spec *= TCP2_PI * 0.05;
				#ifdef UNITY_COLORSPACE_GAMMA
					spec = max(0, sqrt(max(1e-4h, spec)));
					half surfaceReduction = 1.0 - 0.28 * roughness * __specularRoughnessPbr;
				#else
					half surfaceReduction = 1.0 / (roughness*roughness + 1.0);
				#endif
				spec = max(0, spec * ndl);
				spec *= surfaceReduction;
				spec *= atten;
				
				//Apply specular
				emission.rgb += spec * lightColor.rgb * __specularColor;
				#endif

				// Additional lights loop
			#ifdef _ADDITIONAL_LIGHTS
				uint pixelLightCount = GetAdditionalLightsCount();

				LIGHT_LOOP_BEGIN(pixelLightCount)
				{
					#if defined(URP_10_OR_NEWER)
						Light light = GetAdditionalLight(lightIndex, positionWS, shadowMask);
					#else
						Light light = GetAdditionalLight(lightIndex, positionWS);
					#endif
					half atten = light.shadowAttenuation * light.distanceAttenuation;

					#if defined(_LIGHT_LAYERS)
						half3 lightDir = half3(0, 1, 0);
						half3 lightColor = half3(0, 0, 0);
						if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
						{
							lightColor = light.color.rgb;
							lightDir = light.direction;
						}
					#else
						half3 lightColor = light.color.rgb;
						half3 lightDir = light.direction;
					#endif

					half ndl = dot(normalWS, lightDir);
					half3 ramp;
					
					sampler2D rampTexture = _Ramp;
					half2 rampUv = ndl;
					ramp = tex2D(rampTexture, rampUv).rgb;

					// apply attenuation (shadowmaps & point/spot lights attenuation)
					ramp *= atten;

					accumulatedRamp += ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
					accumulatedColors += ramp * lightColor.rgb;

					#if defined(TCP2_SPECULAR)
					//Specular: GGX
					half3 halfDir = SpecSafeNormalize(lightDir + viewDirWS);
					half roughness = __specularRoughnessPbr*__specularRoughnessPbr;
					half nh = saturate(dot(normalWS, halfDir));
					half spec = GGX(nh, saturate(roughness));
					spec *= TCP2_PI * 0.05;
					#ifdef UNITY_COLORSPACE_GAMMA
						spec = max(0, sqrt(max(1e-4h, spec)));
						half surfaceReduction = 1.0 - 0.28 * roughness * __specularRoughnessPbr;
					#else
						half surfaceReduction = 1.0 / (roughness*roughness + 1.0);
					#endif
					spec = max(0, spec * ndl);
					spec *= surfaceReduction;
					spec *= atten;
					
					//Apply specular
					emission.rgb += spec * lightColor.rgb * __specularColor;
					#endif
				}
				LIGHT_LOOP_END
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				color += input.vertexLights * albedo;
			#endif

				accumulatedRamp = saturate(accumulatedRamp);
				half3 shadowColor = (1 - accumulatedRamp.rgb) * __shadowColor;
				accumulatedRamp = accumulatedColors.rgb * __highlightColor + shadowColor;
				color += albedo * accumulatedRamp;

				// apply ambient
				color += indirectDiffuse;

				// Premultiply blending
				#if defined(_ALPHAPREMULTIPLY_ON)
					color.rgb *= alpha;
				#endif

				color += emission;

				// Mix the pixel color with fogColor. You can optionally use MixFogColor to override the fogColor with a custom one.
				float fogFactor = input.worldPosAndFog.w;
				color = MixFog(color, fogFactor);
				
				// Vertical Fog
				#if defined(TCP2_VERTICAL_FOG)
				half vertFogThreshold = input.worldPosAndFog.xyz.y;
				half verticalFogThreshold = __verticalFogThreshold;
				half verticalFogSmooothness = __verticalFogSmoothness;
				half verticalFogMin = verticalFogThreshold - verticalFogSmooothness;
				half verticalFogMax = verticalFogThreshold + verticalFogSmooothness;
				half4 fogColor = __verticalFogColor;
				#if defined(UNITY_PASS_FORWARDADD)
					fogColor.rgb = half3(0, 0, 0);
				#endif
				half vertFogFactor = 1 - smoothstep(verticalFogMin, verticalFogMax, vertFogThreshold);
				vertFogFactor *= fogColor.a;
				color.rgb = lerp(color.rgb, fogColor.rgb, vertFogFactor);
				#endif

				return half4(color, alpha);
			}
			ENDHLSL
		}

		// Depth & Shadow Caster Passes
		HLSLINCLUDE

		#if defined(SHADOW_CASTER_PASS) || defined(DEPTH_ONLY_PASS)

			#define fixed half
			#define fixed2 half2
			#define fixed3 half3
			#define fixed4 half4

			float3 _LightDirection;
			float3 _LightPosition;

			struct Attributes
			{
				float4 vertex   : POSITION;
				float3 normal   : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float3 normal         : NORMAL;
				float3 pack0 : TEXCOORD1; /* pack0.xyz = positionWS */
				float4 pack1 : TEXCOORD2; /* pack1.xy = texcoord0  pack1.zw = matcap */
			#if defined(DEPTH_ONLY_PASS)
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			#endif
			};

			float4 GetShadowPositionHClip(Attributes input)
			{
				float3 positionWS = TransformObjectToWorld(input.vertex.xyz);
				float3 normalWS = TransformObjectToWorldNormal(input.normal);

				#if _CASTING_PUNCTUAL_LIGHT_SHADOW
					float3 lightDirectionWS = normalize(_LightPosition - positionWS);
				#else
					float3 lightDirectionWS = _LightDirection;
				#endif
				float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

				#if UNITY_REVERSED_Z
					positionCS.z = min(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#else
					positionCS.z = max(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#endif

				return positionCS;
			}

			Varyings ShadowDepthPassVertex(Attributes input)
			{
				Varyings output = (Varyings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				#if defined(DEPTH_ONLY_PASS)
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				#endif

				float3 worldNormalUv = mul(unity_ObjectToWorld, float4(input.normal, 1.0)).xyz;

				// Texture Coordinates
				output.pack1.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;

				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
				output.normal = normalize(worldNormalUv);
				output.pack0.xyz = vertexInput.positionWS;

				#if defined(DEPTH_ONLY_PASS)
					output.positionCS = TransformObjectToHClip(input.vertex.xyz);
				#elif defined(SHADOW_CASTER_PASS)
					output.positionCS = GetShadowPositionHClip(input);
				#else
					output.positionCS = float4(0,0,0,0);
				#endif

				return output;
			}

			half4 ShadowDepthPassFragment(
				Varyings input
			) : SV_TARGET
			{
				#if defined(DEPTH_ONLY_PASS)
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
				#endif

				float3 positionWS = input.pack0.xyz;
				float3 normalWS = normalize(input.normal);

				// Shader Properties Sampling
				float4 __albedo = ( TCP2_TEX2D_SAMPLE(_BaseMap, _BaseMap, input.pack1.xy).rgba );
				float4 __albedo_flash = ( _FlashColor.rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float4 __mainColor_flash = ( _FlashColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );
				float __layer_flash = saturate( _FlashRange.x );

				// Material Layers Blending
				 __albedo = lerp(__albedo, __albedo_flash, __layer_flash);
				 __mainColor = lerp(__mainColor, __mainColor_flash, __layer_flash);

				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - positionWS);
				half ndv = abs(dot(viewDirWS, normalWS));
				half ndvRaw = ndv;

				half3 albedo = half3(1,1,1);
				half alpha = __alpha;
				half3 emission = half3(0,0,0);

				return 0;
			}

		#endif
		ENDHLSL

		Pass
		{
			Name "ShadowCaster"
			Tags
			{
				"LightMode" = "ShadowCaster"
			}

			ZWrite On
			ZTest LEqual
			Cull [_faceCulling]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile SHADOW_CASTER_PASS

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing
			#pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

			ENDHLSL
		}

		Pass
		{
			Name "DepthOnly"
			Tags
			{
				"LightMode" = "DepthOnly"
			}

			ZWrite On
			ColorMask 0
			Cull [_Cull]

			HLSLPROGRAM

			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile DEPTH_ONLY_PASS

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			ENDHLSL
		}

	}

	FallBack "Hidden/InternalErrorShader"
	CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}

/* TCP_DATA u config(ver:"2.9.6";unity:"2022.3.4f1";tmplt:"SG2_Template_URP";features:list["UNITY_5_4","UNITY_5_5","UNITY_5_6","UNITY_2017_1","UNITY_2018_1","UNITY_2018_2","UNITY_2018_3","UNITY_2019_1","UNITY_2019_2","UNITY_2019_3","UNITY_2019_4","UNITY_2020_1","UNITY_2021_1","UNITY_2021_2","UNITY_2022_2","TEXTURE_RAMP","SPEC_PBR_GGX","SPECULAR","SPECULAR_SHADER_FEATURE","RIM","RIM_SHADER_FEATURE","MATCAP_ADD","MATCAP","MATCAP_SHADER_FEATURE","TT_SHADER_FEATURE","VERTICAL_FOG","VERTICAL_FOG_ALPHA","VERTICAL_FOG_SHADER_FEATURE","FOG","VERTICAL_FOG_SMOOTHSTEP","CULLING","AUTO_TRANSPARENT_BLENDING","ALBEDO_HSV_SATURATION","TEMPLATE_LWRP"];flags:list[];flags_extra:dict[];keywords:dict[RENDER_TYPE="Opaque",RampTextureDrawer="[TCP2Gradient]",RampTextureLabel="Ramp Texture",SHADER_TARGET="2.5",RIM_LABEL="Rim Lighting"];shaderProperties:list[sp(name:"Albedo";imps:list[imp_mp_texture(uto:True;tov:"";tov_lbl:"";gto:True;sbt:False;scr:False;scv:"";scv_lbl:"";gsc:False;roff:False;goff:False;sin_anm:False;sin_anmv:"";sin_anmv_lbl:"";gsin:False;notile:False;triplanar_local:False;def:"white";locked_uv:False;uv:0;cc:4;chan:"RGBA";mip:-1;mipprop:False;ssuv_vert:False;ssuv_obj:False;uv_type:Texcoord;uv_chan:"XZ";tpln_scale:1;uv_shaderproperty:__NULL__;uv_cmp:__NULL__;sep_sampler:__NULL__;prop:"_BaseMap";md:"[MainTexture]";gbv:False;custom:False;refs:"";pnlock:False;guid:"6c474aea-2223-4d78-ae02-eac0f6bf6cfe";op:Multiply;lbl:"Albedo";gpu_inst:False;locked:False;impl_index:0)];layers:list["35846a"];unlocked:list["35846a"];clones:dict[35846a=sp(name:"Albedo_35846a";imps:list[imp_ct(lct:"_FlashColor";cc:4;chan:"RGBA";avchan:"RGBA";guid:"c263f8ef-33c7-4677-8c20-bf175fa8048d";op:Multiply;lbl:"FlashColor";gpu_inst:False;locked:False;impl_index:-1)];layers:list[];unlocked:list[];clones:dict[];isClone:True)];isClone:False),,sp(name:"Main Color";imps:list[imp_mp_color(def:RGBA(1, 1, 1, 1);hdr:False;cc:4;chan:"RGBA";prop:"_BaseColor";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"0664a5e1-c9b6-487b-8e49-28e32e555741";op:Multiply;lbl:"Color";gpu_inst:False;locked:False;impl_index:0)];layers:list["35846a"];unlocked:list["35846a"];clones:dict[35846a=sp(name:"Main Color_35846a";imps:list[imp_ct(lct:"_FlashColor";cc:4;chan:"RGBA";avchan:"RGBA";guid:"1d341509-d321-4930-94a2-afbdb0b29197";op:Multiply;lbl:"Color";gpu_inst:False;locked:False;impl_index:-1)];layers:list[];unlocked:list[];clones:dict[];isClone:True)];isClone:False),,,sp(name:"Highlight Color";imps:list[imp_mp_color(def:RGBA(0.75, 0.75, 0.75, 1);hdr:True;cc:3;chan:"RGB";prop:"_HColor";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"a35a0d9b-e735-4cdd-b9a9-f4e665e92344";op:Multiply;lbl:"Highlight Color";gpu_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False),sp(name:"Shadow Color";imps:list[imp_mp_color(def:RGBA(0.2, 0.2, 0.2, 1);hdr:True;cc:3;chan:"RGB";prop:"_SColor";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"43e692e1-87ca-4006-bab0-068363164f3e";op:Multiply;lbl:"Shadow Color";gpu_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False),sp(name:"Specular Color";imps:list[imp_mp_color(def:RGBA(0.5, 0.5, 0.5, 1);hdr:True;cc:3;chan:"RGB";prop:"_SpecularColor";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"f1d7a177-637c-4b06-b918-c5aaadfd3ab0";op:Multiply;lbl:"Specular Color";gpu_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False),,sp(name:"Rim Color";imps:list[imp_mp_color(def:RGBA(0.8, 0.8, 0.8, 0.5);hdr:True;cc:3;chan:"RGB";prop:"_RimColor";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"9b6d1fde-9234-4552-ae30-2b97e7cf47df";op:Multiply;lbl:"Rim Color";gpu_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False),,,,,sp(name:"MatCap Color";imps:list[imp_mp_color(def:RGBA(0.5, 0.5, 0.5, 1);hdr:True;cc:3;chan:"RGB";prop:"_MatCapColor";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"4d049580-9c7c-4bad-b14a-8538d3107825";op:Multiply;lbl:"MatCap Color";gpu_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False),,,,,sp(name:"Face Culling";imps:list[imp_enum(value_type:1;value:0;enum_type:"ToonyColorsPro.ShaderGenerator.Culling";guid:"2e16b2fd-c514-4c8f-b422-34485cd59e49";op:Multiply;lbl:"Face Culling";gpu_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False)];customTextures:list[ct(cimp:imp_mp_range(def:0;min:0;max:1;prop:"_FlashRange";md:"";gbv:False;custom:True;refs:"Flash Source";pnlock:False;guid:"3c903335-fa04-4d88-b8b5-bd760c7da46d";op:Multiply;lbl:"Flash Range";gpu_inst:False;locked:False;impl_index:-1);exp:True;uv_exp:False;imp_lbl:"Range"),ct(cimp:imp_mp_color(def:RGBA(1, 1, 1, 1);hdr:False;cc:4;chan:"RGBA";prop:"_FlashColor";md:"";gbv:False;custom:True;refs:"";pnlock:False;guid:"38c9d798-36b4-4ecd-b603-144169eca004";op:Multiply;lbl:"Flash Color";gpu_inst:False;locked:False;impl_index:-1);exp:True;uv_exp:False;imp_lbl:"Color")];codeInjection:codeInjection(injectedFiles:list[];mark:False);matLayers:list[ml(uid:"35846a";name:"Flash";src:sp(name:"layer_35846a";imps:list[imp_ct(lct:"_FlashRange";cc:1;chan:"X";avchan:"X";guid:"1bdac4e8-6d44-4fae-964f-380a76afb0e9";op:Multiply;lbl:"Source Texture";gpu_inst:False;locked:False;impl_index:-1)];layers:list[];unlocked:list[];clones:dict[];isClone:False);use_contrast:False;ctrst:__NULL__;use_noise:False;noise:__NULL__)]) */
/* TCP_HASH cf0d24b641a73a3852a979c96950f34e */
