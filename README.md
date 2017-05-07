# Cryptography.Obfuscation
Obfuscator is C# library that converts a non-negative integer into 8 character string, generating a result similar to YouTube video id.
Put it simply, this library converts a numeric id such as 12 to xVrAndNb and back.

Preface
------
Obfuscation is not designed to to replace your authentication layer, but simply to provide an additional security step. Ideally, your application should be able to authenticate request without, for example, masking/obfuscating id in the query string.

Install
------
```
Install-Package Kent.Cryptography.Obfuscation
```

Usage
------
### Basic Example
Provides unique *id <> string mapping* which will not change unless Seed value is changed.
```
  var obfuscator = new Obfuscator();
  string obfuscatedID = obfuscator.Obfuscate(15);
  
  // Reverse-process:
  int deobfuscatedID = obfusactor.Deobfuscate(obfuscatedID);
```

### Randomized Mode
Generates randomized sequence which will cause the *Obfuscate* function to generate different values even when obfuscating the same id.
```
  var obfuscator = new Obfuscator();
  obfuscator.Strategy = ObfuscationStrategy.Randomized;
  string obfuscatedID = obfuscator.Obfuscate(15);
  
  // Reverse-process:
  int deobfuscatedID = obfusactor.Deobfuscate(obfuscatedID);
```

### Seed Value
Seed value is the 'differentiator' that makes *id <> string mapping* unique to your application. It is highly recommended to set this value rather than depending on the default value set by the library (which is 312).
```
  var obfuscator = new Obfuscator();
  obfuscator.Seed = 713;
  string obfuscatedID = obfuscator.Obfuscate(15);
```
**Note**: Minimum seed value is 2 and it's recommended to set this value to varying digits (instead of something like 100), as this will be used in XOR operations. Some example of recommended seed values are 317, 714, 213, etc.

License
------
This project is licensed under the GNU Public License v3.0. Feel free to reuse for personal and commercial use.
