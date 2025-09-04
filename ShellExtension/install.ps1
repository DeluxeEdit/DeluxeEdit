$path=  "$env:USERPROFILE\ShellAnythingX"
 $found =Test-Path $path
if ($found=false) 
{
	md $path
}

cd "$path\bin"
regsvr32 sadeluxeeditextension.dll


