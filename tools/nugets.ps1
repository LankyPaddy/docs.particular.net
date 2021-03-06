Clear-Host
$docsDiretory = (get-item $PSScriptRoot ).parent.FullName
$toolsDiretory = (get-item $PSScriptRoot ).FullName
$nuget =$toolsDiretory +"\nuget.exe"
$packagesDirectory  =$docsDiretory +"\packages"

$solutions = Get-ChildItem $docsDiretory -Filter *.sln -Recurse
Set-Location $docsDiretory
foreach ($solution in $solutions) 
{
	$solution.FullName	
	& $nuget restore $solution.FullName -packagesDirectory $packagesDirectory | Out-Null
	& $nuget update  $solution.FullName -safe -repositoryPath $packagesDirectory
} 