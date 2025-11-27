using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphUI : MonoBehaviour
{
    [SerializeField] NodeUI nodePrefab;
    [SerializeField] Transform nodesContainer;
    [SerializeField] Button startSearchButton;
    [SerializeField] Button clearObstaclesButton;
    [SerializeField] Button resetGraphButton;
    [SerializeField] TMP_Dropdown searchTypeDropdown;
    [SerializeField] TMP_Dropdown nodeSelectionDropdown;
    [SerializeField] TMP_Text visitedText;
    [SerializeField] TMP_Text pathText;
    Graph graph;
    const string listDefaultText = "No search done yet.";

    enum NodeSelection
    {
        Start, Goal, Obstacle
    }

    void Awake()
    {
        graph = FindObjectOfType<Graph>();
    }

    void OnEnable()
    {
        startSearchButton.onClick.AddListener(StartSearch);
        clearObstaclesButton.onClick.AddListener(graph.ClearObstacles);
        resetGraphButton.onClick.AddListener(ResetGraph);
    }

    void OnDisable()
    {
        startSearchButton.onClick.RemoveListener(StartSearch);
        clearObstaclesButton.onClick.RemoveListener(graph.ClearObstacles);
        resetGraphButton.onClick.RemoveListener(ResetGraph);
    }

    void Start()
    {
        FillGrid();
        FillDropdown(searchTypeDropdown, typeof(SearchType));
        FillDropdown(nodeSelectionDropdown, typeof(NodeSelection));
        SetDefaultLists();
    }

    void ResetGraph()
    {
        SetDefaultLists();
        graph.ResetGraph();
    }

    void SetDefaultLists()
    {
        visitedText.text = listDefaultText;
        pathText.text = listDefaultText;
    }

    void FillGrid()
    {
        foreach (Node node in graph.GetNodes())
        {
            NodeUI nodeInstance = Instantiate(nodePrefab, nodesContainer);
            nodeInstance.SetNode(node);
            nodeInstance.OnNodeSelected = OnNodeSelected;
        }
    }

    void FillDropdown(TMP_Dropdown dropdown, Type type)
    {
        string[] types = Enum.GetNames(type);
        dropdown.ClearOptions();
        dropdown.AddOptions(types.ToList());
    }

    void OnNodeSelected(Node node)
    {
        NodeSelection nodeSelection = (NodeSelection)nodeSelectionDropdown.value;

        switch (nodeSelection)
        {
            case NodeSelection.Start:
                graph.SetStartNode(node);
                break;

            case NodeSelection.Goal:
                graph.SetGoalNode(node);
                break;

            case NodeSelection.Obstacle:
                graph.SetObstacleNode(node);
                break;
        }
    }

    void StartSearch()
    {
        int selectedIndex = searchTypeDropdown.value;
        SearchType searchType = (SearchType)selectedIndex;
        List<Node> path = graph.StartSearch(searchType);
        PrintNodes(graph.GetVisited(), ",", visitedText);
        PrintNodes(path, "->", pathText);
    }

    void PrintNodes(IEnumerable<Node> nodes, string separator, TMP_Text uiText)
    {
        uiText.text = "";
        uiText.text += "[";

        Node[] nodesArray = nodes.ToArray();

        for (int i = 0; i < nodesArray.Length; i++)
        {
            uiText.text += nodesArray[i].GetCoordinates();

            if (i < nodesArray.Length - 1)
            {
                uiText.text += separator;
            }
        }
        
        uiText.text += "]";
    }
}
