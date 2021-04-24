using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SkyBoxManager : MonoBehaviour
    {
        private float anglePerFrame = 0.01f;    // 1ƒtƒŒ[ƒ€‚É‰½“x‰ñ‚·‚©[unit : deg]
        private float rot = 0.0f;
        
        void Update()
        {
            rot += anglePerFrame;

            if (rot >= 90.0f)
            {
                anglePerFrame = -0.01f;
            }
            if (rot <= -90.0f)
            {
                anglePerFrame = 0.01f;
            }
            RenderSettings.skybox.SetFloat("_Rotation", rot);    // ‰ñ‚·
        }
    }
}