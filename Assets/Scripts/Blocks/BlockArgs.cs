using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Special Event when player interact with it
/// </summary>
public class BlockArgs : EventArgs
{
    /// <summary>
    /// a player who touch the block
    /// </summary>
    public ChefController ChefController { get; set; }
    /// <summary>
    /// block which was touched by a Player
    /// </summary>
    public Block Block { get; set; }
    /// <summary>
    /// called when player press the SecondKey input
    /// </summary>
    public bool PressCondition = false;

    public BlockArgs(ChefController playerController, Block block)
    {
        ChefController = playerController;
        Block = block;
    }

    public BlockArgs(ChefController playerController, Block block, bool pressCondition)
    {
        ChefController = playerController;
        Block = block;
        PressCondition = pressCondition;
    }
}
