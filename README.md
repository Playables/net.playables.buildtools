# Build tools for Unity

## Features
- commandline build utility: ```Commandlinebuild.Build()```
- read android keystore passwords from MacOS keychain (set them with utils/set_android_passwords.sh)
- set iOS versioning system to apple-generic so fastlane can change the build number
- set iOS ITSAppUsesNonExemptEncryption on build
- Override values in Info.plist by creating a plist file at ```Assets/Build/iOS/Info.plist``` with the overridden values
- Integrates with fastlane: https://github.com/Playables/net.playables.buildtools-fastlane


## Usage

Download and copy to your_project/Packages/net.playables.buildtools or use this repository directly in manifest.json

```json
{
  "dependencies": {
    "net.playables.buildtools": "https://github.com/playables/net.playables.buildtools.git"
  }
}
```


## Use macOS keychain for android keystore while building
- Copy keystore to ```~/.androidkey/<filename>.keystore``` (or any other location, preferrably outside your project)
- Set keystore in Unity player settings.
- Add android keystore passwords to keychain (set_android_passwords.sh)
