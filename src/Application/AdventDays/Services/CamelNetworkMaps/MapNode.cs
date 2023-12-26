// <copyright file="MapNode.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Zearain.AoC23.Application.AdventDays.Services.CamelNetworkMaps;

public class MapNode
{
    public MapNode(string nodeLabel)
    {
        this.NodeLabel = nodeLabel;
    }

    public string NodeLabel { get;}

    public MapNode Left { get; set; }

    public MapNode Right { get; set; }

    public int Cost => 1;
}