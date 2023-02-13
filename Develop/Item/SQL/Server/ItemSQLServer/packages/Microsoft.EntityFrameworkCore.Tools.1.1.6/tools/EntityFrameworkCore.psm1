$ErrorActionPreference = 'Stop'

#
# Add-Migration
#

Register-TabExpansion Add-Migration @{
    OutputDir = { <# Disabled. Otherwise, paths would be relative to the solution directory. #> }
    Context = { param($x) GetContextTypes $x.Project $x.StartupProject $x.Environment }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Adds a new migration.

.DESCRIPTION
    Adds a new migration.

.PARAMETER Name
    The name of the migration.

.PARAMETER OutputDir
    The directory (and sub-namespace) to use. Paths are relative to the project directory. Defaults to "Migrations".

.PARAMETER Context
    The DbContext type to use.

.PARAMETER Environment
    The environment to use. Defaults to "Development".

.PARAMETER Project
    The project to use.

.PARAMETER StartupProject
    The startup project to use. Defaults to the solution's startup project.

.LINK
    Remove-Migration
    Update-Database
    about_EntityFrameworkCore
#>
function Add-Migration
{
    [CmdletBinding(PositionalBinding = $false)]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $Name,
        [string] $OutputDir,
        [string] $Context,
        [string] $Environment,
        [string] $Project,
        [string] $StartupProject)

    WarnIfEF6 'Add-Migration'

    $dteProject = GetProject $Project
    $dteStartupProject = GetStartupProject $StartupProject $dteProject

    $params = 'migrations', 'add', $Name, '--json'

    if ($OutputDir)
    {
        $params += '--output-dir', $OutputDir
    }

    $params += GetParams $Context $Environment

    # NB: -join is here to support ConvertFrom-Json on PowerShell 3.0
    $result = (EF $dteProject $dteStartupProject $params) -join "`n" | ConvertFrom-Json
    Write-Output 'To undo this action, use Remove-Migration.'

    $dteProject.ProjectItems.AddFromFile($result.migrationFile) | Out-Null
    $DTE.ItemOperations.OpenFile($result.migrationFile) | Out-Null
    ShowConsole

    $dteProject.ProjectItems.AddFromFile($result.metadataFile) | Out-Null

    $dteProject.ProjectItems.AddFromFile($result.snapshotFile) | Out-Null
}

#
# Drop-Database
#

Register-TabExpansion Drop-Database @{
    Context = { param($x) GetContextTypes $x.Environment $x.Project $x.StartupProject }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Drops the database.

.DESCRIPTION
    Drops the database.

.PARAMETER Context
    The DbContext to use.

.PARAMETER Environment
    The environment to use. Defaults to "Development".

.PARAMETER Project
    The project to use.

.PARAMETER StartupProject
    The startup project to use. Defaults to the solution's startup project.

.LINK
    Update-Database
    about_EntityFrameworkCore
#>
function Drop-Database
{
    [CmdletBinding(PositionalBinding = $false, SupportsShouldProcess = $true, ConfirmImpact = 'High')]
    param([string] $Context, [string] $Environment, [string] $Project, [string] $StartupProject)

    $dteProject = GetProject $Project
    $dteStartupProject = GetStartupProject $StartupProject $dteProject

    if (IsUWP $dteProject)
    {
        throw 'Drop-Database shouldn''t be used with Universal Windows Platform apps. Instead, call ' +
            'DbContext.Database.EnsureDeleted() at runtime.'
    }

    $params = 'dbcontext', 'info', '--json'
    $params += GetParams $Context $Environment

    # NB: -join is here to support ConvertFrom-Json on PowerShell 3.0
    $info = (EF $dteProject $dteStartupProject $params) -join "`n" | ConvertFrom-Json

    if ($PSCmdlet.ShouldProcess("database '$($info.databaseName)' on server '$($info.dataSource)'"))
    {
        $params = 'database', 'drop', '--force'
        $params += GetParams $Context $Environment

        EF $dteProject $dteStartupProject $params -skipBuild
    }
}

#
# Enable-Migrations (Obsolete)
#

function Enable-Migrations
{
    WarnIfEF6 'Update-Database'
    Write-Warning 'Enable-Migrations is obsolete. Use Add-Migration to start using Migrations.'
}

#
# Remove-Migration
#

Register-TabExpansion Remove-Migration @{
    Context = { param($x) GetContextTypes $x.Environment $x.Project $x.StartupProject }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Removes the last migration.

.DESCRIPTION
    Removes the last migration.

.PARAMETER Force
    Don't check to see if the migration has been applied to the database. Always implied on UWP apps.

.PARAMETER Context
    The DbContext to use.

.PARAMETER Environment
    The environment to use. Defaults to "Development".

.PARAMETER Project
    The project to use.

.PARAMETER StartupProject
    The startup project to use. Defaults to the solution's startup project.

.LINK
    Add-Migration
    about_EntityFrameworkCore
#>
function Remove-Migration
{
    [CmdletBinding(PositionalBinding = $false)]
    param([switch] $Force, [string] $Context, [string] $Environment, [string] $Project, [string] $StartupProject)

    $dteProject = GetProject $Project
    $dteStartupProject = GetStartupProject $StartupProject $dteProject

    if (IsUWP $dteStartupProject)
    {
        $Force = [switch]::Present
    }

    $params = 'migrations', 'remove', '--json'

    if ($Force)
    {
        $params += '--force'
    }

    $params += GetParams $Context $Environment

    # NB: -join is here to support ConvertFrom-Json on PowerShell 3.0
    $result = (EF $dteProject $dteStartupProject $params) -join "`n" | ConvertFrom-Json

    $result | %{
        $projectItem = GetProjectItem $dteProject $_
        if ($projectItem)
        {
            $projectItem.Remove()
        }
    }
}

#
# Scaffold-DbContext
#

Register-TabExpansion Scaffold-DbContext @{
    Provider = { param($x) GetProviders $x.Project }
    Project = { GetProjects }
    StartupProject = { GetProjects }
    OutputDir = { <# Disabled. Otherwise, paths would be relative to the solution directory. #> }
}

<#
.SYNOPSIS
    Scaffolds a DbContext and entity types for a database.

.DESCRIPTION
    Scaffolds a DbContext and entity types for a database.

.PARAMETER Connection
    The connection string to the database.

.PARAMETER Provider
    The provider to use. (E.g. Microsoft.EntityFrameworkCore.SqlServer)

.PARAMETER OutputDir
    The directory to put files in. Paths are relaive to the project directory.

.PARAMETER Context
    The name of the DbContext to generate.

.PARAMETER Schemas
    The schemas of tables to generate entity types for.

.PARAMETER Tables
    The tables to generate entity types for.

.PARAMETER DataAnnotations
    Use attributes to configure the model (where possible). If omitted, only the fluent API is used.

.PARAMETER Force
    Overwrite existing files.

.PARAMETER Environment
    The environment to use. Defaults to "Development".

.PARAMETER Project
    The project to use.

.PARAMETER StartupProject
    The startup project to use. Defaults to the solution's startup project.

.LINK
    about_EntityFrameworkCore
#>
function Scaffold-DbContext
{
    [CmdletBinding(PositionalBinding = $false)]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $Connection,
        [Parameter(Position = 1, Mandatory =  $true)]
        [string] $Provider,
        [string] $OutputDir,
        [string] $Context,
        [string[]] $Schemas = @(),
        [string[]] $Tables = @(),
        [switch] $DataAnnotations,
        [switch] $Force,
        [string] $Environment,
        [string] $Project,
        [string] $StartupProject)

    $dteProject = GetProject $Project
    $dteStartupProject = GetStartupProject $StartupProject $dteProject

    $params = 'dbcontext', 'scaffold', $Connection, $Provider, '--json'

    if ($OutputDir)
    {
        $params += '--output-dir', $OutputDir
    }

    if ($Context)
    {
        $params += '--context', $Context
    }

    $params += $Schemas | %{ '--schema', $_ }
    $params += $Tables | %{ '--table', $_ }

    if ($DataAnnotations)
    {
        $params += '--data-annotations'
    }

    if ($Force)
    {
        $params += '--force'
    }

    $params += GetParams -Environment $Environment

    # NB: -join is here to support ConvertFrom-Json on PowerShell 3.0
    $result = (EF $dteProject $dteStartupProject $params) -join "`n" | ConvertFrom-Json

    $result | %{ $dteProject.ProjectItems.AddFromFile($_) | Out-Null }
    $DTE.ItemOperations.OpenFile($result[0]) | Out-Null
    ShowConsole
}

#
# Script-Migration
#

Register-TabExpansion Script-Migration @{
    From = { param($x) GetMigrations $x.Context $x.Environment $x.Project $x.StartupProject }
    To = { param($x) GetMigrations $x.Context $x.Environment $x.Project $x.StartupProject }
    Context = { param($x) GetContextTypes $x.Environment $x.Project $x.StartupProject }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Generates a SQL script from migrations.

.DESCRIPTION
    Generates a SQL script from migrations.

.PARAMETER From
    The starting migration. Defaults to '0' (the initial database).

.PARAMETER To
    The ending migration. Defaults to the last migration.

.PARAMETER Idempotent
    Generate a script that can be used on a database at any migration.

.PARAMETER Output
    The file to write the result to.

.PARAMETER Context
    The DbContext to use.

.PARAMETER Environment
    The environment to use. Defaults to "Development".

.PARAMETER Project
    The project to use.

.PARAMETER StartupProject
    The startup project to use. Defaults to the solution's startup project.

.LINK
    Update-Database
    about_EntityFrameworkCore
#>
function Script-Migration
{
    [CmdletBinding(PositionalBinding = $false)]
    param(
        [Parameter(ParameterSetName = 'WithoutTo', Position = 0)]
        [Parameter(ParameterSetName = 'WithTo', Position = 0, Mandatory = $true)]
        [string] $From,
        [Parameter(ParameterSetName = 'WithTo', Position = 1, Mandatory = $true)]
        [string] $To,
        [switch] $Idempotent,
        [string] $Output,
        [string] $Context,
        [string] $Environment,
        [string] $Project,
        [string] $StartupProject)

    $dteProject = GetProject $Project
    $dteStartupProject = GetStartupProject $StartupProject $dteProject

    if (!$Output)
    {
        $intermediatePath = GetIntermediatePath $dteProject
        if (!(Split-Path $intermediatePath -IsAbsolute))
        {
            $projectDir = GetProperty $dteProject.Properties 'FullPath'
            $intermediatePath = Join-Path $projectDir $intermediatePath -Resolve
        }

        $scriptFileName = [IO.Path]::ChangeExtension([IO.Path]::GetRandomFileName(), '.sql')
        $Output = Join-Path $intermediatePath $scriptFileName
    }
    elseif (!(Split-Path $Output -IsAbsolute))
    {
        $Output = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($Output)
    }

    $params = 'migrations', 'script', '--output', $Output

    if ($From)
    {
        $params += $From
    }

    if ($To)
    {
        $params += $To
    }

    if ($Idempotent)
    {
        $params += '--idempotent'
    }

    $params += GetParams $Context $Environment

    EF $dteProject $dteStartupProject $params

    $DTE.ItemOperations.OpenFile($Output) | Out-Null
    ShowConsole
}

#
# Update-Database
#

Register-TabExpansion Update-Database @{
    Migration = { param($x) GetMigrations $x.Context $x.Environment $x.Project $x.StartupProject }
    Context = { param($x) GetContextTypes $x.Environment $x.Project $x.StartupProject }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Updates the database to a specified migration.

.DESCRIPTION
    Updates the database to a specified migration.

.PARAMETER Migration
    The target migration. If '0', all migrations will be reverted. Defaults to the last migration.

.PARAMETER Context
    The DbContext to use.

.PARAMETER Environment
    The environment to use. Defaults to "Development".

.PARAMETER Project
    The project to use.

.PARAMETER StartupProject
    The startup project to use. Defaults to the solution's startup project.

.LINK
    Script-Migration
    about_EntityFrameworkCore
#>
function Update-Database
{
    [CmdletBinding(PositionalBinding = $false)]
    param(
        [Parameter(Position = 0)]
        [string] $Migration,
        [string] $Context,
        [string] $Environment,
        [string] $Project,
        [string] $StartupProject)

    WarnIfEF6 'Update-Database'

    $dteProject = GetProject $Project
    $dteStartupProject = GetStartupProject $StartupProject $dteProject

    if (IsUWP $dteStartupProject)
    {
        throw 'Update-Database shouldn''t be used with Universal Windows Platform apps. Instead, call ' +
            'DbContext.Database.Migrate() at runtime.'
    }

    $params = 'database', 'update'

    if ($Migration)
    {
        $params += $Migration
    }

    $params += GetParams $Context $Environment

    EF $dteProject $dteStartupProject $params
}

#
# (Private Helpers)
#

function GetProjects
{
    return Get-Project -All | %{ $_.ProjectName }
}

function GetProviders($projectName)
{
    if (!$projectName)
    {
        $projectName = (Get-Project).ProjectName
    }

    return Get-Package -ProjectName $projectName | %{ $_.Id }
}

function GetContextTypes($environment, $projectName, $startupProjectName)
{
    $project = GetProject $projectName
    $startupProject = GetStartupProject $startupProjectName $project

    $params = 'dbcontext', 'list', '--json'
    $params += GetParams -Environment $environment

    # NB: -join is here to support ConvertFrom-Json on PowerShell 3.0
    $result = (EF $project $startupProject $params -skipBuild) -join "`n" | ConvertFrom-Json

    return $result | %{ $_.safeName }
}

function GetMigrations($context, $environment, $projectName, $startupProjectName)
{
    $project = GetProject $projectName
    $startupProject = GetStartupProject $startupProjectName $project

    $params = 'migrations', 'list', '--json'
    $params += GetParams $context $environment

    # NB: -join is here to support ConvertFrom-Json on PowerShell 3.0
    $result = (EF $project $startupProject $params -skipBuild) -join "`n" | ConvertFrom-Json

    return $result | %{ $_.safeName }
}

function WarnIfEF6 ($cmdlet)
{
    if (Get-Module 'EntityFramework')
    {
        Write-Warning "Both Entity Framework Core and Entity Framework 6 are installed. The Entity Framework Core tools are running. Use 'EntityFramework\$cmdlet' for Entity Framework 6."
    }
}

function GetProject($projectName)
{
    if (!$projectName)
    {
        return Get-Project
    }

    return Get-Project $projectName
}

function GetStartupProject($name, $fallbackProject)
{
    if ($name)
    {
        return Get-Project $name
    }

    $startupProjectPaths = $DTE.Solution.SolutionBuild.StartupProjects
    if ($startupProjectPaths)
    {
        if ($startupProjectPaths.Length -eq 1)
        {
            $startupProjectPath = $startupProjectPaths[0]
            if (!(Split-Path -IsAbsolute $startupProjectPath))
            {
                $solutionPath = Split-Path (GetProperty $DTE.Solution.Properties 'Path')
                $startupProjectPath = Join-Path $solutionPath $startupProjectPath -Resolve
            }

            $startupProject = GetSolutionProjects | ?{
                try
                {
                    $fullName = $_.FullName
                }
                catch [NotImplementedException]
                {
                    return $false
                }

                if ($fullName -and $fullName.EndsWith('\'))
                {
                    $fullName = $fullName.Substring(0, $fullName.Length - 1)
                }

                return $fullName -eq $startupProjectPath
            }
            if ($startupProject)
            {
                return $startupProject
            }

            Write-Warning "Unable to resolve startup project '$startupProjectPath'."
        }
        else
        {
            Write-Verbose 'More than one startup project found.'
        }
    }
    else
    {
        Write-Verbose 'No startup project found.'
    }

    return $fallbackProject
}

function GetSolutionProjects()
{
    $projects = New-Object 'System.Collections.Stack'

    $DTE.Solution.Projects | %{
        $projects.Push($_)
    }

    while ($projects.Count)
    {
        $project = $projects.Pop();

        <# yield return #> $project

        if ($project.ProjectItems)
        {
            $project.ProjectItems | ?{ $_.SubProject } | %{
                $projects.Push($_.SubProject)
            }
        }
    }
}

function GetParams($context, $environment)
{
    $params = @()

    if ($context)
    {
        $params += '--context', $context
    }

    if ($environment)
    {
        $params += '--environment', $environment
    }

    return $params
}

function ShowConsole
{
    $componentModel = Get-VSComponentModel
    $powerConsoleWindow = $componentModel.GetService([NuGetConsole.IPowerConsoleWindow])
    $powerConsoleWindow.Show()
}

function EF($project, $startupProject, $params, [switch] $skipBuild)
{
    if (IsUWP $project)
    {
        $outputType = GetProperty $project.Properties 'OutputType'
        $outputTypeEx = GetProperty $project.Properties 'OutputTypeEx'
        if ($outputType -eq 2 -and $outputTypeEx -eq 3)
        {
            throw "Project '$($project.ProjectName)' is a Windows Runtime component. The Entity Framework Core " +
                'Package Manager Console Tools don''t support this type of project.'
        }
    }

    if (IsXproj $startupProject)
    {
        throw "Startup project '$($startupProject.ProjectName)' is an ASP.NET Core or .NET Core project for Visual " +
            'Studio 2015. This version of the Entity Framework Core Package Manager Console Tools doesn''t support ' +
            'these types of projects.'
    }
    if (IsUWP $startupProject)
    {
        $useDotNetNative = GetProperty $startupProject.ConfigurationManager.ActiveConfiguration.Properties 'ProjectN.UseDotNetNativeToolchain'
        if ($useDotNetNative -eq 'True')
        {
            throw "Startup project '$($startupProject.ProjectName)' compiles with the .NET Native tool chan. Uncheck " +
                'this option in the project settings or use a different configuration and try again.'
        }

        $outputType = GetProperty $startupProject.Properties 'OutputType'
        if ($outputType -eq 2)
        {
            $outputTypeEx = GetProperty $startupProject.Properties 'OutputTypeEx'
            if ($outputTypeEx -eq 2)
            {
                throw "Startup project '$($startupProject.ProjectName)' is a class library. Select a Universal " +
                    'Windows Platform app as your startup project and try again.'
            }
            if ($outputTypeEx -eq 3)
            {
                throw "Startup project '$($startupProject.ProjectName)' is a Windows Runtime component. The Entity " +
                    'Framework Core Package Manager Console Tools don''t support this type of project.'
            }
        }
    }

    Write-Verbose "Using project '$($project.ProjectName)'."
    Write-Verbose "Using startup project '$($startupProject.ProjectName)'."

    if (!$skipBuild)
    {
        Write-Verbose 'Build started...'

        # TODO: Only build startup project. Don't use BuildProject, you can't specify platform
        $solutionBuild = $DTE.Solution.SolutionBuild
        $solutionBuild.Build(<# WaitForBuildToFinish: #> $true)
        if ($solutionBuild.LastBuildInfo)
        {
            throw 'Build failed.'
        }

        Write-Verbose 'Build succeeded.'
    }

    $startupProjectDir = GetProperty $startupProject.Properties 'FullPath'
    $outputPath = GetProperty $startupProject.ConfigurationManager.ActiveConfiguration.Properties 'OutputPath'
    $targetDir = Join-Path $startupProjectDir $outputPath -Resolve
    $startupTargetFileName = GetOutputFileName $startupProject
    $startupTargetPath = Join-Path $targetDir $startupTargetFileName
    $targetFrameworkMoniker = GetProperty $startupProject.Properties 'TargetFrameworkMoniker'
    $frameworkName = New-Object 'System.Runtime.Versioning.FrameworkName' $targetFrameworkMoniker
    $targetFramework = $frameworkName.Identifier

    if ($targetFramework -in '.NETFramework', '.NETCore')
    {
        $platformTarget = GetPlatformTarget $startupProject
        if ($platformTarget -eq 'x86')
        {
            $exePath = Join-Path $PSScriptRoot 'net451\ef.x86.exe'
        }
        elseif ($platformTarget -in 'AnyCPU', 'x64')
        {
            $exePath = Join-Path $PSScriptRoot 'net451\ef.exe'
        }
        else
        {
            throw "Startup project '$($startupProject.ProjectName)' has an active platform of '$platformTarget'. Select " +
                'a different platform and try again.'
        }
    }
    elseif ($targetFramework -in '.NETCoreApp', '.NETStandard')
    {
        if ($targetFramework -eq '.NETStandard')
        {
            Write-Warning ("Startup project '$($startupProject.ProjectName)' targets framework '.NETStandard'. This " +
                "framework is not intended for execution and may fail to resolve runtime dependencies. If so, select " +
                "a different startup project and try again.")
        }

        $exePath = (Get-Command 'dotnet').Path

        $startupTargetName = GetProperty $startupProject.Properties 'AssemblyName'
        $depsFile = Join-Path $targetDir ($startupTargetName + '.deps.json')
        $projectAssetsFile = GetCsproj2Property $startupProject 'ProjectAssetsFile'
        $runtimeConfig = Join-Path $targetDir ($startupTargetName + '.runtimeconfig.json')
        $runtimeFrameworkVersion = GetCsproj2Property $startupProject 'RuntimeFrameworkVersion'
        $efPath = Join-Path $PSScriptRoot 'netcoreapp1.0\ef.dll'

        $dotnetParams = 'exec', '--depsfile', $depsFile

        if ($projectAssetsFile)
        {
            # NB: -Raw is here to support ConvertFrom-Json on PowerShell 3.0
            $projectAssets = Get-Content $projectAssetsFile -Raw | ConvertFrom-Json
            $projectAssets.packageFolders.psobject.Properties.Name | %{
                $dotnetParams += '--additionalprobingpath', $_.TrimEnd('\')
            }
        }

        if (Test-Path $runtimeConfig)
        {
            $dotnetParams += '--runtimeconfig', $runtimeConfig
        }
        elseif ($runtimeFrameworkVersion)
        {
            $dotnetParams += '--fx-version', $runtimeFrameworkVersion
        }

        $dotnetParams += $efPath

        $params = $dotnetParams + $params
    }
    else
    {
        throw "Startup project '$($startupProject.ProjectName)' targets framework '$targetFramework'. " +
            'The Entity Framework Core Package Manager Console Tools don''t support this framework.'
    }

    $projectDir = GetProperty $project.Properties 'FullPath'
    $targetFileName = GetOutputFileName $project
    $targetPath = Join-Path $targetDir $targetFileName
    $rootNamespace = GetProperty $project.Properties 'RootNamespace'

    if (IsWeb $startupProject)
    {
        $dataDir = Join-Path $startupProjectDir 'App_Data'
    }
    else
    {
        $dataDir = $targetDir
    }

    $params += '--verbose',
        '--no-color',
        '--prefix-output',
        '--assembly', $targetPath,
        '--startup-assembly', $startupTargetPath,
        '--project-dir', $projectDir,
        '--content-root', $startupProjectDir,
        '--data-dir', $dataDir

    if (IsUWP $startupProject)
    {
        $params += '--no-appdomain'
    }

    if ($rootNamespace)
    {
        $params += '--root-namespace', $rootNamespace
    }

    $arguments = ToArguments $params
    $startInfo = New-Object 'System.Diagnostics.ProcessStartInfo' -Property @{
        FileName = $exePath;
        Arguments = $arguments;
        UseShellExecute = $false;
        CreateNoWindow = $true;
        RedirectStandardOutput = $true;
        StandardOutputEncoding = [Text.Encoding]::UTF8;
    }

    Write-Verbose "$exePath $arguments"

    $process = [Diagnostics.Process]::Start($startInfo)

    while ($line = $process.StandardOutput.ReadLine())
    {
        $level = $null
        $text = $null

        $parts = $line.Split(':', 2)
        if ($parts.Length -eq 2)
        {
            $level = $parts[0]
            $text = $parts[1].Substring(8 - $level.Length)
        }

        switch ($level)
        {
            'error' { throw $text }
            'warn' { Write-Warning $text }
            'info' { Write-Host $text }
            'data' { Write-Output $text }
            'verbose' { Write-Verbose $text }
            default { Write-Host $line }
        }
    }

    $process.WaitForExit()
}

function IsXproj($project)
{
    return $project.Kind -eq '{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}'
}

function IsCsproj2($project)
{
    return $project.Kind -eq '{9A19103F-16F7-4668-BE54-9A1E7A4F7556}'
}

function IsWeb($project)
{
    $types = GetProjectTypes $project

    return $types -contains '{349C5851-65DF-11DA-9384-00065B846F21}'
}

function IsUWP($project)
{
    $types = GetProjectTypes $project

    return $types -contains '{A5A43C5B-DE2A-4C0C-9213-0A381AF9435A}'
}

function GetIntermediatePath($project)
{
    # TODO: Remove when dotnet/roslyn-project-system#665 is fixed
    if (IsCsproj2 $project)
    {
        return GetCsproj2Property $project 'IntermediateOutputPath'
    }

    return GetProperty $project.ConfigurationManager.ActiveConfiguration.Properties 'IntermediatePath'
}

function GetPlatformTarget($project)
{
    # TODO: Remove when dotnet/roslyn-project-system#669 is fixed
    if (IsCsproj2 $project)
    {
        $platformTarget = GetCsproj2Property $project 'PlatformTarget'
        if ($platformTarget)
        {
            return $platformTarget
        }

        return GetCsproj2Property $project 'Platform'
    }

    return GetProperty $project.ConfigurationManager.ActiveConfiguration.Properties 'PlatformTarget'
}

function GetOutputFileName($project)
{
    # TODO: Remove when dotnet/roslyn-project-system#667 is fixed
    if (IsCsproj2 $project)
    {
        return GetCsproj2Property $project 'TargetFileName'
    }

    return GetProperty $project.Properties 'OutputFileName'
}

function GetProjectTypes($project)
{
    $solution = Get-VSService 'Microsoft.VisualStudio.Shell.Interop.SVsSolution' 'Microsoft.VisualStudio.Shell.Interop.IVsSolution'
    $hierarchy = $null
    $hr = $solution.GetProjectOfUniqueName($project.UniqueName, [ref] $hierarchy)
    [Runtime.InteropServices.Marshal]::ThrowExceptionForHR($hr)

    $aggregatableProject = Get-Interface $hierarchy 'Microsoft.VisualStudio.Shell.Interop.IVsAggregatableProject'
    if (!$aggregatableProject)
    {
        return $project.Kind
    }

    $projectTypeGuidsString = $null
    $hr = $aggregatableProject.GetAggregateProjectTypeGuids([ref] $projectTypeGuidsString)
    [Runtime.InteropServices.Marshal]::ThrowExceptionForHR($hr)

    return $projectTypeGuidsString.Split(';')
}

function GetProperty($properties, $propertyName)
{
    try
    {
        return $properties.Item($propertyName).Value
    }
    catch
    {
        return $null
    }
}

function GetCsproj2Property($project, $propertyName)
{
    $browseObjectContext = Get-Interface $project 'Microsoft.VisualStudio.ProjectSystem.Properties.IVsBrowseObjectContext'
    $unconfiguredProject = $browseObjectContext.UnconfiguredProject
    $configuredProject = $unconfiguredProject.GetSuggestedConfiguredProjectAsync().Result
    $properties = $configuredProject.Services.ProjectPropertiesProvider.GetCommonProperties()

    return $properties.GetEvaluatedPropertyValueAsync($propertyName).Result
}

function GetProjectItem($project, $path)
{
    $fullPath = GetProperty $project.Properties 'FullPath'

    if (Split-Path $path -IsAbsolute)
    {
        $path = $path.Substring($fullPath.Length)
    }

    $itemDirectory = (Split-Path $path -Parent)

    $projectItems = $project.ProjectItems
    if ($itemDirectory)
    {
        $directories = $itemDirectory.Split('\')
        $directories | %{
            if ($projectItems)
            {
                $projectItems = $projectItems.Item($_).ProjectItems
            }
        }
    }

    if (!$projectItems)
    {
        return $null
    }

    $itemName = Split-Path $path -Leaf

    try
    {
        return $projectItems.Item($itemName)
    }
    catch [Exception]
    {
    }

    return $null
}

function ToArguments($params)
{
    $arguments = ''
    for ($i = 0; $i -lt $params.Length; $i++)
    {
        if ($i)
        {
            $arguments += " "
        }

        if (!$params[$i].Contains(' '))
        {
            $arguments += $params[$i]

            continue
        }

        $arguments += '"'

        $pendingBackslashs = 0
        for ($j = 0; $j -lt $params[$i].Length; $j++)
        {
            switch ($params[$i][$j])
            {
                '"'
                {
                    if ($pendingBackslashs)
                    {
                        $arguments += '\' * $pendingBackslashs * 2
                        $pendingBackslashs = 0
                    }
                    $arguments += '\"'
                }

                '\'
                {
                    $pendingBackslashs++
                }

                default
                {
                    if ($pendingBackslashs)
                    {
                        if ($pendingBackslashs -eq 1)
                        {
                            $arguments += '\'
                        }
                        else
                        {
                            $arguments += '\' * $pendingBackslashs * 2
                        }

                        $pendingBackslashs = 0
                    }

                    $arguments += $params[$i][$j]
                }
            }
        }

        if ($pendingBackslashs)
        {
            $arguments += '\' * $pendingBackslashs * 2
        }

        $arguments += '"'
    }

    return $arguments
}

# SIG # Begin signature block
# MIIj9AYJKoZIhvcNAQcCoIIj5TCCI+ECAQExDzANBglghkgBZQMEAgEFADB5Bgor
# BgEEAYI3AgEEoGswaTA0BgorBgEEAYI3AgEeMCYCAwEAAAQQH8w7YFlLCE63JNLG
# KX7zUQIBAAIBAAIBAAIBAAIBADAxMA0GCWCGSAFlAwQCAQUABCCE4+ntjy8Xld2g
# tpoS//LfMX34t781e3slHHaxnzJf/aCCDYMwggYBMIID6aADAgECAhMzAAAAxOmJ
# +HqBUOn/AAAAAADEMA0GCSqGSIb3DQEBCwUAMH4xCzAJBgNVBAYTAlVTMRMwEQYD
# VQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24xKDAmBgNVBAMTH01pY3Jvc29mdCBDb2RlIFNpZ25p
# bmcgUENBIDIwMTEwHhcNMTcwODExMjAyMDI0WhcNMTgwODExMjAyMDI0WjB0MQsw
# CQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9u
# ZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMR4wHAYDVQQDExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIB
# AQCIirgkwwePmoB5FfwmYPxyiCz69KOXiJZGt6PLX4kvOjMuHpF4+nypH4IBtXrL
# GrwDykbrxZn3+wQd8oUK/yJuofJnPcUnGOUoH/UElEFj7OO6FYztE5o13jhwVG87
# 7K1FCTBJwb6PMJkMy3bJ93OVFnfRi7uUxwiFIO0eqDXxccLgdABLitLckevWeP6N
# +q1giD29uR+uYpe/xYSxkK7WryvTVPs12s1xkuYe/+xxa8t/CHZ04BBRSNTxAMhI
# TKMHNeVZDf18nMjmWuOF9daaDx+OpuSEF8HWyp8dAcf9SKcTkjOXIUgy+MIkogCy
# vlPKg24pW4HvOG6A87vsEwvrAgMBAAGjggGAMIIBfDAfBgNVHSUEGDAWBgorBgEE
# AYI3TAgBBggrBgEFBQcDAzAdBgNVHQ4EFgQUy9ZihM9gOer/Z8Jc0si7q7fDE5gw
# UgYDVR0RBEswSaRHMEUxDTALBgNVBAsTBE1PUFIxNDAyBgNVBAUTKzIzMDAxMitj
# ODA0YjVlYS00OWI0LTQyMzgtODM2Mi1kODUxZmEyMjU0ZmMwHwYDVR0jBBgwFoAU
# SG5k5VAF04KqFzc3IrVtqMp1ApUwVAYDVR0fBE0wSzBJoEegRYZDaHR0cDovL3d3
# dy5taWNyb3NvZnQuY29tL3BraW9wcy9jcmwvTWljQ29kU2lnUENBMjAxMV8yMDEx
# LTA3LTA4LmNybDBhBggrBgEFBQcBAQRVMFMwUQYIKwYBBQUHMAKGRWh0dHA6Ly93
# d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMvY2VydHMvTWljQ29kU2lnUENBMjAxMV8y
# MDExLTA3LTA4LmNydDAMBgNVHRMBAf8EAjAAMA0GCSqGSIb3DQEBCwUAA4ICAQAG
# Fh/bV8JQyCNPolF41+34/c291cDx+RtW7VPIaUcF1cTL7OL8mVuVXxE4KMAFRRPg
# mnmIvGar27vrAlUjtz0jeEFtrvjxAFqUmYoczAmV0JocRDCppRbHukdb9Ss0i5+P
# WDfDThyvIsoQzdiCEKk18K4iyI8kpoGL3ycc5GYdiT4u/1cDTcFug6Ay67SzL1BW
# XQaxFYzIHWO3cwzj1nomDyqWRacygz6WPldJdyOJ/rEQx4rlCBVRxStaMVs5apao
# pIhrlihv8cSu6r1FF8xiToG1VBpHjpilbcBuJ8b4Jx/I7SCpC7HxzgualOJqnWmD
# oTbXbSD+hdX/w7iXNgn+PRTBmBSpwIbM74LBq1UkQxi1SIV4htD50p0/GdkUieeN
# n2gkiGg7qceATibnCCFMY/2ckxVNM7VWYE/XSrk4jv8u3bFfpENryXjPsbtrj4Ns
# h3Kq6qX7n90a1jn8ZMltPgjlfIOxrbyjunvPllakeljLEkdi0iHv/DzEMQv3Lz5k
# pTdvYFA/t0SQT6ALi75+WPbHZ4dh256YxMiMy29H4cAulO2x9rAwbexqSajplnbI
# vQjE/jv1rnM3BrJWzxnUu/WUyocc8oBqAU+2G4Fzs9NbIj86WBjfiO5nxEmnL9wl
# iz1e0Ow0RJEdvJEMdoI+78TYLaEEAo5I+e/dAs8DojCCB3owggVioAMCAQICCmEO
# kNIAAAAAAAMwDQYJKoZIhvcNAQELBQAwgYgxCzAJBgNVBAYTAlVTMRMwEQYDVQQI
# EwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3Nv
# ZnQgQ29ycG9yYXRpb24xMjAwBgNVBAMTKU1pY3Jvc29mdCBSb290IENlcnRpZmlj
# YXRlIEF1dGhvcml0eSAyMDExMB4XDTExMDcwODIwNTkwOVoXDTI2MDcwODIxMDkw
# OVowfjELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcT
# B1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEoMCYGA1UE
# AxMfTWljcm9zb2Z0IENvZGUgU2lnbmluZyBQQ0EgMjAxMTCCAiIwDQYJKoZIhvcN
# AQEBBQADggIPADCCAgoCggIBAKvw+nIQHC6t2G6qghBNNLrytlghn0IbKmvpWlCq
# uAY4GgRJun/DDB7dN2vGEtgL8DjCmQawyDnVARQxQtOJDXlkh36UYCRsr55JnOlo
# XtLfm1OyCizDr9mpK656Ca/XllnKYBoF6WZ26DJSJhIv56sIUM+zRLdd2MQuA3Wr
# aPPLbfM6XKEW9Ea64DhkrG5kNXimoGMPLdNAk/jj3gcN1Vx5pUkp5w2+oBN3vpQ9
# 7/vjK1oQH01WKKJ6cuASOrdJXtjt7UORg9l7snuGG9k+sYxd6IlPhBryoS9Z5JA7
# La4zWMW3Pv4y07MDPbGyr5I4ftKdgCz1TlaRITUlwzluZH9TupwPrRkjhMv0ugOG
# jfdf8NBSv4yUh7zAIXQlXxgotswnKDglmDlKNs98sZKuHCOnqWbsYR9q4ShJnV+I
# 4iVd0yFLPlLEtVc/JAPw0XpbL9Uj43BdD1FGd7P4AOG8rAKCX9vAFbO9G9RVS+c5
# oQ/pI0m8GLhEfEXkwcNyeuBy5yTfv0aZxe/CHFfbg43sTUkwp6uO3+xbn6/83bBm
# 4sGXgXvt1u1L50kppxMopqd9Z4DmimJ4X7IvhNdXnFy/dygo8e1twyiPLI9AN0/B
# 4YVEicQJTMXUpUMvdJX3bvh4IFgsE11glZo+TzOE2rCIF96eTvSWsLxGoGyY0uDW
# iIwLAgMBAAGjggHtMIIB6TAQBgkrBgEEAYI3FQEEAwIBADAdBgNVHQ4EFgQUSG5k
# 5VAF04KqFzc3IrVtqMp1ApUwGQYJKwYBBAGCNxQCBAweCgBTAHUAYgBDAEEwCwYD
# VR0PBAQDAgGGMA8GA1UdEwEB/wQFMAMBAf8wHwYDVR0jBBgwFoAUci06AjGQQ7kU
# BU7h6qfHMdEjiTQwWgYDVR0fBFMwUTBPoE2gS4ZJaHR0cDovL2NybC5taWNyb3Nv
# ZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWljUm9vQ2VyQXV0MjAxMV8yMDExXzAz
# XzIyLmNybDBeBggrBgEFBQcBAQRSMFAwTgYIKwYBBQUHMAKGQmh0dHA6Ly93d3cu
# bWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljUm9vQ2VyQXV0MjAxMV8yMDExXzAz
# XzIyLmNydDCBnwYDVR0gBIGXMIGUMIGRBgkrBgEEAYI3LgMwgYMwPwYIKwYBBQUH
# AgEWM2h0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMvZG9jcy9wcmltYXJ5
# Y3BzLmh0bTBABggrBgEFBQcCAjA0HjIgHQBMAGUAZwBhAGwAXwBwAG8AbABpAGMA
# eQBfAHMAdABhAHQAZQBtAGUAbgB0AC4gHTANBgkqhkiG9w0BAQsFAAOCAgEAZ/KG
# pZjgVHkaLtPYdGcimwuWEeFjkplCln3SeQyQwWVfLiw++MNy0W2D/r4/6ArKO79H
# qaPzadtjvyI1pZddZYSQfYtGUFXYDJJ80hpLHPM8QotS0LD9a+M+By4pm+Y9G6XU
# tR13lDni6WTJRD14eiPzE32mkHSDjfTLJgJGKsKKELukqQUMm+1o+mgulaAqPypr
# WEljHwlpblqYluSD9MCP80Yr3vw70L01724lruWvJ+3Q3fMOr5kol5hNDj0L8giJ
# 1h/DMhji8MUtzluetEk5CsYKwsatruWy2dsViFFFWDgycScaf7H0J/jeLDogaZiy
# WYlobm+nt3TDQAUGpgEqKD6CPxNNZgvAs0314Y9/HG8VfUWnduVAKmWjw11SYobD
# HWM2l4bf2vP48hahmifhzaWX0O5dY0HjWwechz4GdwbRBrF1HxS+YWG18NzGGwS+
# 30HHDiju3mUv7Jf2oVyW2ADWoUa9WfOXpQlLSBCZgB/QACnFsZulP0V3HjXG0qKi
# n3p6IvpIlR+r+0cjgPWe+L9rt0uX4ut1eBrs6jeZeRhL/9azI2h15q/6/IvrC4Dq
# aTuv/DDtBEyO3991bWORPdGdVk5Pv4BXIqF4ETIheu9BCrE/+6jMpF3BoYibV3FW
# TkhFwELJm3ZbCoBIa/15n8G9bW1qyVJzEw16UM0xghXHMIIVwwIBATCBlTB+MQsw
# CQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9u
# ZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSgwJgYDVQQDEx9NaWNy
# b3NvZnQgQ29kZSBTaWduaW5nIFBDQSAyMDExAhMzAAAAxOmJ+HqBUOn/AAAAAADE
# MA0GCWCGSAFlAwQCAQUAoIG6MBkGCSqGSIb3DQEJAzEMBgorBgEEAYI3AgEEMBwG
# CisGAQQBgjcCAQsxDjAMBgorBgEEAYI3AgEVMC8GCSqGSIb3DQEJBDEiBCAAFPsp
# dZ4ehih3ZQwnvGtT32bqX9wbmc4fd0EZt+C/wjBOBgorBgEEAYI3AgEMMUAwPqAk
# gCIATQBpAGMAcgBvAHMAbwBmAHQAIABBAFMAUAAuAE4ARQBUoRaAFGh0dHA6Ly93
# d3cuYXNwLm5ldC8gMA0GCSqGSIb3DQEBAQUABIIBAAjZsW4JkzcKVGFIALvkeqxJ
# cjWfvlGNVTVCfkRpEolZYvfcDSMvrMMnMUJOxobp5BxVU5spxRjUsAeQBPqLxnIj
# x07MmPHOecrnAkVgHX1HGO6DeIKrDgAfCLdY+Dgjz5vXMG59WOWJLZJ40CHY6uQr
# uprRSBUF3XKOw2ZhmErGfdharNe2UBH+Q/v55LG/R23g9caVX/xoUd40U8DF01VY
# MYAaJccT45+/pFrx0GrGYAaqpujoe9OmblBsDWr/kjlSOLILAN4+b2wvJm6XFZyF
# LYQAhLmxnAPa6nHUhIydAPDa6s1BHoa6bV0CsTwrSO53IBifSM7dqOOz3K2V0qah
# ghNFMIITQQYKKwYBBAGCNwMDATGCEzEwghMtBgkqhkiG9w0BBwKgghMeMIITGgIB
# AzEPMA0GCWCGSAFlAwQCAQUAMIIBOwYLKoZIhvcNAQkQAQSgggEqBIIBJjCCASIC
# AQEGCisGAQQBhFkKAwEwMTANBglghkgBZQMEAgEFAAQgS5WxwPZv/wvUBF/1aa7w
# AZZsku+CxtqcTOTzx8Y/fwsCBlspUDhBChgTMjAxODA2MjEwNzI2NTguMjc3WjAH
# AgEBgAIB9KCBt6SBtDCBsTELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0
# b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3Jh
# dGlvbjEMMAoGA1UECxMDQU9DMSYwJAYDVQQLEx1UaGFsZXMgVFNTIEVTTjpDM0Iw
# LTBGNkEtNDExMTElMCMGA1UEAxMcTWljcm9zb2Z0IFRpbWUtU3RhbXAgU2Vydmlj
# ZaCCDsowggZxMIIEWaADAgECAgphCYEqAAAAAAACMA0GCSqGSIb3DQEBCwUAMIGI
# MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVk
# bW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMTIwMAYDVQQDEylN
# aWNyb3NvZnQgUm9vdCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkgMjAxMDAeFw0xMDA3
# MDEyMTM2NTVaFw0yNTA3MDEyMTQ2NTVaMHwxCzAJBgNVBAYTAlVTMRMwEQYDVQQI
# EwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3Nv
# ZnQgQ29ycG9yYXRpb24xJjAkBgNVBAMTHU1pY3Jvc29mdCBUaW1lLVN0YW1wIFBD
# QSAyMDEwMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqR0NvHcRijog
# 7PwTl/X6f2mUa3RUENWlCgCChfvtfGhLLF/Fw+Vhwna3PmYrW/AVUycEMR9BGxqV
# Hc4JE458YTBZsTBED/FgiIRUQwzXTbg4CLNC3ZOs1nMwVyaCo0UN0Or1R4HNvyRg
# MlhgRvJYR4YyhB50YWeRX4FUsc+TTJLBxKZd0WETbijGGvmGgLvfYfxGwScdJGcS
# chohiq9LZIlQYrFd/XcfPfBXday9ikJNQFHRD5wGPmd/9WbAA5ZEfu/QS/1u5ZrK
# sajyeioKMfDaTgaRtogINeh4HLDpmc085y9Euqf03GS9pAHBIAmTeM38vMDJRF1e
# FpwBBU8iTQIDAQABo4IB5jCCAeIwEAYJKwYBBAGCNxUBBAMCAQAwHQYDVR0OBBYE
# FNVjOlyKMZDzQ3t8RhvFM2hahW1VMBkGCSsGAQQBgjcUAgQMHgoAUwB1AGIAQwBB
# MAsGA1UdDwQEAwIBhjAPBgNVHRMBAf8EBTADAQH/MB8GA1UdIwQYMBaAFNX2VsuP
# 6KJcYmjRPZSQW9fOmhjEMFYGA1UdHwRPME0wS6BJoEeGRWh0dHA6Ly9jcmwubWlj
# cm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY1Jvb0NlckF1dF8yMDEwLTA2
# LTIzLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUHMAKGPmh0dHA6Ly93d3cu
# bWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljUm9vQ2VyQXV0XzIwMTAtMDYtMjMu
# Y3J0MIGgBgNVHSABAf8EgZUwgZIwgY8GCSsGAQQBgjcuAzCBgTA9BggrBgEFBQcC
# ARYxaHR0cDovL3d3dy5taWNyb3NvZnQuY29tL1BLSS9kb2NzL0NQUy9kZWZhdWx0
# Lmh0bTBABggrBgEFBQcCAjA0HjIgHQBMAGUAZwBhAGwAXwBQAG8AbABpAGMAeQBf
# AFMAdABhAHQAZQBtAGUAbgB0AC4gHTANBgkqhkiG9w0BAQsFAAOCAgEAB+aIUQ3i
# xuCYP4FxAz2do6Ehb7Prpsz1Mb7PBeKp/vpXbRkws8LFZslq3/Xn8Hi9x6ieJeP5
# vO1rVFcIK1GCRBL7uVOMzPRgEop2zEBAQZvcXBf/XPleFzWYJFZLdO9CEMivv3/G
# f/I3fVo/HPKZeUqRUgCvOA8X9S95gWXZqbVr5MfO9sp6AG9LMEQkIjzP7QOllo9Z
# Kby2/QThcJ8ySif9Va8v/rbljjO7Yl+a21dA6fHOmWaQjP9qYn/dxUoLkSbiOewZ
# SnFjnXshbcOco6I8+n99lmqQeKZt0uGc+R38ONiU9MalCpaGpL2eGq4EQoO4tYCb
# IjggtSXlZOz39L9+Y1klD3ouOVd2onGqBooPiRa6YacRy5rYDkeagMXQzafQ732D
# 8OE7cQnfXXSYIghh2rBQHm+98eEA3+cxB6STOvdlR3jo+KhIq/fecn5ha293qYHL
# pwmsObvsxsvYgrRyzR30uIUBHoD7G4kqVDmyW9rIDVWZeodzOwjmmC3qjeAzLhIp
# 9cAvVCch98isTtoouLGp25ayp0Kiyc8ZQU3ghvkqmqMRZjDTu3QyS99je/WZii8b
# xyGvWbWu3EQ8l1Bx16HSxVXjad5XwdHeMMD9zOZN+w2/XU/pnR4ZOC+8z1gFLu8N
# oFA12u8JJxzVs341Hgi62jbb01+P3nSISRIwggTYMIIDwKADAgECAhMzAAAArYAj
# N2XEWiNqAAAAAACtMA0GCSqGSIb3DQEBCwUAMHwxCzAJBgNVBAYTAlVTMRMwEQYD
# VQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24xJjAkBgNVBAMTHU1pY3Jvc29mdCBUaW1lLVN0YW1w
# IFBDQSAyMDEwMB4XDTE2MDkwNzE3NTY1NVoXDTE4MDkwNzE3NTY1NVowgbExCzAJ
# BgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25k
# MR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xDDAKBgNVBAsTA0FPQzEm
# MCQGA1UECxMdVGhhbGVzIFRTUyBFU046QzNCMC0wRjZBLTQxMTExJTAjBgNVBAMT
# HE1pY3Jvc29mdCBUaW1lLVN0YW1wIFNlcnZpY2UwggEiMA0GCSqGSIb3DQEBAQUA
# A4IBDwAwggEKAoIBAQDtsw3+ZRrdjbFV70cpvihU4xUCwcs6tloenowDcczbSqzn
# jVS/XqH6NQEUIvxck+9Hmpe46mrv0eqnqiW5R1zmyWvTtAeaBvLnsjTIixh72HW1
# eGlFDNdSIoyxEBKpuyZaPr9GUho5YUgLlFp4om6pKFxxQa0+g1aDJCyh1anwxb5v
# 3PbvElanMuvpy8epJH5Rm3n6B2u3JgoDMKyotjO6RK0aN4OXVyuEkGg0wYB/HlFg
# ydV+Ngqyqfk+tH633oT3m/FPqCYI5himKgJZcczBl/gpYnnX1s5Nds//wRUB5pOY
# YJHTagWJLCABh9wQPTrLdqyDnRAJ/IcNnK5cmf2nAgMBAAGjggEbMIIBFzAdBgNV
# HQ4EFgQUZCG6wC+s+JzHDJTl5x/JCta8/WswHwYDVR0jBBgwFoAU1WM6XIoxkPND
# e3xGG8UzaFqFbVUwVgYDVR0fBE8wTTBLoEmgR4ZFaHR0cDovL2NybC5taWNyb3Nv
# ZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWljVGltU3RhUENBXzIwMTAtMDctMDEu
# Y3JsMFoGCCsGAQUFBwEBBE4wTDBKBggrBgEFBQcwAoY+aHR0cDovL3d3dy5taWNy
# b3NvZnQuY29tL3BraS9jZXJ0cy9NaWNUaW1TdGFQQ0FfMjAxMC0wNy0wMS5jcnQw
# DAYDVR0TAQH/BAIwADATBgNVHSUEDDAKBggrBgEFBQcDCDANBgkqhkiG9w0BAQsF
# AAOCAQEAEoVHudSdmMR8ZG61qrhNU4oihtx2lXVdIo83dTxe9qVvOo0Zf7vqlDT+
# IKZqVDZPNFT/AAOVVCbzeWfv8cAY+um3MO13/2l02jOEEQ6Tr4EYxyVauIXWYKG0
# a1szo/60hhgk2CGVW+6QFcbZY+7UpNj55HrdS7JzrBa5oMAEFu81dTtQPPlhdtwB
# 6AsfZlAa3apZT0yD2MWHBEIid6jK902lrhqRxccdIHwhWHVTr08luuLMuHBw41ph
# XVys42v8jNOsOD9iPyZgw3mmlXX8j/zmLSyUfQLRG+7Df6TB2aRGIjfXr3+Yul73
# d5NdqwuvFFBO6WZBLznqhOMXeflcVKGCA3UwggJdAgEBMIHhoYG3pIG0MIGxMQsw
# CQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9u
# ZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMQwwCgYDVQQLEwNBT0Mx
# JjAkBgNVBAsTHVRoYWxlcyBUU1MgRVNOOkMzQjAtMEY2QS00MTExMSUwIwYDVQQD
# ExxNaWNyb3NvZnQgVGltZS1TdGFtcCBTZXJ2aWNloiUKAQEwCQYFKw4DAhoFAAMV
# AJwY5hrfGwOIF10cxscsS5qv4TDZoIHBMIG+pIG7MIG4MQswCQYDVQQGEwJVUzET
# MBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMV
# TWljcm9zb2Z0IENvcnBvcmF0aW9uMQwwCgYDVQQLEwNBT0MxJzAlBgNVBAsTHm5D
# aXBoZXIgTlRTIEVTTjoyNjY1LTRDM0YtQzVERTErMCkGA1UEAxMiTWljcm9zb2Z0
# IFRpbWUgU291cmNlIE1hc3RlciBDbG9jazANBgkqhkiG9w0BAQUFAAIFAN7VSU8w
# IhgPMjAxODA2MjAyMTQ0NDdaGA8yMDE4MDYyMTIxNDQ0N1owdjA8BgorBgEEAYRZ
# CgQBMS4wLDAKAgUA3tVJTwIBADAJAgEAAgF2AgH/MAcCAQACAhRyMAoCBQDe1prP
# AgEAMDYGCisGAQQBhFkKBAIxKDAmMAwGCisGAQQBhFkKAwGgCjAIAgEAAgMW42Ch
# CjAIAgEAAgMehIAwDQYJKoZIhvcNAQEFBQADggEBAHP/pxcLrTtRwX/jK3vv8Mae
# lKGq2s1223Des3oo/8N1BpEsSn1gnK19D9wkJnEISX7s30r6XT4jZCp55huh14Zt
# FmMSyxx+RBBYv6Q+r0G+hUV/I6ZHXDYw8B9sPwqPiWKqQXqzeuybJwpPffIhVFX0
# ar5QiGmttTnmgg9kdtL77Ve6wrGh+e39xG75aKq58KwwRTBIhqtCvi+KPQIYGibp
# K/6MpuS3AnixhJRvcylfF+hpi6Cn9s1ZB2yOw8A370QE+ATlCkX2IduoEcKnoitK
# DNvuR2EXjVCNHezQCOOp7468N4f0lgD1mRi9DBSkpxH7v/577RtKV1ex7FEB+3Ex
# ggL1MIIC8QIBATCBkzB8MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3Rv
# bjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0
# aW9uMSYwJAYDVQQDEx1NaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EgMjAxMAITMwAA
# AK2AIzdlxFojagAAAAAArTANBglghkgBZQMEAgEFAKCCATIwGgYJKoZIhvcNAQkD
# MQ0GCyqGSIb3DQEJEAEEMC8GCSqGSIb3DQEJBDEiBCBB/u+lJJF5JBqZVDUNDKTC
# MJCfbvfN+Ko9kgkNB1rsBDCB4gYLKoZIhvcNAQkQAgwxgdIwgc8wgcwwgbEEFJwY
# 5hrfGwOIF10cxscsS5qv4TDZMIGYMIGApH4wfDELMAkGA1UEBhMCVVMxEzARBgNV
# BAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jv
# c29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWljcm9zb2Z0IFRpbWUtU3RhbXAg
# UENBIDIwMTACEzMAAACtgCM3ZcRaI2oAAAAAAK0wFgQUhQMPVvxARDL3j8odnHso
# JiCI3TEwDQYJKoZIhvcNAQELBQAEggEAURJy3hzbgzNlU4zOzQNZbdsEC+CjE2BR
# JHaqJ2wn9E7W8rJn54r92hbXMb78ec7dgunxirJYYc2dNLr3BVxOBbX0VI9ubbAH
# gub1UJk5gqBn6xdqdd7BRk3rQJFKajAkwa7mYWUplYL1XwoWOSERWvieq5TCPj19
# 3kF0KBbFoM/my9JMb4bXcnTHgajm+bsypNpYjutVidhbovmdvyQYqrgBvJqfr7J9
# COJ7cIZb3Vp1ovsJuC9/EcCCzdb7gUzYKm3JDJ6I2XPVNvQoqisabVR4sOaYUFcG
# YIfuNyfh6TEW8dAyFMPGGZFAXAOS4CPSydxmHAMSHOQ3gonWguUw/A==
# SIG # End signature block
