using UnityEngine;

namespace DollarGame.Features.Graph.Logic
{
    public class IngameMathHelperBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject nodeA;
        [SerializeField] private GameObject nodeB;
        [SerializeField] private Vector3 axis;

        public void CalculateMiddlePosAndAngle()
        {
            Vector2 connection = nodeA.transform.TransformDirection(nodeB.transform.localPosition);
            
            Debug.Log("Angle: " + Vector2.Angle(connection, axis));
            Debug.Log("MiddlePosX: " + connection.x / 2 + " MiddlePosY" + connection.y / 2);
        }

        public void CalculateLocalDistance()
        {
            float localDistance = Vector2.Distance(nodeA.transform.localPosition, nodeB.transform.localPosition);
            Debug.Log("LocalDistance: " + localDistance);
        }

        public void CalculateDistance()
        {
            float distance = Vector2.Distance(nodeA.transform.position, nodeB.transform.position);
            Debug.Log("Distance: " + distance);
        }

        public void CalculateLocalAngle()
        {
            float localAngle = Vector3.Angle(nodeA.transform.localPosition, nodeB.transform.localPosition);
            Debug.Log("Angle: " + localAngle);
        }

        public void CalculateAngle()
        {
            float angle = Vector3.Angle(nodeA.transform.position, nodeB.transform.position);
            Debug.Log("Angle: " + angle);
        }

        public void CalculateLocalMiddlePosition()
        {
            Vector3 position = nodeA.transform.localPosition;
            Vector3 position1 = nodeB.transform.localPosition;
            float middlePosX = position.x + (position.x - position1.x) / 2;
            float middlePosY = position.y + (position.y - position1.y) / 2;
            Vector3 localMiddlePos = new Vector3(middlePosX, middlePosY, 0);
            Debug.Log("LocalMiddlePos: " + localMiddlePos);
        }

        public void CalculateMiddlePosition()
        {
            Vector3 position = nodeA.transform.position;
            Vector3 position1 = nodeB.transform.position;
            float middlePosX = position.x + (position.x - position1.x) / 2;
            float middlePosY = position.y + (position.y - position1.y) / 2;
            Vector3 middlePos = new Vector3(middlePosX, middlePosY, 0);
            Debug.Log("MiddlePos: " + middlePos);
        }
    }
}
