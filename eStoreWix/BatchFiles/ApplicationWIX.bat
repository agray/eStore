:: *****************************************
:: * ApplicationWIX.bat
:: * Generates Application WIX files,
:: * moves them and commits them to Git
:: *
:: *****************************************

@ECHO OFF
"C:\Program Files\WIXWriter\WIXWriter.exe" "D:\GITRepositories\estore\eStoreWeb" eStore eStoreWixContent.wxs Config.wxi Product.wxs "D:\GITRepositories\estore\eStoreWix"

d:
cd GITRepositories\estore
"C:\Program Files\Git\bin\git.exe" config --global user.name "%1"
"C:\Program Files\Git\bin\git.exe" config --global user.email %2

"C:\Program Files\Git\bin\git.exe" add *.wxs
"C:\Program Files\Git\bin\git.exe" add *.wxi
"C:\Program Files\Git\bin\git.exe" commit -m "%BUILD_TAG% updated WIX files"
"C:\Program Files\Git\bin\git.exe" push
