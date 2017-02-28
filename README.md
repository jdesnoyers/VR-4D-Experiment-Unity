# VR-4D-Experiment-Unity
An experiment in four dimensional manipulation in Unity for Gear VR

Several 3D objects and one 4D tesseract that can be manipulated in 6 axes of rotation (WX,WY,WZ,XY,XZ,YZ). Second room contains a "perpendicular" 3D space with coordinates WYZ - The objects have a counterpart in this space (aka this is where their "matter" goes to when rotated out of XYZ coordinates.

Framerate is low on GearVR at 30fps - bearable but be careful about moving through space and rotating your head simultaneously.

All rotations applied directly to mesh (due to implications of rotating in four dimensions), 4D coordinates stored in Vector4 arrays.

Requires Oculus Utilities for Unity (OVR Folder) - some default controls will conflict 
https://developer3.oculus.com/downloads/
