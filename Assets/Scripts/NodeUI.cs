using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField] TMP_Text coordinatesText;
    [SerializeField] TMP_Text valueText;
    [SerializeField] Image statusIcon;
    Node node;
    Image background;
    Button button;

    public void SetNode(Node node)
    {
        this.node = node;

        coordinatesText.text = $"{node.GetCoordinates()}";
        valueText.text = $"{node.GetHeuristicValue()}";
        node.OnChange += RefreshNode;
        RefreshNode();
    }

    void Awake()
    {
        background = GetComponent<Image>();
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
        if (node.IsStart() || node.IsGoal())
        {
            return;
        }

        node.SetIsWalkable(false);
        SetDarkColor(Color.red);
    }

    void RefreshNode()
    {
        if (!node.IsWalkable())
        {
            return;
        }

        bool isStart = node.IsStart();
        bool isGoal = node.IsGoal();

        statusIcon.enabled = isStart || isGoal;
        
        if (statusIcon.enabled)
        {
            statusIcon.color = isStart ? Color.green : Color.magenta;
        }

        if (node.IsPath())
        {
            SetLightColor(Color.yellow);
        }
        else if (node.IsExplored())
        {
            SetLightColor(Color.gray);
        }
        else
        {
            SetLightColor(Color.white);
        }
    }

    void SetLightColor(Color color)
    {
        background.color = color;
        coordinatesText.color = Color.black;
        valueText.color = Color.black;
    }
    
    void SetDarkColor(Color color)
    {
        background.color = color;
        coordinatesText.color = Color.white;
        valueText.color = Color.white;
    }
}
