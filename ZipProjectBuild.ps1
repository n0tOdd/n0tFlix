    Param
    (
         [Parameter(Mandatory=$true, Position=0)]
         [string] $Dir,
         [Parameter(Mandatory=$true, Position=1)]
         [int] $Id
    )
Compress-Archive -Path $Dir -DestinationPath "$PSScriptRoot\Build\"