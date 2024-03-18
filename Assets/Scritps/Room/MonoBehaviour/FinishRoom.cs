namespace Scritps.Room.MonoBehaviour
{
    public class FinishRoom : UnityEngine.MonoBehaviour
    {
        public ObjectEventSO loadMapEvent;
        private void OnMouseDown()
        {
            //返回地图
            loadMapEvent.RaseEvent(null,this);
        }
    
    }
}
