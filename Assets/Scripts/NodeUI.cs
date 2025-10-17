using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField] TMP_Text coordinatesText;
    [SerializeField] TMP_Text valueText;
    Node node;
    Image image;

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
    }

    void RefreshNode()
    {
        if (node.IsExplored())
        {
            image.color = Color.gray;
        }

        if (node.IsPath())
        {
            image.color = Color.yellow;
        }
        
        if (!node.IsExplored() && !node.IsPath())
        {
            image.color = Color.white;
        }
    }
}
