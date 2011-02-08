param (
    [string]$namespace = $args[0],
    [string]$repository = $args[1]
)

function Download([string]$url, [string]$file) {
    Import-Module BitsTransfer
    Start-BitsTransfer -DisplayName "Downloading neostructure" -Description "Progress:" -Source $url -Destination $file
}

function UnZip([string]$file, [string]$target) {
    $file = Join-Path $PWD $file
    if ($target -ne ".") {
        $target = Join-Path $PWD $target
    }
    else {
        $target = $PWD
    }

    $shell = New-Object -Com Shell.Application
    $zip = $shell.NameSpace($file)
    $folder = $shell.NameSpace($target)
    
    $folder.CopyHere($zip.Items().Item(0).GetFolder.Items())
}

MkDir "!temp"
Download "https://github.com/${repository}/zipball/master" "!temp\neostructure.zip"
UnZip "!temp\neostructure.zip" "."

Get-ChildItem -Path "." -Recurse -Include *.csproj,*.cs,*.cfg.xml | Where-Object  { ! $_.PSIsContainer } | ForEach-Object {
    $text = [System.IO.File]::ReadAllText($_.FullName)
    $replaced = [System.Text.RegularExpressions.Regex]::Replace($text, "(?<=^|\W)(X|ProjectTemplate)(?=\W|$)", "${namespace}")
    if ($replaced -ne $text) {
        [System.IO.File]::WriteAllText($_.FullName, $replaced)
    }
}

Rename-Item "Neostructure.sln" "${namespace}.sln"