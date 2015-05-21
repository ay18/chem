# Chem - a 3D molecule viewer
*2015 Project for CS476 (Emerging Platforms)  
by Stanley Chan and Andy Yee*  

![chem gif](https://github.com/sksea/chem/raw/master/chem.gif)

### Description
*Chem* is our end-of-semester project for a class whose overarching theme was to explore untraditional platforms (read non-desktop). This project incorporates Unity 5 and Leap Motion to create an app which parses [.sdf files](http://en.wikipedia.org/wiki/Chemical_table_file) and presents those molecules in 3D. The user can then interact with the molecule using hand gestures via Leap Motion.

### Known Bugs
Due to time constraints, the following are known issues:
* **Project works in dev/preview mode for unity, but not after being built.**   
   *Cause: Unity apps don't have permission to read from disk after being built. Unity allows loading in text assets, but considering .sdf is not one of the supported formats, our files might need to be coverted to .txt first.*
* **Only horizontal rotations of molecules is allowed, as opposed to rotating the molecule in 3 axes.**   
   *Cause: Bad camera implementation our part. We have it so the molecules are stationary with "orbiting" camera. Unfortunately, we couldn't figure out how to make the camera object travel on a spherical path.*
