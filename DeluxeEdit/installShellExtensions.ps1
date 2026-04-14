exit
try
{
 git -v
}
catch 
{ 
	winget install Git.Git
}
try
{
 dotnet   --version
}
catch 
{ 
 winget install Microsoft.DotNet.SDK.10
}
 

 $installDir= "$env:USERPROFILE\DeluxeShellExtensaions"

if (Test-Path $installDir=false) 
{
	md $installDir
}
cd $installDir
git clone  https://github.com/DeluxeEdit/ShellExtensions.git
cd ShellExtensions
dotnet build
$foundAt=find ".dll" $installDir
$RegAsmPath= "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe"
.\$RegAsmPath $foundAt


