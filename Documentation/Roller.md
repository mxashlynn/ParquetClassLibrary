<a name='assembly'></a>
# roller

## Contents

- [Command](#T-ParquetRoller-Roller-Command 'ParquetRoller.Roller.Command')
- [Roller](#T-ParquetRoller-Roller 'ParquetRoller.Roller')
  - [DefaultMessage](#P-ParquetRoller-Roller-DefaultMessage 'ParquetRoller.Roller.DefaultMessage')
  - [HelpMessage](#P-ParquetRoller-Roller-HelpMessage 'ParquetRoller.Roller.HelpMessage')
  - [ListPropertyForCategory](#P-ParquetRoller-Roller-ListPropertyForCategory 'ParquetRoller.Roller.ListPropertyForCategory')
  - [VersionMessage](#P-ParquetRoller-Roller-VersionMessage 'ParquetRoller.Roller.VersionMessage')
  - [CreateTemplates(inWorkload)](#M-ParquetRoller-Roller-CreateTemplates-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.CreateTemplates(ParquetClassLibrary.ModelCollection)')
  - [DisplayBadArguments(inWorkload)](#M-ParquetRoller-Roller-DisplayBadArguments-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.DisplayBadArguments(ParquetClassLibrary.ModelCollection)')
  - [DisplayDefault(inWorkload)](#M-ParquetRoller-Roller-DisplayDefault-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.DisplayDefault(ParquetClassLibrary.ModelCollection)')
  - [DisplayHelp(inWorkload)](#M-ParquetRoller-Roller-DisplayHelp-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.DisplayHelp(ParquetClassLibrary.ModelCollection)')
  - [DisplayVersion(inWorkload)](#M-ParquetRoller-Roller-DisplayVersion-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.DisplayVersion(ParquetClassLibrary.ModelCollection)')
  - [ListCollisions(inWorkload)](#M-ParquetRoller-Roller-ListCollisions-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.ListCollisions(ParquetClassLibrary.ModelCollection)')
  - [ListMaxIDs(inWorkload)](#M-ParquetRoller-Roller-ListMaxIDs-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.ListMaxIDs(ParquetClassLibrary.ModelCollection)')
  - [ListNames(inWorkload)](#M-ParquetRoller-Roller-ListNames-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.ListNames(ParquetClassLibrary.ModelCollection)')
  - [ListPronouns(inWorkload)](#M-ParquetRoller-Roller-ListPronouns-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.ListPronouns(ParquetClassLibrary.ModelCollection)')
  - [ListRanges(inWorkload)](#M-ParquetRoller-Roller-ListRanges-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.ListRanges(ParquetClassLibrary.ModelCollection)')
  - [ListTags(inWorkload)](#M-ParquetRoller-Roller-ListTags-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.ListTags(ParquetClassLibrary.ModelCollection)')
  - [Main(args)](#M-ParquetRoller-Roller-Main-System-String[]- 'ParquetRoller.Roller.Main(System.String[])')
  - [ParseCategory(inCategory)](#M-ParquetRoller-Roller-ParseCategory-System-String- 'ParquetRoller.Roller.ParseCategory(System.String)')
  - [ParseCommand(inCommandText)](#M-ParquetRoller-Roller-ParseCommand-System-String- 'ParquetRoller.Roller.ParseCommand(System.String)')
  - [ParseProperty(inProperty)](#M-ParquetRoller-Roller-ParseProperty-System-String- 'ParquetRoller.Roller.ParseProperty(System.String)')
  - [RollCSVs(inWorkload)](#M-ParquetRoller-Roller-RollCSVs-ParquetClassLibrary-ModelCollection- 'ParquetRoller.Roller.RollCSVs(ParquetClassLibrary.ModelCollection)')

<a name='T-ParquetRoller-Roller-Command'></a>
## Command `type`

##### Namespace

ParquetRoller.Roller

##### Summary

Represents an action that the user may ask [Roller](#T-ParquetRoller-Roller 'ParquetRoller.Roller') to perform.

##### Returns

A value indicating success or the manner of failure.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [T:ParquetRoller.Roller.Command](#T-T-ParquetRoller-Roller-Command 'T:ParquetRoller.Roller.Command') | The [ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') to act on, if any. |

<a name='T-ParquetRoller-Roller'></a>
## Roller `type`

##### Namespace

ParquetRoller

##### Summary

A command line tool that reads in game definitions from CSV files, verifies, modifies, and writes them out.

<a name='P-ParquetRoller-Roller-DefaultMessage'></a>
### DefaultMessage `property`

##### Summary

What to display when roller is started without any arguments.

<a name='P-ParquetRoller-Roller-HelpMessage'></a>
### HelpMessage `property`

##### Summary

A detailed help message explaining how to use roller.

<a name='P-ParquetRoller-Roller-ListPropertyForCategory'></a>
### ListPropertyForCategory `property`

##### Summary

A flag indicating that a subcommand must be executed.

<a name='P-ParquetRoller-Roller-VersionMessage'></a>
### VersionMessage `property`

##### Summary

Displays roller's current version, and various library-related version strings.

<a name='M-ParquetRoller-Roller-CreateTemplates-ParquetClassLibrary-ModelCollection-'></a>
### CreateTemplates(inWorkload) `method`

##### Summary

Write CSV templates to current directory.

##### Returns

A value indicating success or the nature of the failure.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | Ignored. |

<a name='M-ParquetRoller-Roller-DisplayBadArguments-ParquetClassLibrary-ModelCollection-'></a>
### DisplayBadArguments(inWorkload) `method`

##### Summary

Displays the help message to the user, also indicating that the arguments given were not understood.

##### Returns

[BadArguments](#F-ParquetRoller-ExitCode-BadArguments 'ParquetRoller.ExitCode.BadArguments')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | Ignored. |

<a name='M-ParquetRoller-Roller-DisplayDefault-ParquetClassLibrary-ModelCollection-'></a>
### DisplayDefault(inWorkload) `method`

##### Summary

Displays the default message to the user.

##### Returns

[Success](#F-ParquetRoller-ExitCode-Success 'ParquetRoller.ExitCode.Success')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | Ignored. |

<a name='M-ParquetRoller-Roller-DisplayHelp-ParquetClassLibrary-ModelCollection-'></a>
### DisplayHelp(inWorkload) `method`

##### Summary

Displays a detailed help message to the user.

##### Returns

[Success](#F-ParquetRoller-ExitCode-Success 'ParquetRoller.ExitCode.Success')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | Ignored. |

<a name='M-ParquetRoller-Roller-DisplayVersion-ParquetClassLibrary-ModelCollection-'></a>
### DisplayVersion(inWorkload) `method`

##### Summary

Displays version information to the user.

##### Returns

[Success](#F-ParquetRoller-ExitCode-Success 'ParquetRoller.ExitCode.Success')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | Ignored. |

<a name='M-ParquetRoller-Roller-ListCollisions-ParquetClassLibrary-ModelCollection-'></a>
### ListCollisions(inWorkload) `method`

##### Summary

If more than one unique [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model') uses the same [Name](#P-ParquetClassLibrary-Model-Name 'ParquetClassLibrary.Model.Name'), lists that as a name collision.

##### Returns

[BadArguments](#F-ParquetRoller-ExitCode-BadArguments 'ParquetRoller.ExitCode.BadArguments')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | The [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s to inspect. |

<a name='M-ParquetRoller-Roller-ListMaxIDs-ParquetClassLibrary-ModelCollection-'></a>
### ListMaxIDs(inWorkload) `method`

##### Summary

Lists the largest [ModelID](#T-ParquetClassLibrary-ModelID 'ParquetClassLibrary.ModelID') actually in use in each of the given categories of [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s.

##### Returns

[Success](#F-ParquetRoller-ExitCode-Success 'ParquetRoller.ExitCode.Success')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | The [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s to inspect. |

<a name='M-ParquetRoller-Roller-ListNames-ParquetClassLibrary-ModelCollection-'></a>
### ListNames(inWorkload) `method`

##### Summary

Lists every unique [Name](#P-ParquetClassLibrary-Model-Name 'ParquetClassLibrary.Model.Name') in use in each of the given [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s.

##### Returns

[Success](#F-ParquetRoller-ExitCode-Success 'ParquetRoller.ExitCode.Success')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | The [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s to inspect. |

<a name='M-ParquetRoller-Roller-ListPronouns-ParquetClassLibrary-ModelCollection-'></a>
### ListPronouns(inWorkload) `method`

##### Summary

List all defined pronoun groups.

##### Returns

A value indicating success or the nature of the failure.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | Ignored. |

<a name='M-ParquetRoller-Roller-ListRanges-ParquetClassLibrary-ModelCollection-'></a>
### ListRanges(inWorkload) `method`

##### Summary

Lists the defined ranges for the given [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s' [ModelID](#T-ParquetClassLibrary-ModelID 'ParquetClassLibrary.ModelID')s.

##### Returns

[Success](#F-ParquetRoller-ExitCode-Success 'ParquetRoller.ExitCode.Success')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | The [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s to inspect. |

<a name='M-ParquetRoller-Roller-ListTags-ParquetClassLibrary-ModelCollection-'></a>
### ListTags(inWorkload) `method`

##### Summary

Lists every unique [ModelTag](#T-ParquetClassLibrary-ModelTag 'ParquetClassLibrary.ModelTag') in use in each of the given [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s.

##### Returns

[BadArguments](#F-ParquetRoller-ExitCode-BadArguments 'ParquetRoller.ExitCode.BadArguments')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | The [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s to inspect. |

<a name='M-ParquetRoller-Roller-Main-System-String[]-'></a>
### Main(args) `method`

##### Summary

A command line tool for working with Parquet configuration files.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Command line arguments passed in to the tool. |

<a name='M-ParquetRoller-Roller-ParseCategory-System-String-'></a>
### ParseCategory(inCategory) `method`

##### Summary

Takes a single argument corresponding to the "category" selection and determines which workload it corresponds to.

##### Returns

A collection of [Model](#T-ParquetClassLibrary-Model 'ParquetClassLibrary.Model')s to take action on.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inCategory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The third command line argument. |

<a name='M-ParquetRoller-Roller-ParseCommand-System-String-'></a>
### ParseCommand(inCommandText) `method`

##### Summary

Takes a single argument corresponding to the "command" selection and determines which command it corresponds to.

##### Returns

An action for [Roller](#T-ParquetRoller-Roller 'ParquetRoller.Roller') to take.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inCommandText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The first command line argument. |

<a name='M-ParquetRoller-Roller-ParseProperty-System-String-'></a>
### ParseProperty(inProperty) `method`

##### Summary

Takes a single argument corresponding to the "property" selection and determines which subcommand it corresponds to.

##### Returns

An action for [Roller](#T-ParquetRoller-Roller 'ParquetRoller.Roller') to take.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inProperty | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The second command line argument. |

<a name='M-ParquetRoller-Roller-RollCSVs-ParquetClassLibrary-ModelCollection-'></a>
### RollCSVs(inWorkload) `method`

##### Summary

Prepare CSVs in current directory for use.

##### Returns

A value indicating success or the nature of the failure.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| inWorkload | [ParquetClassLibrary.ModelCollection](#T-ParquetClassLibrary-ModelCollection 'ParquetClassLibrary.ModelCollection') | Ignored. |