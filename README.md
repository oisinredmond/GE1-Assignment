# GE1-Assignment
Game Engines 1 Assignment - Procederal visuals in Unity 3D based on audio input

# Project Description:
The aim of this project is to create an impressive and mesmerising visual experience through procedural generation 
of visuals. The project uses Fast Fourier Transform to analyse audio signals visually represent different frequency bands through the use of a phyllotaxis spiral with lerping trail renderers.

# Audio Analyser:
The audio input is interpreted and divided into componenet frequencies using the Fast Fourier Transform algorithm. Each frame, a sample of the audio track is put through the FFT algorithm to produce spectrum data for that black of audio. This spectrum data is then split into separate frequency bands, with each entry in the frequency band array containing the average value of its corresponding slice of spectrum data. These frequency bands are divided based on the human ear, beginning at the low end of human hearing range with "subbass" and ending near the limit with "brilliance". These frequency bands are then used in the visualiser the control and alter the visual experience.

# Trails and Phyllotaxis


# Video Demo:
[![YouTube](http://img.youtube.com/vi/ZjWBGvVb7Vg/0.jpg)](https://www.youtube.com/watch?v=ZjWBGvVb7Vg)


