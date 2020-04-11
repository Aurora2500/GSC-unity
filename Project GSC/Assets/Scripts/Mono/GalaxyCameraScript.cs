using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Mono
{
    public class GalaxyCameraScript : MonoBehaviour
    {
        public float defaultSpeed = 20f;
        public float shiftSpeed = 50f;
        public float mouseSensitivity = 10f;
        public float zoomSensitivity = 10f;

        public Vector2 panBorder;
        Vector3 initialPos;
        public float minZoom = -30f;
        public float maxZoom = -10f;

        Vector2 mousePress;
        Vector3 mousePressPos;
        float zoomAmount;

        bool isMousePressed;


        public float outAngle = -10;
        public float inAngle = -45;
        float zoomOffset;

        public static bool locked = false;

        void Start()
        {
            initialPos = transform.position;
        }


        // Update is called once per frame
        void Update()
        {
            if (locked)
            {
                return;
            }
            Vector3 pos = transform.position;
            pos.y += zoomOffset;

            // mouse pan
            if (Input.GetMouseButtonDown(1) && !IsMouseOverObject())
            {
                mousePress = Input.mousePosition;
                mousePressPos = transform.position;
                isMousePressed = true;
            }
            if (Input.GetMouseButtonUp(1) && isMousePressed)
            {
                isMousePressed = false;
            }

            if (isMousePressed)
            {
                Vector2 currentMouse = Input.mousePosition;

                var newPos = (Vector2)mousePressPos + (mousePress - currentMouse) / mouseSensitivity * zoomAmount;
                pos.x = newPos.x;
                pos.y = newPos.y + zoomOffset;
            }
            else
            {
                // keyboard pan

                if (Input.GetKey("w"))
                {
                    pos.y += Time.deltaTime * (Input.GetKey("left shift") ? shiftSpeed : defaultSpeed);
                }
                if (Input.GetKey("s"))
                {
                    pos.y -= Time.deltaTime * (Input.GetKey("left shift") ? shiftSpeed : defaultSpeed);
                }
                if (Input.GetKey("a"))
                {
                    pos.x -= Time.deltaTime * (Input.GetKey("left shift") ? shiftSpeed : defaultSpeed);
                }
                if (Input.GetKey("d"))
                {
                    pos.x += Time.deltaTime * (Input.GetKey("left shift") ? shiftSpeed : defaultSpeed);
                }
            }

            #region scroll and zoom speed penalty

            if (IsMouseOverObject()) return;

            zoomAmount = pos.z / minZoom;

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.z += scroll * zoomSensitivity;

            //rotation
            var rot = transform.rotation.eulerAngles;
            var zoomAmountPercent = -maxZoom / (minZoom - maxZoom) + 1f / (minZoom - maxZoom) * zoomAmount * minZoom;
            var dAngle = inAngle - outAngle;
            rot.x = inAngle - Mathf.Sqrt(zoomAmountPercent) * dAngle;

            pos = Limit(pos);

            //offset
            zoomOffset = 1f / zoomAmount;

            pos.y -= zoomOffset;

            transform.position = pos;
            transform.eulerAngles = rot;
            #endregion
        }

        Vector3 Limit(Vector3 input)
        {
            input.x = Mathf.Clamp(input.x, initialPos.x - panBorder.x, initialPos.x + panBorder.x);
            input.y = Mathf.Clamp(input.y, initialPos.y - panBorder.y, initialPos.y + panBorder.y);
            input.z = Mathf.Clamp(input.z, minZoom, maxZoom);

            return input;
        }

        bool IsMouseOverObject()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}