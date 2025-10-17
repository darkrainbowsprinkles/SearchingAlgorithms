using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField] TMP_Text coordinatesText;
    [SerializeField] TMP_Text valueText;
    Node node;
    Image image;
    Button button;

    public void SetNode(Node node)
    {
        this.node = node;

        coordinatesText.text = $"{node.GetCoordinates()}";
        valueText.text = $"{node.GetValue()}";
        node.OnChange += RefreshNode;
    }

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(SetNonWalkable);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(SetNonWalkable);
    }

    void SetNonWalkable()
    {
        node.SetIsWalkable(false);
        SetDarkColor(Color.red);
    }

    void RefreshNode()
    {
        if (!node.IsWalkable())
        {
            return;
        }

        if (node.IsExplored())
        {
            SetLightColor(Color.gray);
        }

        if (node.IsPath())
        {
            SetLightColor(Color.yellow);
        }

        if (!node.IsExplored() && !node.IsPath())
        {
            SetLightColor(Color.white);
        }
    }

    void SetLightColor(Color color)
    {
        image.color = color;
        coordinatesText.color = Color.black;
        valueText.color = Color.black;
    }
    
    void SetDarkColor(Color color)
    {
        image.color = color;
        coordinatesText.color = Color.white;
        valueText.color = Color.white;
    }
}
