using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackgound : MonoBehaviour
{
    [SerializeField]
    private RawImage _image;

    [SerializeField]
    private float _x,
        _y;

    // Update is called once per frame
    void Update()
    {
        _image.uvRect = new Rect(
            _image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime,
            _image.uvRect.size
        );
    }

    public void SetScrollSpeed(float newY)
    {
        _y = newY;
    }

    public float GetScrollSpeed()
    {
        return _y;
    }
}
