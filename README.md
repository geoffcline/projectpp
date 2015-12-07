# projectpp

Microsoft .NET Framework 4.5 Readme

Date Published: September 28, 2012

For the latest version of the Readme click here


1. .Net Known Issues
1.1. NetFx Installation
1.2. NetFx Uninstallation
1.3. .Net Product Issues
1.3.1. General Issues
1.3.2. ASP.NET
1.3.3. Winforms
1.3.4. Parallel Programming
1.3.5. Managed Extensibility Framework
1.3.6. Entity Framework
1.3.7. LINQ to SQL
1.3.8. Windows Communication Foundation (WCF)
1.3.9. Windows Presentation Foundation (WPF)
1.3.10. Windows Workflow Foundation (WF)
2. .Net Maintenance
3. Related Links
1. .Net Known Issues

1.1 NetFx Installation

1.1.1 The .NET Framework 4.5 language packs are not available on Windows 8

The .NET Framework 4.5 language packs cannot be installed on the Windows 8 operating system, because the .NET Framework 4.5 is a component of the operating system. The .NET Framework 4.5 language packs can be installed on earlier supported versions of Windows.

To resolve this issue:

Download the language packs for the Windows 8 operating system, or install a localized version of Windows 8 to get the localized resources for the .NET Framework 4.5.

1.1.2 Failed or canceled .NET Framework 4.5 installation reverts application pools to 2.0

If you are upgrading from the .NET Framework 4 to the .NET Framework 4.5 RTM release, and if the installation is canceled or fails, the .NET Framework reverts to version 4. However, ASP.NET 4 is also unregistered and all the application pools in IIS are set to target the .NET Framework 2.0.

If this happens, browsing to a web application based on ASP.NET 4 results in a configuration error that reports that the "targetFramework" attribute of the "compilation" element is not recognized.

To resolve this issue:

To work around this issue:

Fix the incorrect registration by completing option A or option B below (you do not have to do both):
Reregister ASP.NET 4 by running the ASP.NET IIS Registration tool (aspnet_regiis) using the following command:
%windir%\Microsoft.Net\Framework(64)\v4.0.30319\aspnet_regiis.exe -iru -enable
-or-

Repair the .NET Framework 4 Extended Profile by rerunning the installation program and choosing the Repair option.

Additionally, if you have custom application pools that targeted ASP.NET 4 before the failed upgrade, use IIS Manager (inetmgr.exe) to manually reset them to target ASP.NET 4.
1.2 NetFx Uninstallation

1.2.1 ASP.NET 2.0 and 3.5 don’t work after you remove the .NET Framework 4.5 from Windows 8 or Windows Server 2012

On Windows 8 and Windows Server 2012, ASP.NET 2.0 and 3.5 require the ASP.NET 4.5 feature to be enabled. If you remove or disable the .NET Framework 4.5, your ASP.NET 2.0 and 3.5 applications will no longer run.

To resolve this issue:

On Windows 8

Enable the ASP.NET 4.5 feature in Control Panel:

Open Control Panel.
Choose Programs.
Under the Programs and Features heading, choose Turn Windows features on or off.
Expand the node .NET Framework 4.5 Advanced Services.
Select the ASP.NET 4.5 check box.
Choose OK.
On Windows Server 2012

See IIS 8.0 Using ASP.NET 3.5 and ASP.NET 4.5 in the IIS Learning Center.

1.3 .Net Product Issues

1.3.1 General Issues

There are no known issues.

1.3.2 ASP.NET

There are no known issues.

1.3.3 Winforms

There are no known issues.

1.3.4 Parallel Programming

There are no known issues.

1.3.5 Managed Extensibility Framework

There are no known issues.

1.3.6 Entity Framework

There are no known issues.

1.3.7 LINQ to SQL

There are no known issues.

1.3.8 Windows Communication Foundation (WCF)

1.3.8.1 Problems running existing XML serialization code in WCF 4.5

In Windows Communication Foundation (WCF) 4.5, the XmlSerializer class was optimized to remove its dependency on the C# compiler. This change provides significant performance gains for cold startup scenarios. However, it may cause problems in XML serialization code that was compiled in WCF 4 but is running against WCF 4.5.

To resolve this issue:

If you encounter any problems running your existing XML serialization code in WCF 4.5, use the following configuration element to revert to the XmlSerializer behavior in WCF 4:

<configuration>
   <system.xml.serialization>
      <xmlSerializer useLegacySerializerGeneration="true"/>
   </system.xml.serialization>
</configuration>

