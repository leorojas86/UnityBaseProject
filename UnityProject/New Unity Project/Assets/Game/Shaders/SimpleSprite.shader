Shader "Custom/SimpleSprite" 
{
    Properties 
    {
         _MainTex("Base (RGB)", 2D)   = "white" {}
         _Color ("Main Color", Color) = (0.5,0.5,0.5,0.5)
    }
    SubShader 
    {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Fog  { Mode Off }
		
		ZTest Off
		AlphaTest Off
		ZWrite Off
		Cull Off
		Lighting Off
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
		
        Pass 
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

            	sampler2D _MainTex;
            	fixed4 _Color;
			
				struct appdata 
				{
    				half4 vertex 	: POSITION;
			        half2 texcoord 	: TEXCOORD0;
			        fixed4 color 	: COLOR;
				};

                struct v2f 
                {
                    half4 pos 		: SV_POSITION;
                    half2 uv 		: TEXCOORD0;
                    fixed4 color 	: COLOR;
                };

                v2f vert(appdata v) 
                {
                    v2f o;
                    o.pos   = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv    = v.texcoord;
                    o.color = v.color * _Color;
                    
                    return o;
                }
                
                float4 frag(v2f IN) : COLOR
                {
                	//original color is 0.5,0.5,0.5,0.5 for that reason the result color is multiplied by 2
                    return tex2D(_MainTex, IN.uv) * IN.color * 2;
                }
            ENDCG
        }
    }
}