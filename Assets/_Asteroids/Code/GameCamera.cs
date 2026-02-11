using System;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public CameraBounds Bounds => _cameraBounds;
    
    private CameraBounds _cameraBounds;
    
    private void Awake()
    {
        SetCameraBounds();
    }

    private void SetCameraBounds()
    {
        Camera cam = Camera.main;

        float screenHalfHeight = cam.orthographicSize;
        float screenHalfWidth = cam.orthographicSize * cam.aspect;
        
        float top = cam.transform.position.y + screenHalfHeight;
        float bottom = cam.transform.position.y - screenHalfHeight;
        float right = cam.transform.position.x + screenHalfWidth;
        float left = cam.transform.position.x - screenHalfWidth;

        _cameraBounds = new CameraBounds(top, bottom, right, left);
    }

    public struct CameraBounds
    {
        public CameraBounds(float top, float bottom, float right, float left)
        {
            _top = top;
            _bottom = bottom;
            _right = right;
            _left = left;
        }

        private float _top;
        private float _bottom;
        private float _right;
        private float _left;

        public bool OutOfBounds(Vector2 position)
        {
            return position.x < _left ||
                    position.x > _right ||
                    position.y < _bottom ||
                    position.y > _top;
        }
        
        public bool WrapPosition(Vector2 position, out Vector2 wrappedPosition)
        {
            bool wrapped = false;
            wrappedPosition = position;
            
            if (position.x < _left)
            {
                wrappedPosition.x = _right;
                wrapped = true;
            }
            else if (position.x > _right)
            {
                wrappedPosition.x = _left;
                wrapped = true;
            }

            if (position.y < _bottom)
            {
                wrappedPosition.y = _top;
                wrapped = true;
            }
            else if (position.y > _top)
            {
                wrappedPosition.y = _bottom;
                wrapped = true;
            }

            return wrapped;
        }
    }
}
