using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject circleCursor;

    Vector3 lastFramePosition;
    Vector3 dragStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentFramePosition.z = 0;

        //Debug.Log(currentFramePosition.x);

        //update the circle cursor position
        Tile tileUnderMouse = GetTileAtWorldCoord(currentFramePosition);
        if (tileUnderMouse != null)
        {
            circleCursor.SetActive(true);
            Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
            circleCursor.transform.position = cursorPosition;
        }
        else
        {
            circleCursor.SetActive(false);
        }

        //Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currentFramePosition;
        }

        //End Drag
        if (Input.GetMouseButtonUp(0))
        {
            int startX = Mathf.FloorToInt(dragStartPosition.x);
            int endX = Mathf.FloorToInt(currentFramePosition.x);

            if(endX < startX)
            {
                int temp = endX;
                endX = startX;
                startX = temp;
            }

            int startY = Mathf.FloorToInt(dragStartPosition.y);
            int endY = Mathf.FloorToInt(currentFramePosition.y);

            if (endY < startY)
            {
                int temp = endY;
                endY = startY;
                startY = temp;
            }

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if(t != null)
                    {
                        t.Type = Tile.TileType.Floor;
                    }
                }
            }
        }


        //Handle screen dragging
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) //Right or Middle mouse button
        {
            Vector3 diff = lastFramePosition - currentFramePosition;
            Camera.main.transform.Translate(diff);
        }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return WorldController.Instance.World.GetTileAt(x, y);
    }
}
