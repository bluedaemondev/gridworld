using UnityEngine;

namespace Player
{
    public class PlayerMovementWRigidbody : MonoBehaviour
    {
        public string rotationAxis = "Horizontal";
        public float rotationSpeed = 90f;
        public string movementAxis = "Vertical";
        public float movementSpeed = 2f;
        public Rigidbody myRigidbody;
        private float rotationInput;
        private float movementInput;
        public ForceMode movementForceMode = ForceMode.Force;

        void Update()
        {
            rotationInput = Input.GetAxis(rotationAxis);
            movementInput = Input.GetAxis(movementAxis);
        }

        private void FixedUpdate()
        {
            myRigidbody.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationSpeed * rotationInput * Time.deltaTime, 0)));

            myRigidbody.AddForce(transform.forward * movementSpeed * movementInput, movementForceMode);
            Debug.Log(myRigidbody.velocity.magnitude);
        }
    }
}