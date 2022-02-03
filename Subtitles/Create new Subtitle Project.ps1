 Param(

   [Parameter(Mandatory=$true)]

   [string]$PluginName
) #end param

$Old = 'SubtitleBase'

cd $PSScriptRoot

Copy-Item -Path "n0tFlix.Plugin.SubtitleBase" -Destination "n0tFlix.Plugin.$PluginName" -recurse -Force



Get-ChildItem "n0tFlix.Plugin.$PluginName" -Recurse -Include *.* | Rename-Item -NewName { $_.Name.replace("$Old","$PluginName") }

function ReplaceText($fileInfo)
{
    if( $_.GetType().Name -ne 'FileInfo')
    {
        # i.e. reject DirectoryInfo and other types
         return
    }
    (Get-Content $fileInfo.FullName) | % {$_ -replace $Old, $PluginName} | % {$_ -replace "n0tGUID", [guid]::NewGuid().Guid}  | Set-Content -path $fileInfo.FullName
    "Processed: " + $fileInfo.FullName
}

$files = Get-ChildItem "n0tFlix.Plugin.$PluginName" -recurse
$files | % { ReplaceText( $_ ) }

Write-Output "Finished creating a new subtitle project and saved it in youre Subtitle folder as n0tFlix.Plugin.$PluginName" 