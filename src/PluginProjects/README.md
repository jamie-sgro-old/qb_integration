# PluginProjects

## Purpose

This folder is dedicated to developing plugins for the main project that are included as .dll files to be consumed by MEF.



## Implementation

To create a new plugin:

1. Right click the Solutions 'src'

   1. Select Add... New Project...
   2. Use the Class Library (.NET Framework) template, select 'Next'
   3. Under 'Project name' type a meaningful plugin name that outlines the plugins general purpose
   4. Change the default location to route to .\qb_integration\src\\**PluginProjects**

2. In Solution Explorer, right click the new project

   1. Under 'Build' select navigate to the 'Configuration:' dropdown and select '**Debug**'
      1. Overwrite the 'Output path:' for the 'Debug' configuration to the following:
         1. ..\..\WPFDesktopUI\bin\x86\Release\Plugins\
   2. Under 'Build' select navigate to the 'Configuration:' dropdown and select '**Release**'
      1. Overwrite the 'Output path:' for the 'Release' configuration to the following:
         1. ..\..\WPFDesktopUI\bin\x86\Release\Plugins\

   