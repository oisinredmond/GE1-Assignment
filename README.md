# GE1-Assignment
Game Engines 1 Assignment - Procederal visuals in Unity 3D based on audio input

# Project Description:
The aim of this project is to create an impressive and mesmerising visual experience through procedural generation 
of visuals. The project uses Fast Fourier Transform to analyse audio signals visually represent different frequency bands through the use of a phyllotaxis spiral with lerping trail renderers.

# Audio Analyser:
The audio input is interpreted and divided into componenet frequencies using the Fast Fourier Transform algorithm. Each frame, a sample of the audio track is put through the FFT algorithm to produce spectrum data for that black of audio. This spectrum data is then split into separate frequency bands, with each entry in the frequency band array containing the average value of its corresponding slice of spectrum data. These frequency bands are divided based on the human ear, beginning at the low end of human hearing range with "subbass" and ending near the limit with "brilliance". These frequency bands are then used in the visualiser the control and alter the visual experience.

# Phyllotaxis
The main visual component of this project is the use of a phyllotaxis formula to map points on the screen to and from which trail renderers
can lerp to produce interesting visuals. The formula used to create a phyllotaxis spiral requires three parameters: **n**, the current iteration, **theta**, the angle that each point will increase by, and **scale**, which is a constant value used to scale the radius.

The angle from the center of the spiral for each point is calculated using **n X theta**. The radius is calculated using **scale X sqrt(n)**. These values can be converted into **x** and **y** coordinates for use in the unity engine by multiplying the radius by Cos and Sin of the angle.

# Video Demo:
[![YouTube](http://img.youtube.com/vi/ZjWBGvVb7Vg/0.jpg)](https://www.youtube.com/watch?v=ZjWBGvVb7Vg)


