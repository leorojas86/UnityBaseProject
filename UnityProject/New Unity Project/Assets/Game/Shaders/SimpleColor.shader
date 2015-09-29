Shader "Custom/SimpleColor" 
{
    Properties 
    {
         _Color ("Main Color", Color) = (0.5,0.5,0.5,0.5)
    }
    SubShader 
    {
		Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Opaque" }
		Fog  { Mode Off }
		
		ZTest On
		ZWrite On
		Cull Off
		Lighting Off
		LOD 100
		
        Pass 
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

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
                    return IN.color * 2;
                }
            ENDCG
        }
    }
}