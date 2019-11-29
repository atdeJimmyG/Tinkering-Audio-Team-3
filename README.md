# Tinkering-Audio-Team-3
[Link to the GitHub repository](https://github.com/atdeJimmyG/Tinkering-Audio-Team-3)

## Contract #3 - Melody Generation (Non-Diegetic Audio) - [James Gill](https://github.com/atdeJimmyG)
**You are tasked with creating a tool which procedurally generates and exports
a music track for the game. It will be important to research some rules to see
which tones and samples go well with each other to design the synthesiser. An
ability to draw upon both tones and samples is expected! The style of music
is up to you as the developer, but should fit the creativity card brief and the
style of your particular game, but is anticipated to be somewhat simplistic and
retro.**

### Requirements:
* Create procedually generated melody
* Use both tones and samples
* Export the music track 

### Instructions:
1. Make sure contract 3 scene is selected
2. In the inspector, choose how long you want your music track to be (Using Sample Duration)
3. Run the scene 
4. Tones will be generated in the savedWAVs Folder (I wasn't able to to save the whole track as one .WAV, so each tone will need to be merged in another program)

## Contract #4 - User Interface Audio (Non-Diegetic Audio) - [Thomas O'Leary](https://github.com/thomasoleary)
**You need to develop a tool to generate audio for the user interface. These
should be short tones to feed back to the player that they are successfully
navigating the user interface and configuring the game’s settings. This should
be somewhat consistent across the interface, but the tones and samples
should be modified in a systematic way to indicate success or failure. A variety
of settings should be made available and configurable by a designer.**

### Requirements
* Be able to notify the user of their actions
* Tone varies based upon success & failure
* This tool must be made available & configurable to designers

### Instructions for the Editor Tool
#### To access the Editor Window
1. Go to the 'Window' tab in Unity (Top of screen located between 'Component' and 'Help' tabs)
2. Select 'Editor Tool'
3. (Optional) Place the 'Editor Tool' window where suited

#### How to use the Editor Tool
1. Select the button you wish to change the tone of
2. Change any of the values provided within the Editor Tool
3. Click the 'Generate Tone' button
4. Click the 'Save Generated Tone' button
5. Go to folder 'Sounds' that will be located at Assets/Resources/Sounds
6. Right click within this folder and refresh - the newly saved tone will load into this folder
7. Select the button you wish to apply this tone to (if un-selected)
8. In the Editor Tool, select the 'Apply Tone to GameObject' button
9. Run the scene and click the button

### License Justification 
We have chosen to use the GNU GPL-3.0 as we felt it was best for our needs. It was decidied that we wanted out code to be open source but we also wanted people that may use this code, to indicate that they have gathered it form another source; if used, the code must remain open source under the same GLP-3.0 License. We also made sure we used a licence that made us not liable for any errors or bugs inside the code. 
