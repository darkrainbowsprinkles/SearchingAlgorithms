using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    Node node;
    TMP_Text coordinatesText;
    Image image;

    public void SetNode(Node node)
    {
        this.node = node;

        coordinatesText.text = $"{node.GetCoordinates()}";
        node.OnChange += RefreshNode;
    }

    void Awake()
    {
        coordinatesText = GetComponentInChildren<TMP_Text>();
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
    }
}
