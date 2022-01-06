using UnityEngine;
using Baby_vs_Aliens.Tools;

namespace Baby_vs_Aliens
{
    public class InputController : BaseController, IUpdateableRegular
    {
        public readonly SubscriptionProperty<Vector3> MovementVector;
        public readonly SubscriptionProperty<Vector3> MousePosition;

        public InputController()
        {
            MovementVector = new SubscriptionProperty<Vector3>();
            MousePosition = new SubscriptionProperty<Vector3>();
        }

        public void UpdateRegular()
        {
            CheckForMoveInput();
            GetMousePosition();
        }

        private void CheckForMoveInput()
        {
            MovementVector.Value = new Vector3 { x = Input.GetAxisRaw(References.INPUT_AXIS_HORIZONTAL),
                                                y = 0,
                                                z = Input.GetAxisRaw(References.INPUT_AXIS_VERTICAL)};
        }

        public void GetMousePosition()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Floor")))
            {
                var hitPoint = hit.point;
                hitPoint.y = 0;
                MousePosition.Value = hitPoint;
            }
        }
    }
}