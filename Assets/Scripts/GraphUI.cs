using UnityEngine;
using UnityEngine.UI;

public class GraphUI : MonoBehaviour
{
    [SerializeField] NodeUI nodePrefab;
    [SerializeField] Transform nodesContainer;
    [SerializeField] Button startSearchButton;
    Graph graph;

    void Awake()
    {
        graph = FindObjectOfType<Graph>();
    }

    void OnEnable()
    {
        startSearchButton.onClick.AddListener(StartSearch);
    }

    void OnDisable()
    {
        startSearchButton.onClick.RemoveListener(StartSearch);
    }

    void Start()
    {
        foreach (Node node in graph.GetNodes())
        {
            NodeUI nodeInstance = Instantiate(nodePrefab, nodesContainer);
            nodeInstance.SetNode(node);
        }
    }

    void StartSearch()
    {
        graph.StartSearch();
    }
}
