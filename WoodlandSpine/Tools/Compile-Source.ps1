param([string]$UnityData = 'C:\Program Files\Unity\Hub\Editor\6000.5.6f1\Editor\Data')
$ErrorActionPreference = 'Stop'
$project = Split-Path $PSScriptRoot -Parent
$scratch = Join-Path $project 'Temp\SourceValidation'
New-Item -ItemType Directory -Force $scratch | Out-Null
$sdk = Get-ChildItem "$UnityData\DotNetSdk\sdk" -Directory | Sort-Object Name -Descending | Select-Object -First 1
$refs = @(Get-ChildItem "$UnityData\NetStandard\ref\2.1.0" -Filter *.dll)
$refs += @(Get-ChildItem "$UnityData\Managed\UnityEngine" -Filter *.dll | Where-Object {$_.Name -like 'UnityEngine.*Module.dll' -or $_.Name -like 'UnityEditor.*Module.dll'})
$arguments = @('-nologo','-target:library','-langversion:latest','-nostdlib+',('-out:"'+$scratch+'\SourceValidation.dll"'))
$arguments += $refs | ForEach-Object {'-r:"'+$_.FullName+'"'}
$arguments += Get-ChildItem "$project\Assets" -Filter *.cs -Recurse | ForEach-Object {'"'+$_.FullName+'"'}
[IO.File]::WriteAllLines("$scratch\compile.rsp",$arguments)
& "$UnityData\DotNetSdk\dotnet.exe" "$($sdk.FullName)\Roslyn\bincore\csc.dll" "@$scratch\compile.rsp"
if($LASTEXITCODE -ne 0){exit $LASTEXITCODE}
'PASS: all project C# sources compile against installed Unity assemblies. This is not a Unity import/build or playtest.' | Tee-Object "$project\Validation\source-result.txt"
