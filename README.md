# Cryptography.Obfuscation
Obfuscator is C# library that converts a non-negative integer into 8 character string, generating a result similar to YouTube video id.

Preface
------
Obfuscation is not designed to replace your authentication layer by masking your query string, but rather, it simply helps by giving an additional security layer to make it harder for malicious users to inject data in the first place.

Ideally, your application should be able to authenticate requests made from user, validating whether the user can access the particular resource/operation. Put it simply, if a malicious user decides to change that id in your URL from '7' to '8' (where he doesn't own resource with id: 8), your app's security layer should be able to handle this.

Install
------
Go to Package Manager Console and run the following nuget command.
```
Install-Package Kent.Cryptography.Obfuscation
```
## Usage
### Basic Example
Obfuscation strategy employed by default is ObfuscationStrategy.Constant, where the same id always obfuscates to the same string.
```
  var obfuscator = new Obfuscator();
  string obfuscatedID = obfuscator.Obfuscate(15);
  
  // Reverse-process:
  int deobfuscatedID = obfusactor.Deobfuscate(obfuscatedID);
```
### Randomized Mode
You can also generate a randomized sequence, which will cause the obfuscate function to generate 'randomized' sequence for the same id, while still giving you capability to convert this back to numeric id.
```
  var obfuscator = new Obfuscator();
  obfuscator.Strategy = ObfuscationStrategy.Randomized;
  string obfuscatedID = obfuscator.Obfuscate(15);
  
  // Reverse-process:
  int deobfuscatedID = obfusactor.Deobfuscate(obfuscatedID);
```

### Seed Value
Out of the box, Constant Mode always generates the same ID <> string mapping for any application which uses this library. To strengthen your app's security, modify the Seed value to generate unique id <> string mapping for your application. 

```
  var obfuscator = new Obfuscator();
  obfuscator.Seed = 713;
  string obfuscatedID = obfuscator.Obfuscate(15);
```
**Note**: Higher, varying digits number is preferred for seed value as it's used in XOR operation (e.g. 317, 714, 211, etc). Also, consider storing this value in your web.config file.

License
------
This project is licensed under the GNU Public License v3.0. Feel free to reuse for personal and commercial use.
