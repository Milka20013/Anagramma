using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI textObj;
    public string Characters
    {
        get
        {
            return textObj.text;
        }
    }

    private Vector3 startPosition;
    private Vector3 startScale;
    public void InitTile(string characters)
    {
        if (string.IsNullOrEmpty(characters))
        {
            characters = "a";
        }
        textObj.text = characters;

    }
    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(textObj.text);
        /*if (transform.position == startPosition)
        {
            TextField.instance.AttachObject(this.gameObject);
        }
        else
        {
            TextField.instance.DetachObject(this.gameObject);
        }*/
    }
}
