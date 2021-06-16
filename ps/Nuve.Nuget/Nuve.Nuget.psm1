function New-NugetVersion {
    [CmdletBinding()]
    param(
        [string]$Id,
        [string]$Version
    )

    $nuveUrl = $env:NUVE_URL
    if (-not $nuveUrl){
        throw "environment variable NUVE_URL is not set, pls set to smth like http://{server}/nuve"
    }

    $url = "$nuveUrl/issue/$($Id)?version=$($Version)"
    $v = Invoke-RestMethod -Method POST $url
    Write-Verbose "$Id/$Version => $v"
    return $v
}

function Get-Nuget() {
    param(
        [Parameter(Mandatory = $true)]
        [string]$id,
        [string]$version,
        [string]$source
    )

    $findParams = @{
        ProviderName = "Nuget";
        Name         = $id;
    }

    if ($source) {
        $findParams.Add("Source", $source)
    }

    if ($version) {
        $findParams.Add("MinimumVersion", $version)
        $findParams.Add("MaximumVersion", $version)
    }

    $p = Find-Package @findParams

    if (-not $p) {
        Write-Output "package not found"
        return
    }

    $sourceUrl = $(Get-PackageSource $p.Source -ProviderName Nuget).Location | Select-Object -First 1
    if (-not $sourceUrl) {
        throw "can not get a source url for $($p.Source)"
    }

    $packageUrl = "$sourceUrl/Packages(Id='$($p.Name)',Version='$($p.version)')/Download";
    Write-Output "downloaing package from $packageUrl"
    Invoke-WebRequest $packageUrl -OutFile $p.PackageFilename
    
    $null
}