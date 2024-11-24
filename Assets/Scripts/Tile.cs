using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
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

    public void OnClick()
    {
        eventContainer.tileClicked.RaiseEvent(this);
    }

    public void ResetPosition()
    {
        transform.SetParent(initialParent);
        transform.position = startPosition;
    }
    public void DestroyTile()
    {
        isDead = true;
        eventContainer.tileDestroyed.RaiseEvent(this);
        Destroy(gameObject);
    }
}
