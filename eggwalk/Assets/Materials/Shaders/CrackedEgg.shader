// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:False,mssp:False,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:1,vtps:0,hqsc:True,nrmq:1,nrsp:1,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32809,y:32713,varname:node_2865,prsc:2|diff-8041-OUT,spec-358-OUT,gloss-1813-OUT,normal-9981-OUT,amdfl-4932-OUT,amspl-4750-OUT;n:type:ShaderForge.SFN_Slider,id:358,x:31982,y:32854,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:31979,y:32958,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Tex2d,id:5903,x:30990,y:32705,ptovrint:False,ptlb:MaskLevel1,ptin:_MaskLevel1,varname:node_5903,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:eaff7b082659aa145817525c94a84586,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3375,x:31636,y:32564,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_3375,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e3a727b2a513e74b8c9b83f78d4f471,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6114,x:31224,y:33495,ptovrint:False,ptlb:NormalLevel1,ptin:_NormalLevel1,varname:node_6114,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4bb951f4b40ca434aab8a86b32e40e98,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:8667,x:31663,y:33599,ptovrint:False,ptlb:SpecularLevel1,ptin:_SpecularLevel1,varname:node_8667,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:19fa1d08a3b21ef42a5cd5d3c4e861a1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6319,x:30990,y:32891,ptovrint:False,ptlb:MaskLevel2,ptin:_MaskLevel2,varname:node_6319,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aca412df43472834b87f906685f7c1ad,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:646,x:30990,y:33293,ptovrint:False,ptlb:CrackingLevel,ptin:_CrackingLevel,varname:node_646,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:94,x:30990,y:33075,ptovrint:False,ptlb:MaskLevel3,ptin:_MaskLevel3,varname:node_94,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9d40a2a4529085e4bb0cf96ce4f68133,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8192,x:31224,y:33682,ptovrint:False,ptlb:NormalLevel2,ptin:_NormalLevel2,varname:node_8192,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aab93ecdde22ef845857089b89deb318,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Code,id:9981,x:31774,y:33259,varname:node_9981,prsc:2,code:aQBmACAAKABWAGEAbAAgAD0APQAgADAAKQAgAHsACgAgACAAcgBlAHQAdQByAG4AIABMAGUAdgBlAGwAMAA7AAoAfQAgAGUAbABzAGUAIABpAGYAIAAoAFYAYQBsACAAPQA9ACAAMQApACAAewAKACAAIAByAGUAdAB1AHIAbgAgAEwAZQB2AGUAbAAxADsACgB9ACAAZQBsAHMAZQAgAGkAZgAgACgAVgBhAGwAIAA9AD0AIAAyACkAIAB7AAoAIAAgAHIAZQB0AHUAcgBuACAATABlAHYAZQBsADIAOwAKAH0AIABlAGwAcwBlACAAewAKACAAIAByAGUAdAB1AHIAbgAgAEwAZQB2AGUAbAAzADsACgB9AA==,output:2,fname:Function_node_4212,width:247,height:148,input:0,input:2,input:2,input:2,input:2,input_1_label:Val,input_2_label:Level0,input_3_label:Level1,input_4_label:Level2,input_5_label:Level3|A-646-OUT,B-2897-OUT,C-6114-RGB,D-8192-RGB,E-6105-RGB;n:type:ShaderForge.SFN_Tex2d,id:6105,x:31224,y:33880,ptovrint:False,ptlb:NormalLevel3,ptin:_NormalLevel3,varname:node_6105,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4f2a2a0af56b2c94eae36c84b9aa0c07,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:2567,x:31669,y:33789,ptovrint:False,ptlb:SpecularLevel2,ptin:_SpecularLevel2,varname:node_2567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:689523670b8baf64483c2b8a8d048de0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8377,x:31669,y:33982,ptovrint:False,ptlb:SpecularLevel3,ptin:_SpecularLevel3,varname:node_8377,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28264c14dbeba9c40ab3b6864e97f2c5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector3,id:2897,x:31224,y:33377,varname:node_2897,prsc:2,v1:1,v2:0,v3:0;n:type:ShaderForge.SFN_Vector3,id:4858,x:31663,y:33491,varname:node_4858,prsc:2,v1:0.5176471,v2:0.5215687,v3:0.5176471;n:type:ShaderForge.SFN_Code,id:7994,x:31990,y:33458,varname:node_7994,prsc:2,code:aQBmACAAKABWAGEAbAAgAD0APQAgADAAKQAgAHsACgAgACAAcgBlAHQAdQByAG4AIABMAGUAdgBlAGwAMAA7AAoAfQAgAGUAbABzAGUAIABpAGYAIAAoAFYAYQBsACAAPQA9ACAAMQApACAAewAKACAAIAByAGUAdAB1AHIAbgAgAEwAZQB2AGUAbAAxADsACgB9ACAAZQBsAHMAZQAgAGkAZgAgACgAVgBhAGwAIAA9AD0AIAAyACkAIAB7AAoAIAAgAHIAZQB0AHUAcgBuACAATABlAHYAZQBsADIAOwAKAH0AIABlAGwAcwBlACAAewAKACAAIAByAGUAdAB1AHIAbgAgAEwAZQB2AGUAbAAzADsACgB9AA==,output:2,fname:Function_node_4213,width:247,height:148,input:0,input:2,input:2,input:2,input:2,input_1_label:Val,input_2_label:Level0,input_3_label:Level1,input_4_label:Level2,input_5_label:Level3|A-646-OUT,B-4858-OUT,C-8667-RGB,D-2567-RGB,E-8377-RGB;n:type:ShaderForge.SFN_Code,id:3528,x:31300,y:32851,varname:node_3528,prsc:2,code:aQBmACAAKABWAGEAbAAgAD0APQAgADAAKQAgAHsACgAgACAAcgBlAHQAdQByAG4AIABMAGUAdgBlAGwAMAA7AAoAfQAgAGUAbABzAGUAIABpAGYAIAAoAFYAYQBsACAAPQA9ACAAMQApACAAewAKACAAIAByAGUAdAB1AHIAbgAgAEwAZQB2AGUAbAAxADsACgB9ACAAZQBsAHMAZQAgAGkAZgAgACgAVgBhAGwAIAA9AD0AIAAyACkAIAB7AAoAIAAgAHIAZQB0AHUAcgBuACAATABlAHYAZQBsADIAOwAKAH0AIABlAGwAcwBlACAAewAKACAAIAByAGUAdAB1AHIAbgAgAEwAZQB2AGUAbAAzADsACgB9AA==,output:2,fname:Function_node_4211,width:247,height:148,input:0,input:2,input:2,input:2,input:2,input_1_label:Val,input_2_label:Level0,input_3_label:Level1,input_4_label:Level2,input_5_label:Level3|A-646-OUT,B-8414-OUT,C-5903-RGB,D-6319-RGB,E-94-RGB;n:type:ShaderForge.SFN_Vector3,id:8414,x:30990,y:32580,varname:node_8414,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Multiply,id:4750,x:32348,y:33458,varname:node_4750,prsc:2|A-7994-OUT,B-5934-OUT;n:type:ShaderForge.SFN_Vector1,id:5934,x:32138,y:33630,varname:node_5934,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Color,id:9923,x:32222,y:32475,ptovrint:False,ptlb:ColorOverlay,ptin:_ColorOverlay,varname:node_9923,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:232,x:31686,y:32852,varname:node_232,prsc:2|A-3528-OUT,B-1530-OUT;n:type:ShaderForge.SFN_Color,id:9692,x:31300,y:33076,ptovrint:False,ptlb:CrackColor,ptin:_CrackColor,varname:node_9692,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_OneMinus,id:1530,x:31461,y:33076,varname:node_1530,prsc:2|IN-9692-RGB;n:type:ShaderForge.SFN_Add,id:9986,x:31867,y:32670,varname:node_9986,prsc:2|A-3375-RGB,B-3528-OUT;n:type:ShaderForge.SFN_Subtract,id:8352,x:32222,y:32646,varname:node_8352,prsc:2|A-9986-OUT,B-232-OUT;n:type:ShaderForge.SFN_Add,id:8041,x:32419,y:32554,varname:node_8041,prsc:2|A-9923-RGB,B-8352-OUT;n:type:ShaderForge.SFN_Vector3,id:4932,x:32510,y:33025,varname:node_4932,prsc:2,v1:1,v2:1,v3:1;proporder:3375-9923-9692-646-358-1813-5903-6319-94-6114-8192-6105-8667-2567-8377;pass:END;sub:END;*/

