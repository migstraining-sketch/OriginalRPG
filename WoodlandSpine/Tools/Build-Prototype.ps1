param([string]$UnityEditor = 'C:\Program Files\Unity\Hub\Editor\6000.5.6f1\Editor\Unity.exe')
$ErrorActionPreference='Stop'
$project=Split-Path $PSScriptRoot -Parent
$log=Join-Path $project 'Validation\unity-build.log'
$process=Start-Process -FilePath $UnityEditor -ArgumentList @('-batchmode','-nographics','-quit','-projectPath',('"'+$project+'"'),'-executeMethod','WoodlandSpine.Editor.PrototypeBuild.Setup','-logFile',('"'+$log+'"')) -WindowStyle Hidden -PassThru -Wait
if($process.ExitCode -ne 0){Write-Error "Unity build failed ($($process.ExitCode)). Read $log";exit $process.ExitCode}
Write-Output "Built $project\Builds\Windows\WoodlandSpine.exe"
