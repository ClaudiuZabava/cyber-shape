using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairImg;

    private void Start()
    {
        var hotSpot = new Vector2(crosshairImg.width / 2f, crosshairImg.height / 2f);
        Cursor.SetCursor(crosshairImg, hotSpot, CursorMode.Auto);
    }
}
