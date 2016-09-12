$fakeExe = (gci .\packages\FAKE.*\tools\FAKE.exe)[0]
$scriptPath = (gci .\packages\PackProj.*\scripts\push-all.fsx)[0]

# If you use `nuget setApiKey` then set
$apiKey = ""
# else
# $apikey = "XXXXXXXXXXX"
if ($apiKey) { $apiKeyArg = "-ev api-key $apiKey" } else { $apiKeyArg = "" }

$source = "https://api.nuget.org/v3/index.json"

& $fakeExe $scriptPath -ev source $source $apiKeyArg
