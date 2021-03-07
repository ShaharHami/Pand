using UnityEngine;

namespace Utils
{
    public enum Placement
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT
    }
    // Used to place objects on screen edges
    public class Placer : MonoBehaviour
    {
        [SerializeField] private Placement placement;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        void Start()
        {
            Vector2 pos = Vector2.zero;
            Vector2 bounds = Utils.ScreenBounds(cam);
            switch (placement)
            {
                case Placement.TOP:
                    pos.y = bounds.y + transform.localScale.y / 2;
                    break;
                case Placement.BOTTOM:
                    pos.y = -bounds.y - transform.localScale.y / 2;
                    break;
                case Placement.LEFT:
                    pos.x = -bounds.x - transform.localScale.x / 2;
                    break;
                case Placement.RIGHT:
                    pos.x = bounds.x + transform.localScale.x / 2;
                    break;
            }

            transform.position = pos;
        }
    }
}