1.3.9 Windows Presentation Foundation (WPF)

There are no known issues.

1.3.10 Windows Workflow Foundation (WF)

1.3.10.1 Host fails to start if you use workflows with the Workflow Identity parameter

The .NET Framework 4.5 includes a new workflow parameter, Workflow Identity, that is persisted to the SQL Workflow Instance Store. If you use workflows that include this parameter and its value is not null, you must update the Instance Store so it can store the value. Otherwise, the host will fail to start.

To resolve this issue:

Update the SQL Workflow Instance Store by running the script at the following location:

%windir%\Microsoft.NET\Framework\<version>\SQL\<language>\SqlWorkflowInstanceStoreSchemaUpgrade.sql

1.3.10.2 Some of the new features in the Workflow Designer may cause issues with existing solutions

In the .NET Framework 4.5, the Workflow Designer includes the following changes:

The shortcut menu is now available when multiple activities are selected.
In an activity designer, the area that can be dragged is now the header of the designer instead of being the whole designer. In the FlowDecision and FlowSwitch designers, the area that can be dragged is still the whole designer.
Behavior change for multi-selection with Ctrl key pressed: In the .NET Framework 4, the selection is canceled on mouse down. In the .NET Framework 4.5 RC, the selection is canceled on mouse up. Selection still happens on mouse down.
Activity designers now support activity delegates. For more information, see "How To: Define and consume activity delegates in the Workflow Designer" in the MSDN Library.
The variable and argument designers now support the shortcut menu. The shortcut menu has items for deleting variables and arguments, and working with annotations. For more information, see "How To: Add comments to a workflow in the Workflow Designer" in the MSDN Library.
Previously, the MetadataStore class did not support adding an attribute on a generic property of a generic type definition. This is now supported.
The ModelItemDictionary class now supports a null key.
Previously, the ModelItem.Parents property contained items that were not in the model tree. Now, all objects in ModelItem.Parents are valid objects that are in the model tree.
Previously, when a Case was selected in the designer for the Switch activity, the keyboard focus went to the activity in the selected Case, and the activity was highlighted. Now, the activity is highlighted, but the keyboard focus does not change.
Previously, if just one character was entered in an expression text box, the designer treated the value as a constant of type Char. Now, the expression is treated as a variable or argument name.
Previously, the type browser showed types in referenced assemblies that had dependencies on unreferenced assemblies. Now, types that have dependencies on unreferenced assembles do not appear until the dependent assemblies are referenced.
The following types and members are now obsolete:
Most of the properties in the System.Activities.Presentation.Services.ModelChangedEventArgs class are obsolete. Use the ModelChangeInfo property instead of the obsolete properties.
In the System.Activities.Presentation.DragDropHelper class, the following methods have been made obsolete by new multi-select features:
DoDragMove(WorkflowViewElement, Point) - Use DoDragMove(IEnumerable<WorkflowViewElement>, Point) instead.
GetDroppedObject(DependencyObject, DragEventArgs, EditingContext) - Use GetDroppedObjects(DependencyObject, DragEventArgs, EditingContext) instead.
GetDraggedModelItem(DragEventArgs) - Use GetDraggedModelItems(DragEventArgs) instead.
GetCompositeView(DragEventArgs) - Use GetCompositeView(WorkflowViewElement) instead.
SetDragDropCompletedEffects(DragEventArgs, DragDropEffects) - Use SetDragDropMovedViewElements(DragEventArgs, IEnumerable<WorkflowViewElement>) instead.
GetDragDropCompletedEffects(DataObject) - Use GetDragDropMovedViewElements(DataObject) instead.
To resolve this issue:

Use the workaround for each issue discussed in the preceding list.

2. .Net Maintenance

2..1 Upgrading to Windows 8 does not update .NET Framework 4 language packs

If you upgrade from the Windows 7 operating system to Windows 8, the .NET Framework 4 language packs that you previously installed on your computer are not removed or updated. This issue affects language packs that do not match the language of the upgraded Windows 8, not including English. For example, if you have an English edition of Windows 7 Ultimate and the .NET Framework 4 German language pack, and you upgrade your system to the English edition of Windows 8, the language pack will remain on your system but will not be updated to the .NET Framework 4.5.

To resolve this issue:

Either uninstall the .NET Framework 4 language pack before you upgrade to Windows 8, or install the appropriate Windows 8 language pack after you upgrade.

3. Related Links

© 2012 Microsoft Corporation. All rights reserved.

Terms of Use | Trademarks | Privacy Statement