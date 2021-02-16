# About

This branch is related to the following Google Groups post.

https://groups.google.com/g/google-admob-ads-sdk/c/9fi29_H2Y6o

# App Id and Ad Unit Id

These are all sample IDs.

* App Id: ca-app-pub-3940256099942544~3347511713
* BannerId: ca-app-pub-3940256099942544/2934735716
* InterstitialId: ca-app-pub-3940256099942544/4411468910
* RewardId: ca-app-pub-3940256099942544/1712485313

# Steps to reproduce the issue

1. Checkout develop branch
2. Build iOS App and run on iPhone
3. Press the "Play Short Sound" button and make sure the sound is heard.
4. Press "Show Reward" button.
5. Press the "Play Short Sound" button and make sure the sound is not heard.

If you comment out the following call in VibrationManager.cs#Awake, you will see that the sound is heard.

`VibrationUtil.SetupHapticEngine();`
