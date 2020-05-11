using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour {

    // Know what objects are clickable
    public LayerMask clickableLayer;

    // Swappable cursor images
    public Texture2D pointer; // normal texture
    public Texture2D target; // target texture
    public Texture2D doorway; // doorway texture
    public Texture2D combat; // actionable texture

    public EventVector3 OnClickableEnvironment;
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value)) {

            bool isDoor = false;
            bool isItem = false;

            if (hit.collider.gameObject.tag.ToLower().Equals("doorway")) {

                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                isDoor = true;

            } else if (hit.collider.gameObject.tag.ToLower().Equals("item")) {

                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                isItem = true;

            } else {

                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0)) {

                if (isDoor) {

                    Transform doorwayTransform = hit.collider.gameObject.transform;

                    OnClickableEnvironment.Invoke(doorwayTransform.position);

                    Debug.Log("DOOR!");

                } else if (isItem) {

                    Transform itemTransform = hit.collider.gameObject.transform;

                    OnClickableEnvironment.Invoke(itemTransform.position);

                    Debug.Log("ITEM!");

                } else {

                    OnClickableEnvironment.Invoke(hit.point);
                }
            }

        } else {

            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
	}
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
