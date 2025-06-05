 $found =Test-Path "$env:USERPROFILE\ShellAnything"
if ($found=false) 
{
	md "$env:USERPROFILE\ShellAnything"
}
cd "$env:USERPROFILE\ShellAnything\bin"
regsvr32 sadeluxeeditextension.dll


