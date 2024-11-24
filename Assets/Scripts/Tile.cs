using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI textObj;
    [SerializeField] private GameEventContainer eventContainer;
    public string Characters
    {
        get
        {
            return textObj.text;
        }
    }

    private Vector3 startPosition;
    private Vector3 startScale;
    private Transform initialParent;
    [HideInInspector] public bool isDead;
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
        initialParent = transform.parent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventContainer.tileClicked.RaiseEvent(this);
        /*if (transform.position == startPosition)
        {
            TextField.instance.AttachObject(this.gameObject);
        }
        else
        {
            TextField.instance.DetachObject(this.gameObject);
        }*/
    }

    public void ResetPosition()
    {
        transform.SetParent(initialParent);
        transform.position = startPosition;
    }
    public void DestroyTile()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
