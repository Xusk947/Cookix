using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockArgs : EventArgs
{
    public PlayerController PlayerController { get; set; }
    public Block Block { get; set; }

    public BlockArgs(PlayerController playerController, Block block)
    {
        PlayerController = playerController;
        Block = block;
    }
}
