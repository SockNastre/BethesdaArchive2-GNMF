## BethesdaArchive2 GNMF
Requires .NET Framework 4.8

### Features
 - Can pack single-chunk GNMF BA2 archives.
 - Drag & drop for files and folders to add quickly into archive.
 - Allows for modification of textures on Fallout 4 PS4.
 - CLI tool allows for dragging folder onto executable and getting archives packed straight away.

### F.A.Q

Q: _How can we **open GNMF BA2**?_  
A: The [BSA Browser](https://github.com/AlexxEG/BSA_Browser) tool by AlexxEG (see credits) supports opening GNMF BA2.  

Q: _Why only one chunk? Does that matter?_  
A: I have not researched multi-chunk GNMF BA2 but for authenticities sake I may try to include the option in a future release. From experience one chunk will do fine, but it can be assumed with a lot of high resolution textures multi-chunk would help.

Q: _What does saving the String Table do?_  
A: By default this should be on, but this removes the table from the packed output. It shouldn't have any effects on mods loading, but may effect loading from archive readers. Personally I recommend keeping this on.

### Credits
 - Thanks to [AlexxEG](https://github.com/AlexxEG) for help with understanding original BA2 format and helping me understand buffers and SharpZipLib.
 - Thanks to derwangler for getting me a proper CRC32 hashing class.
 - [SharpZipLib](https://github.com/icsharpcode/SharpZipLib) library for zlib compression.
 - Josip Medved for the OpenFolderDialog.
