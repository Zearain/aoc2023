// <copyright file="CamelNetworkMap.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Zearain.AoC23.Domain.AdventDayAggregate.ValueObjects;

namespace Zearain.AoC23.Application.AdventDays.Services.CamelNetworkMaps;

public class CamelNetworkMap
{
    private readonly Dictionary<string, MapNode> _nodes = new();

    private char[] instructions;

    private CamelNetworkMap(string instructions)
    {
        this.instructions = instructions.ToCharArray();
    }

    public static CamelNetworkMap Read(DayInput input)
    {
        var sectionLines = input.SectionLines;

        var map = new CamelNetworkMap(sectionLines[0][0]);

        foreach (string line in sectionLines[1])
        {
            string nodeLabel = line.Split('=')[0].Trim();
            map.AddNode(nodeLabel);
        }

        foreach (string line in sectionLines[1])
        {
            string[] parts = line.Split('=');
            string nodeLabel = line.Split('=')[0].Trim();
            string[] neighbors = parts[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(',');
            string left = neighbors[0].Trim();
            string right = neighbors[1].Trim();

            map.AddEdge(nodeLabel, left, right);
        }

        return map;
    }

    public void AddNode(string nodeLabel)
    {
        if (this._nodes.ContainsKey(nodeLabel))
        {
            return;
        }

        this._nodes.Add(nodeLabel, new MapNode(nodeLabel));
    }

    public void AddEdge(string nodeLabel, string leftNodeLabel, string rightNodeLabel)
    {
        this._nodes[nodeLabel].Left = this._nodes[leftNodeLabel];
        this._nodes[nodeLabel].Right = this._nodes[rightNodeLabel];
    }

    public int GetCostOfInstructions()
    {
        var totalCost = 0;
        var currentNode = this._nodes["AAA"];

        var i = 0;
        while (currentNode.NodeLabel != "ZZZ")
        {
            var instruction = this.instructions[i];
            currentNode = instruction switch
            {
                'L' => currentNode.Left,
                'R' => currentNode.Right,
                _ => throw new NotImplementedException(),
            };

            totalCost += currentNode.Cost;

            if (i == this.instructions.Length - 1)
            {
                i = 0;
                continue;
            }

            i++;
        }

        return totalCost;
    }

    public int GetCostOfSimultanousInstructions()
    {
        var steps = 0;
        var currentNodes = new List<MapNode>(this._nodes.Where(n => n.Key.EndsWith('A')).Select(n => n.Value));

        var i = 0;
        while (currentNodes.All(n => n.NodeLabel.EndsWith('Z')) == false)
        {
            var instruction = this.instructions[i];
            currentNodes = instruction switch
            {
                'L' => currentNodes.Select(n => n.Left).ToList(),
                'R' => currentNodes.Select(n => n.Right).ToList(),
                _ => throw new NotImplementedException(),
            };

            steps++;

            if (i == this.instructions.Length - 1)
            {
                i = 0;
                continue;
            }

            i++;
        }

        return steps;
    }
}