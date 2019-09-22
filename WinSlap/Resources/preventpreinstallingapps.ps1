	Write-Host "Uninstalling default Microsoft applications..."
    Set-ExecutionPolicy Unrestricted
    Get-AppxProvisionedPackage -online | where-object {$_.packagename -notlike "*Microsoft.WindowsStore*"} | where-object {$_.packagename -notlike "*Microsoft.WindowsCalculator*"} | where-object {$_.packagename -notlike "*Microsoft.Windows.Photos*"} |Remove-AppxProvisionedPackage -online