Shader "Shader Forge/CrackedEgg" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _ColorOverlay ("ColorOverlay", Color) = (0,0,0,1)
        _CrackColor ("CrackColor", Color) = (0,0,0,1)
        _CrackingLevel ("CrackingLevel", Float ) = 0
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0.1
        _MaskLevel1 ("MaskLevel1", 2D) = "white" {}
        _MaskLevel2 ("MaskLevel2", 2D) = "white" {}
        _MaskLevel3 ("MaskLevel3", 2D) = "white" {}
        _NormalLevel1 ("NormalLevel1", 2D) = "bump" {}
        _NormalLevel2 ("NormalLevel2", 2D) = "bump" {}
        _NormalLevel3 ("NormalLevel3", 2D) = "bump" {}
        _SpecularLevel1 ("SpecularLevel1", 2D) = "white" {}
        _SpecularLevel2 ("SpecularLevel2", 2D) = "white" {}
        _SpecularLevel3 ("SpecularLevel3", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "DEFERRED"
            Tags {
                "LightMode"="Deferred"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_DEFERRED
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile ___ UNITY_HDR_ON
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _MaskLevel1; uniform float4 _MaskLevel1_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _NormalLevel1; uniform float4 _NormalLevel1_ST;
            uniform sampler2D _SpecularLevel1; uniform float4 _SpecularLevel1_ST;
            uniform sampler2D _MaskLevel2; uniform float4 _MaskLevel2_ST;
            uniform float _CrackingLevel;
            uniform sampler2D _MaskLevel3; uniform float4 _MaskLevel3_ST;
            uniform sampler2D _NormalLevel2; uniform float4 _NormalLevel2_ST;
            float3 Function_node_4212( float Val , float3 Level0 , float3 Level1 , float3 Level2 , float3 Level3 ){
            if (Val == 0) {
              return Level0;
            } else if (Val == 1) {
              return Level1;
            } else if (Val == 2) {
              return Level2;
            } else {
              return Level3;
            }
            }
            
            uniform sampler2D _NormalLevel3; uniform float4 _NormalLevel3_ST;
            uniform sampler2D _SpecularLevel2; uniform float4 _SpecularLevel2_ST;
            uniform sampler2D _SpecularLevel3; uniform float4 _SpecularLevel3_ST;
            float3 Function_node_4213( float Val , float3 Level0 , float3 Level1 , float3 Level2 , float3 Level3 ){
            if (Val == 0) {
              return Level0;
            } else if (Val == 1) {
              return Level1;
            } else if (Val == 2) {
              return Level2;
            } else {
              return Level3;
            }
            }
            
            float3 Function_node_4211( float Val , float3 Level0 , float3 Level1 , float3 Level2 , float3 Level3 ){
            if (Val == 0) {
              return Level0;
            } else if (Val == 1) {
              return Level1;
            } else if (Val == 2) {
              return Level2;
            } else {
              return Level3;
            }
            }
            
            uniform float4 _ColorOverlay;
            uniform float4 _CrackColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD7;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            void frag(
                VertexOutput i,
                out half4 outDiffuse : SV_Target0,
                out half4 outSpecSmoothness : SV_Target1,
                out half4 outNormal : SV_Target2,
                out half4 outEmission : SV_Target3 )
            {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalLevel1_var = UnpackNormal(tex2D(_NormalLevel1,TRANSFORM_TEX(i.uv0, _NormalLevel1)));
                float3 _NormalLevel2_var = UnpackNormal(tex2D(_NormalLevel2,TRANSFORM_TEX(i.uv0, _NormalLevel2)));
                float3 _NormalLevel3_var = UnpackNormal(tex2D(_NormalLevel3,TRANSFORM_TEX(i.uv0, _NormalLevel3)));
                float3 normalLocal = Function_node_4212( _CrackingLevel , float3(1,0,0) , _NormalLevel1_var.rgb , _NormalLevel2_var.rgb , _NormalLevel3_var.rgb );
                float3 normalDirection = mul( _World2Object, float4(normalLocal,0)) / recipObjScale;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
/////// GI Data:
                UnityLight light; // Dummy light
                light.color = 0;
                light.dir = half3(0,1,0);
                light.ndotl = max(0,dot(normalDirection,light.dir));
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = 1;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
////// Specular:
                float4 _SpecularLevel1_var = tex2D(_SpecularLevel1,TRANSFORM_TEX(i.uv0, _SpecularLevel1));
                float4 _SpecularLevel2_var = tex2D(_SpecularLevel2,TRANSFORM_TEX(i.uv0, _SpecularLevel2));
                float4 _SpecularLevel3_var = tex2D(_SpecularLevel3,TRANSFORM_TEX(i.uv0, _SpecularLevel3));
                float3 specularColor = float3(_Metallic,_Metallic,_Metallic);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular + (Function_node_4213( _CrackingLevel , float3(0.5176471,0.5215687,0.5176471) , _SpecularLevel1_var.rgb , _SpecularLevel2_var.rgb , _SpecularLevel3_var.rgb )*0.1));
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
/////// Diffuse:
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += float3(1,1,1); // Diffuse Ambient Light
                indirectDiffuse += gi.indirect.diffuse;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float4 _MaskLevel1_var = tex2D(_MaskLevel1,TRANSFORM_TEX(i.uv0, _MaskLevel1));
                float4 _MaskLevel2_var = tex2D(_MaskLevel2,TRANSFORM_TEX(i.uv0, _MaskLevel2));
                float4 _MaskLevel3_var = tex2D(_MaskLevel3,TRANSFORM_TEX(i.uv0, _MaskLevel3));
                float3 node_3528 = Function_node_4211( _CrackingLevel , float3(0,0,0) , _MaskLevel1_var.rgb , _MaskLevel2_var.rgb , _MaskLevel3_var.rgb );
                float3 diffuseColor = (_ColorOverlay.rgb+((_Diffuse_var.rgb+node_3528)-(node_3528*(1.0 - _CrackColor.rgb))));
                diffuseColor *= 1-specularMonochrome;
/// Final Color:
                outDiffuse = half4( diffuseColor, 1 );
                outSpecSmoothness = half4( specularColor, gloss );
                outNormal = half4( normalDirection * 0.5 + 0.5, 1 );
                outEmission = half4(0,0,0,1);
                outEmission.rgb += indirectSpecular;
                outEmission.rgb += indirectDiffuse * diffuseColor;
                #ifndef UNITY_HDR_ON
                    outEmission.rgb = exp2(-outEmission.rgb);
                #endif
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _MaskLevel1; uniform float4 _MaskLevel1_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _NormalLevel1; uniform float4 _NormalLevel1_ST;
            uniform sampler2D _SpecularLevel1; uniform float4 _SpecularLevel1_ST;
            uniform sampler2D _MaskLevel2; uniform float4 _MaskLevel2_ST;
            uniform float _CrackingLevel;
            uniform sampler2D _MaskLevel3; uniform float4 _MaskLevel3_ST;
            uniform sampler2D _NormalLevel2; uniform float4 _NormalLevel2_ST;
            float3 Function_node_4212( float Val , float3 Level0 , float3 Level1 , float3 Level2 , float3 Level3 ){
            if (Val == 0) {
              return Level0;
            } else if (Val == 1) {
              return Level1;
            } else if (Val == 2) {
              return Level2;
            } else {
              return Level3;
            }
            }
            
            uniform sampler2D _NormalLevel3; uniform float4 _NormalLevel3_ST;
            uniform sampler2D _SpecularLevel2; uniform float4 _SpecularLevel2_ST;
            uniform sampler2D _SpecularLevel3; uniform float4 _SpecularLevel3_ST;
            float3 Function_node_4213( float Val , float3 Level0 , float3 Level1 , float3 Level2 , float3 Level3 ){
            if (Val == 0) {
              return Level0;
            } else if (Val == 1) {
              return Level1;
            } else if (Val == 2) {
              return Level2;
            } else {
              return Level3;
            }
            }
            
            float3 Function_node_4211( float Val , float3 Level0 , float3 Level1 , float3 Level2 , float3 Level3 ){
            if (Val == 0) {
              return Level0;
            } else if (Val == 1) {
              return Level1;
            } else if (Val == 2) {
              return Level2;
            } else {
              return Level3;
            }
            }
            
            uniform float4 _ColorOverlay;
            uniform float4 _CrackColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalLevel1_var = UnpackNormal(tex2D(_NormalLevel1,TRANSFORM_TEX(i.uv0, _NormalLevel1)));
                float3 _NormalLevel2_var = UnpackNormal(tex2D(_NormalLevel2,TRANSFORM_TEX(i.uv0, _NormalLevel2)));
                float3 _NormalLevel3_var = UnpackNormal(tex2D(_NormalLevel3,TRANSFORM_TEX(i.uv0, _NormalLevel3)));
                float3 normalLocal = Function_node_4212( _CrackingLevel , float3(1,0,0) , _NormalLevel1_var.rgb , _NormalLevel2_var.rgb , _NormalLevel3_var.rgb );
                float3 normalDirection = mul( _World2Object, float4(normalLocal,0)) / recipObjScale;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _SpecularLevel1_var = tex2D(_SpecularLevel1,TRANSFORM_TEX(i.uv0, _SpecularLevel1));
                float4 _SpecularLevel2_var = tex2D(_SpecularLevel2,TRANSFORM_TEX(i.uv0, _SpecularLevel2));
                float4 _SpecularLevel3_var = tex2D(_SpecularLevel3,TRANSFORM_TEX(i.uv0, _SpecularLevel3));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = float3(_Metallic,_Metallic,_Metallic);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular + (Function_node_4213( _CrackingLevel , float3(0.5176471,0.5215687,0.5176471) , _SpecularLevel1_var.rgb , _SpecularLevel2_var.rgb , _SpecularLevel3_var.rgb )*0.1));
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += float3(1,1,1); // Diffuse Ambient Light
                indirectDiffuse += gi.indirect.diffuse;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float4 _MaskLevel1_var = tex2D(_MaskLevel1,TRANSFORM_TEX(i.uv0, _MaskLevel1));
                float4 _MaskLevel2_var = tex2D(_MaskLevel2,TRANSFORM_TEX(i.uv0, _MaskLevel2));
                float4 _MaskLevel3_var = tex2D(_MaskLevel3,TRANSFORM_TEX(i.uv0, _MaskLevel3));
                float3 node_3528 = Function_node_4211( _CrackingLevel , float3(0,0,0) , _MaskLevel1_var.rgb , _MaskLevel2_var.rgb , _MaskLevel3_var.rgb );
                float3 diffuseColor = (_ColorOverlay.rgb+((_Diffuse_var.rgb+node_3528)-(node_3528*(1.0 - _CrackColor.rgb))));
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _MaskLevel1; uniform float4 _MaskLevel1_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _MaskLevel2; uniform float4 _MaskLevel2_ST;
            uniform float _CrackingLevel;
            uniform sampler2D _MaskLevel3; uniform float4 _MaskLevel3_ST;
            float3 Function_node_4211( float Val , float3 Level0 , float3 Level1 , float3 Level2 , float3 Level3 ){
            if (Val == 0) {
              return Level0;
            } else if (Val == 1) {
              return Level1;
            } else if (Val == 2) {
              return Level2;
            } else {
              return Level3;
            }
            }
            
            uniform float4 _ColorOverlay;
            uniform float4 _CrackColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float4 _MaskLevel1_var = tex2D(_MaskLevel1,TRANSFORM_TEX(i.uv0, _MaskLevel1));
                float4 _MaskLevel2_var = tex2D(_MaskLevel2,TRANSFORM_TEX(i.uv0, _MaskLevel2));
                float4 _MaskLevel3_var = tex2D(_MaskLevel3,TRANSFORM_TEX(i.uv0, _MaskLevel3));
                float3 node_3528 = Function_node_4211( _CrackingLevel , float3(0,0,0) , _MaskLevel1_var.rgb , _MaskLevel2_var.rgb , _MaskLevel3_var.rgb );
                float3 diffColor = (_ColorOverlay.rgb+((_Diffuse_var.rgb+node_3528)-(node_3528*(1.0 - _CrackColor.rgb))));
                float3 specColor = float3(_Metallic,_Metallic,_Metallic);
                float specularMonochrome = max(max(specColor.r, specColor.g),specColor.b);
                diffColor *= (1.0-specularMonochrome);
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
