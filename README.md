# OW-Ult-Icon-Remake
Working files for a project where I made a loading animation in Unity, mimicking the Ultimate loading animation from Overwatch as closely as possible! 

Made purely for educational purposes. If you'd like to use any of these assets (i.e. the lightning bolts), feel free; I just ask that you credit me. K2D font designed by Cadson Demak, and used under the Open Font license. 

The following must be considered if you intend to use these assets in your project:
* These items were designed for Unity's URP. Post Processing must be enabled on your camera, and HDR must be enabled in the Pipeline settings. The camera must also have a Volume component, with an override for bloom (my examples were created with a Threshold of 2 and an Intensity of 1, but I'm sure you could tweak these values to your liking).
* The ultIcon and UIMaster prefabs (Prefabs --> masterPefabs) are the only items needed to include in a scene for this project to work. Note, however, that the ultIcon prefab must be attached to a Canvas, as it utilizes a number of Canvas-specific components. 
* After making ultIcon a child of the Canvas, you will also need to plug the Canvas itself into the UIMaster via the Script component in the Inspector panel. 
* Lastly, the project makes heavy usage of layers for use with sprite masks. These layers can be imported into a project by placing TagManager.asset into your ProjectSettings folder. This <b>WILL</b> overwrite your existing layers! My suggestion if you intend to use any of the masked assets in your game is to import to a fresh project, and assign item pairs to existing layers as necessary (items are named to match with their masks; i.e. ringA's mask would be ringAMask, on layerA).
* I realize that every project has unique configurations, meaning that outside files often won't fit in tidily with existing workflows - if you would like to use these assets but find that to be the case here, please let me know exactly what issue pops up on your end, and I'll do what I can to make integration easy!
