using UnityEngine;

/// <summary>
/// Game Bounds determines the edges of the screen based on the orthographic camera.
/// There are additional helper methods that assist in determining if game objects
/// are in bounds or spawning objects on the bounds.
/// </summary>
public class GameBounds
{
    public GameBounds()
    {
        
    }
    
    public GameBounds(Camera cam, float boundsOffset)
    {
        if (!cam.orthographic)
        {
            Debug.LogError("Camera has not been set to orthographic");
        }
        
        float screenHalfHeight = cam.orthographicSize;
        float screenHalfWidth = cam.orthographicSize * cam.aspect;
        
        _top = cam.transform.position.y + screenHalfHeight + boundsOffset;
        _bottom = cam.transform.position.y - screenHalfHeight - boundsOffset;
        _right = cam.transform.position.x + screenHalfWidth + boundsOffset;
        _left = cam.transform.position.x - screenHalfWidth - boundsOffset;
    }

    private float _top = 0f;
    private float _bottom = 0f;
    private float _right = 0f;
    private float _left = 0f;

    public bool OutOfBounds(Vector2 position)
    {
        return position.x < _left ||
               position.x > _right ||
               position.y < _bottom ||
               position.y > _top;
    }
    
    public float DistanceFromEdge(Vector2 position)
    {
        float distLeft = position.x - _left;
        float distRight = _right - position.x;
        float distBottom = position.y - _bottom;
        float distTop = _top - position.y;

        return Mathf.Min(distLeft, distRight, distBottom, distTop);
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

    public Vector2 GetRandomBoundsPosition(float padding = 0f)
    {
        int side = UnityEngine.Random.Range(0, 4);

        switch (side)
        {
            // Top
            case 0:
                return new Vector2
                (
                    UnityEngine.Random.Range(_left, _right), 
                    _top + padding
                );

            // Bottom
            case 1:
                return new Vector2
                (
                    UnityEngine.Random.Range(_left, _right),
                    _bottom - padding
                );

            // Left
            case 2:
                return new Vector2
                (
                    _left - padding,
                    UnityEngine.Random.Range(_bottom, _top)
                );

            // Right
            default:
                return new Vector2
                (
                    _right + padding,
                    UnityEngine.Random.Range(_bottom, _top)
                );
        }
    }
}