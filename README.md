# corrigeAdobe
Small app to solve a specific problem within an organization with Windos 10 workstations

This app solves 2 problems within an organization.

## Problem 1
Unnecessary window prompts about accessibility.

To solve this problem the app uses methods from the System.IO namespace

Because we know wich files need to be delleted an array of string is created with the names of the files.

After checking if the files exists it is deleted.



## Problem 2
Constant Adobe Reader process not responding and using an excessive amount of ressources.

After various ocassions where Adobe Reader would stop working we found that it was caused by 2 optios in advanced settings, ProtectedMode and EnhancedSecurityStandAlone.

2 regestry key are added via CMD commands by using methods from System.Diagnostics namespace.

There is o need to check if the regestry key already exists because if it does it is going to get overriten with the values that we provide.

