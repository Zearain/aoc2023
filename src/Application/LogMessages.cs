// <copyright file="LogMessages.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;

namespace Zearain.AoC23.Application;

/// <summary>
/// Contains the log messages.
/// </summary>
public static partial class Log
{
    /// <summary>
    /// Logs that a part number was found at the given coordinates.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="partNumber">The part number.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="x">The X coordinates.</param>
    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Found part number {PartNumber} at {Y},{X}")]
    public static partial void FoundPartNumber(this ILogger logger, int partNumber, int y, int[] x);

    /// <summary>
    /// Logs that a symbol was found at the given coordinates.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="symbol">The symbol.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="x">The X coordinate.</param>
    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Found symbol {Symbol} at {Y},{X}")]
    public static partial void FoundSymbol(this ILogger logger, char symbol, int y, int x);
}