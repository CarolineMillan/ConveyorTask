# ConveyorTask

A modular conveyor system built in Unity.

Choose a conveyor from the menu and click the floor to place it. Conveyors placed end-to-end snap into a continuous chain. Pressing the spacebar spawns a product at the beginning of each chain. When the Running toggle is on, the products will move along the chain of conveyors.

## Features

 - Rotate a conveyor before placing it by pressing `r`. Rotated conveyors can still snap and be part of a continuous chain.
 - Four conveyors: generic, short, incline up and incline down
 - Two incline conveyors with different directions, one moves the products up the conveyor and the other moves the products down, with products tilting to match the slope. They are mirror images of each other so they can snap together to form a flat continuous chain on the floor
 - Pressing space multiple times alternates between products
 - Can have several separate chains of conveyors at once, and `space` spawns a product at the beginning of each of them
 - Products are destroyed when they reach the end of a chain
 - Products rotate around corners to follow the direction of the conveyor
 - A toggle to start/stop running the conveyors
 - Clicking the menu doesn't place a conveyor on the floor, even if the menu is overlapping the floor

## Controls

To select a conveyor, click the button on the sidebar with the name of the conveyor you would like to place.

Click on the floor to place the conveyor.

Click near the end of the conveyor to snap another conveyor onto it, forming a chain.

Press `r` after selecting but before placing a conveyor to rotate it 90 degrees.

Press `space` to spawn a product on the first conveyor in each chain.

Press the toggle next to 'Conveyors Running' to toggle the product movement along the conveyor on/off. By default the toggle is on.

## How to run

 - Unzip the folder
 - Run ConveyorTask.exe

## Dev Tools

 - Unity 6000.6.10f1
 - C#
 - Claude was used as a tutor/code reviewer to learn Unity and C#
 - VSCode / neovim
 - Git/GitHub and LFS
 - Built on macOS

## Known limitations

 - Rotated conveyors overlap slightly at the corners when snapped.
 - Floor is a single level/plane. Ideally I would have added the option for floor boxes/blocks to raise the floor to connect generic/short conveyors to both ends of an incline conveyor.
 - Conveyors must be snapped in order. You can only snap a conveyor to the end of the chain. Ideally I would have added the ability to snap to either end of the chain, and even allow branching at points with rotated conveyors.
 - A chain of incline down conveyors snaps together to go through the floor. Similarly, incline up conveyors snap together to raise off the floor. 
 - Windows build is untested. I couldn't get access to a Windows machine or set up a VM within the timeframe, and built this project on macOS. The macOS build has been tested.

[GitHub link](https://github.com/CarolineMillan/ConveyorTask)

