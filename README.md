# 3D Weather Station
The project is runnable on GitHub Pages on https://tezcavv.github.io/3D-Weather-Station/


## Project Setup
- The project is built on Unity LTS 6000.3.7f1
- To open and run the project trought Unity:
  - Download/Clone the project
  - Ensure Unity Hub and the latest Unity LTS version are installed (Unity Hub will prompt to install the right one if missing)
  - open the folder trought "Add" from Unity Hub
  - Open the scene asset "Initializer Scene"
  - enter Play Mode
  - Randomize currently allows for 3 weather conditions: Clear, Rain, Snow

## External Libraries / Packages
 - UniTask
 - Vcontainer
 - Newtonsoft Json / Json.net

## Known Bug
 - In the GitHub Pages Build swapping view doesn´t automatically update/spawn some objects until a new Weather call is made
   - any Weather button should correctly trigger the missed update/spawn
   
