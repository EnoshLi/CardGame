using UnityEngine;

namespace Scritps.Room.MonoBehaviour
{
    public class Line : UnityEngine.MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public float offsetSpeed=0.1f;

        private void Update()
        {
            if (lineRenderer!=null)
            {
                //获取当前纹理偏移
                var offset = lineRenderer.material.mainTextureOffset;
                offset.x += offsetSpeed * Time.deltaTime;
                lineRenderer.material.mainTextureOffset = offset;
            }
        
        }
    }
}
