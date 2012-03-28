# CONSTANT CONTACT - UPLOAD CONTACT FORM - ASP Application #


1. ABOUT THIS PACKAGE

This is an ASP sample application that allows you to:
	- add a new Contact 
	- edit an existing Contact and update its profile
	- list all existing account Contacts
	- remove an existing Contact from all lists
	- unsubscribe an existing Contact


2. WHAT THIS PACKAGE CONTAINS 

Below is a list of files released as part of this build.

	[BaseFolder]\Development - Contains the source code of the ConstantContactBO.DLL and ConstantContactUtility.DLL
	[BaseFolder]\Development\ConstantContactBO - Provides access to the following .NET wrapper objects: Contact Lists and Contacts.
	[BaseFolder]\Development\ConstantContactUtility - Provides access to utility methods to create a new Contact, update an existing one, search Contact by email, etc.
	[BaseFolder]\Development\SolutionInfo.cs - Stores information about the ConstantContactBO.DLL and ConstantContactUtility.DLL assemblies (Assembly Company, Assemby Product, Assembly Copyright and Assemby Trademark)
	[BaseFolder]\Development\SolutionVersionInfo.cs - Stores information about the ConstantContactBO.DLL and ConstantContactUtility.DLL assemblies (Assembly Version [v1.0.0.0] and Assembly File Version [v1.0.0.0])
	[BaseFolder]\output\Debug\ConstantContactBO.dll - Output for the ConstantContactBO class library project
	[BaseFolder]\output\Debug\ConstantContactUtility.dll - Output for the ConstantContactUtility class library project
	[BaseFolder]\Release\Setup - Contains the setup to install the sample
	[BaseFolder]\Web - Contains the source code for the ASP web application
	[BaseFolder]\Web\UploadContactForm\App_Code\ConstantContact.cs - This class contains API Key, Username and Password used to acces Constant resources. Also, it contains definition for State/Province and Country
	[BaseFolder]\Web\UploadContactForm\AddContactConfirmation.aspx - Contact added confirmation page
	[BaseFolder]\Web\UploadContactForm\AddContactConfirmation.aspx.cs - Code behind of the Contact added confirmation page
	[BaseFolder]\Web\UploadContactForm\AddContactFullForm.aspx - Page that allows to add a new Contact using the full form
	[BaseFolder]\Web\UploadContactForm\AddContactFullForm.aspx.cs - Code behind of the add new Contact page using the full form
	[BaseFolder]\Web\UploadContactForm\AddContactSmallForm.aspx - Page that allows to add a new Contact using the simplified form
	[BaseFolder]\Web\UploadContactForm\AddContactSmallForm.aspx.cs  - Code behind of the add new Contact page using the simplified form
	[BaseFolder]\Web\UploadContactForm\ConstantContactLogo.jpg - Constant Contact logo
	[BaseFolder]\Web\UploadContactForm\Default.aspx	- Main page of the sample
	[BaseFolder]\Web\UploadContactForm\Default.aspx.cs - Code behind of the main page
	[BaseFolder]\Web\UploadContactForm\ListContacts.aspx - Page that allows to list all existing Contacts
	[BaseFolder]\Web\UploadContactForm\ListContacts.aspx.cs - Code behind of the list all existing Contacts
	[BaseFolder]\Web\UploadContactForm\Master.master - Master page that displayes the menu and the company logo
	[BaseFolder]\Web\UploadContactForm\Master.master.cs	- Code behind of the master page
	[BaseFolder]\Web\UploadContactForm\SearchContact.aspx - Page that allows users to search for an existing Contact
	[BaseFolder]\Web\UploadContactForm\SearchContact.aspx.cs - Code behind of the search page
	[BaseFolder]\Web\UploadContactForm\UpdateContactConfirmationEnd.aspx - Contact updated confirmation page
	[BaseFolder]\Web\UploadContactForm\UpdateContactConfirmationEnd.aspx.cs - Code behind of the Contact updated configuration page
	[BaseFolder]\Web\UploadContactForm\UpdateContactFullForm.aspx	-	Page that allows to update an existing Contact using the full form
	[BaseFolder]\Web\UploadContactForm\UpdateContactFullForm.aspx.cs - Code behind of the update page
	[BaseFolder]\Web\UploadContactForm\UpdateContactSmallForm.aspx	-	Page that allows to update an existing Contact using the simplified form
	[BaseFolder]\Web\UploadContactForm\UpdateContactSmallForm.aspx.cs - Code behind of the update simplified page
	[BaseFolder]\Web\UploadContactForm\Web.Config - Configuration file that exposes the APIKey, Username and Password used to access Constant Contact REST API			
	[BaseFolder]\ASPSample.sln - Visual Studio 2005 solution project
			

3. VERSION AND CHANGES

Version number: 1.1
Release date: 10.28.2009 10:05
Changes: Updated Utility class to correctly use the Link node for creating the ID tag and Update URIs.
Bug Fix: Fixed a problem which would cause usernames with special characters to not be able to update contact resources