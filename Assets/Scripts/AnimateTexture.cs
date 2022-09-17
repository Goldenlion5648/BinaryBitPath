using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTexture : MonoBehaviour
{
    Renderer render;
    float curXOffset;
    float curYOffset;
    public float animationSpeedX = .3f;
    public float animationSpeedY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        curXOffset = render.material.GetTextureOffset("_MainTex").x;
        curYOffset = render.material.GetTextureOffset("_MainTex").y;


    }

    // Update is called once per frame
    void Update()
    {
        curXOffset = (curXOffset + animationSpeedX * Time.deltaTime);
        if (animationSpeedY != 0f)
            curYOffset = (curYOffset + animationSpeedY * Time.deltaTime) % render.material.mainTextureScale.y;
        var offset = new Vector2(curXOffset, curYOffset);

        render.material.SetTextureOffset("_MainTex", offset);

    }
}
