Clear-Host
$docsDiretory = (get-item $PSScriptRoot ).parent.FullName
$toolsDiretory = (get-item $PSScriptRoot ).FullName
$sharedSettings =$toolsDiretory +"\Shared.DotSettings"
$layeredSettings =$toolsDiretory +"\Layered.DotSettings"
$packagesDirectory  =$docsDiretory +"\packages"

$solutions = Get-ChildItem $docsDiretory -Filter *.sln -Recurse
foreach ($solution in $solutions) 
{
	$solution.FullName
	Set-Location $solution.DirectoryName
	$relativePath = Get-Item $sharedSettings | Resolve-Path -Relative
	$relativePath 
	$targetFile = $solution.FullName + ".DotSettings"
	Remove-Item $targetFile
	(Get-Content $layeredSettings) | 
	Foreach-Object 	{ $_ -replace 'SharedDotSettings', $relativePath }  | 
	Out-File $targetFile
	
} 