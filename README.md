# Build tools for Unity

## Features
- commandline build utility: ```Commandlinebuild.Build()```
- Fastlane actions: unity_build, upload_to_steam (in utils/fastlane-actions)
- read android keystore passwords from MacOS keychain (set them with utils/set_android_passwords.sh)
- set iOS versioning system to apple-generic so fastlane can change the build number
- set iOS ITSAppUsesNonExemptEncryption on build


## Usage

Download and copy to your_project/Packages/net.playables.buildtools or use this repository directly in manifest.json

```json
{
  "dependencies": {
    "net.playables.buildtools": "https://github.com/anyuser/net.playables.buildtools.git"
  }
}
```
