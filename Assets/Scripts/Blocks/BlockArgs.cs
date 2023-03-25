using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockArgs : EventArgs
{
    public PlayerController PlayerController { get; set; }
    public Block Block { get; set; }

    public bool PressCondition = false;

    public BlockArgs(PlayerController playerController, Block block)
    {
        PlayerController = playerController;
        Block = block;
    }
    // pressCondition called when player press the SecondKey Input
    public BlockArgs(PlayerController playerController, Block block, bool pressCondition)
    {
        PlayerController = playerController;
        Block = block;
        PressCondition = pressCondition;
    }
}
