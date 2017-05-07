# Cryptography.Obfuscation
Obfuscator is C# library that converts a non-negativei integer into 8 character string, generating a result similar to YouTube video id.

## Preface
Obfuscation is not designed to make your application bullet-proof by masking your query string, but rather, it simply helps by giving an additional security layer to make it harder for malicious users to inject data in the first place.

Ideally, your application should be able to authenticate requests made from user, validating whether the user can access the particular resource/operation. Put it simply, if a malicious user decides to change that id in your URL from '7' to '8' (where he doesn't own resource with id: 8), your app's security layer should be able to handle this.

## Install
```
Install-Package Kent.Cryptography.Obfuscation
```
## Usage
### Basic Example
Obfuscation strategy employed in basic example is Constant, where the same id always obfuscates to the same value.
```
  var obfuscator = new Obfuscator();
  string obfuscatedID = obfuscator.Obfuscate(15);
  
  // Reverse-process:
  int deobfuscatedID = obfusactor.Deobfuscate(obfuscatedID);
```
### Randomized Mode
You can also generate a randomized sequence even when generating obfuscation with the same id.
```
  var obfuscator = new Obfuscator();
  obfuscator.Strategy = ObfuscationStrategy.Randomized;
  string obfuscatedID = obfuscator.Obfuscate(15);
  
  // Reverse-process:
  int deobfuscatedID = obfusactor.Deobfuscate(obfuscatedID);
```

### Seed Value
Out of the box, Constant Mode always generates the same ID to string mapping for any applicaiton. This is because the Seed value remains unchanged between applications who have downloaded this library.

To change this behaviour (which you should), modify the seed value of the Obfuscator object.

```
  var obfuscator = new Obfuscator();
  obfuscator.Seed = 713;
  string obfuscatedID = obfuscator.Obfuscate(15);
```
**Note**: Higher, varying digits number is preferred for seed value as it's used in XOR operation (e.g. 317, 714, 211, etc.)a

## License
This project is licensed under the GNU Public License v3.0. Feel free to reuse for personal and commercial use.
