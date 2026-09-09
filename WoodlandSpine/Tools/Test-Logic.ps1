param([string]$UnityData = 'C:\Program Files\Unity\Hub\Editor\6000.5.6f1\Editor\Data')
$ErrorActionPreference = 'Stop'
$project = Split-Path $PSScriptRoot -Parent
$scratch = Join-Path $project 'Temp\LogicValidation'
New-Item -ItemType Directory -Force $scratch | Out-Null
$sdk = Get-ChildItem "$UnityData\DotNetSdk\sdk" -Directory | Sort-Object Name -Descending | Select-Object -First 1
$ref = Get-ChildItem "$UnityData\DotNetSdk\packs\Microsoft.NETCore.App.Ref" -Directory | Sort-Object Name -Descending | Select-Object -First 1
$refDir = Get-ChildItem "$($ref.FullName)\ref" -Directory | Select-Object -First 1
$arguments = @('-nologo','-target:exe','-langversion:latest','-nostdlib+',('-out:"'+$scratch+'\LogicHarness.dll"'))
$arguments += Get-ChildItem $refDir.FullName -Filter *.dll | ForEach-Object {'-r:"'+$_.FullName+'"'}
foreach($name in @('PrototypeData','WeaponData','BodyData','EnemyData','SliceData','HexGrid','CombatModel','EncounterLayout','OpeningState','PotionSession','CombatSelection','ReactiveIntroState')){$arguments += '"'+$project+'\Assets\Scripts\'+$name+'.cs"'}
$arguments += '"'+$project+'\Assets\Editor\RuleValidation.cs"'
$arguments += '"'+$project+'\Assets\Editor\OpeningValidation.cs"'
$arguments += '"'+$project+'\Assets\Editor\ReactiveIntroValidation.cs"'
$arguments += '"'+$PSScriptRoot+'\LogicHarness.cs"'
[IO.File]::WriteAllLines("$scratch\compile.rsp",$arguments)
& "$UnityData\DotNetSdk\dotnet.exe" "$($sdk.FullName)\Roslyn\bincore\csc.dll" "@$scratch\compile.rsp"
if($LASTEXITCODE -ne 0){exit $LASTEXITCODE}
[IO.File]::WriteAllText("$scratch\LogicHarness.runtimeconfig.json",'{"runtimeOptions":{"tfm":"net8.0","framework":{"name":"Microsoft.NETCore.App","version":"8.0.0"}}}')
& "$UnityData\DotNetSdk\dotnet.exe" "$scratch\LogicHarness.dll" | Tee-Object "$project\Validation\logic-result.txt"
exit $LASTEXITCODE
