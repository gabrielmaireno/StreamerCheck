# StreamCheck Console App

## Overview

This console application, developed in C#, allows you to monitor a list of streamers and automatically record their live streams using Streamlink.

Note: (Currently the app is set to look just for the Art category on Twitch, if you want to change category please change inside the Twitch.cs file the "game_id=" number to the desired category. Here is how you can get the game id inteded for the category [Link](https://dev.twitch.tv/docs/api/reference/#get-games)
 
 
![Twitch API URL](https://github.com/gabrielmaireno/StreamerCheck/assets/73539365/9c5c3627-1107-40b3-a78b-1e0da708dd64)

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) - Ensure you have the .NET SDK installed on your system to build and run the application.
- [Streamlink](https://github.com/streamlink/windows-builds/releases) - Install Streamlink in your system and ensure that the CLI is properly working so the recording process execute smoothly. More information [here](https://streamlink.github.io/).

## Usage

- Before running the application, first open the streamList.txt file inside the main folder and insert the names of the streamers that you want to record (Make sure that each streamer name is written in a new line and also it's exactly the same as it's shown on the stream URL).
- Upon running the application, you will be prompted to enter your client id and client secret from the Twitch API. You can get access to those by following this [Guide](https://dev.twitch.tv/docs/authentication/register-app/).
- The application will continuously monitor the provided streamers to check if they are online.
- Once the streamer goes live, the application will automatically start recording the stream.
- Once you want to stop the stream checking, end the process of the application by pressing Ctrl+C on the terminal. After closing the application the recording will continue, so you will have to manually close all the terminal windows that were opened by the Streamlink.

## Currently supported plataforms:

-Twitch
-Picarto
-Piczel

## Credits

- Concepted by Byndum
- Developed by Gabriel Maireno and Byndum
