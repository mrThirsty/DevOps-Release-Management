# DevOps Release Management

This is a simple application to enable teams to manage approvals for components in Azure DevOps pipelines. While you can use Pre-approvals for stages it does stop downstream items from running in your pipeline. This tool will enable you to configure the components you have in your pipeline and then create a release. For each release you can get the approval state for a component then use that true/false value to allow individual tasks to run. 

### Current state of play:
 - Uses Azure AD for authentication.
 - Configuration of user groups used in Azure AD down in json.
 - No direct integration with DevOps
 - To consume from pipeline you must write your own task to talk to the API to get a component state for a release.

 ### Future Development:
 - Implement a first run configuration page to get all configuration items required
 - Enable authentication mechanism choice: Azure AD or in-App (user/password) authentication.
 - Hooks into Azure DevOps to detect the creation of a new release
    - Release Created
    - Release Deployment Started
    - Release Completed
 - Create a task for Azure DevOps pipelines that will get the components approved for the release and store in pipeline variables.
 - Notification system to users, first point is when a new release is added and approvals are required.
 - Move away from the default Blazor look and feel.

### Getting Involved
This project is built using .net 5 core and Blazor Server side. You will need one of the following depending on your hardware:
 - Visual Studio Code with C# extension
 - Visual Studio 2019
 - Visual Studio for Mac

 Once you have the repo forked and running on your local machine, you will need to configure your own instance of Azure AD and two Security Groups for for Admin and normal users, these ID's you will need to put into the appsettings.json. You will also need to configure an App registration and client secret in your Azure AD to enable the application to talk to it, this must also be stored in the appsettings.json.

 When raising a pull request to merge your code in, do not include your appsettings.json or the SQLite DB. if you need to have new items added to the appsettings.json then add them in the PR notes. The migrations will take care of the DB changes.

