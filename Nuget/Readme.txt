To be able to run the Create-NugetPackage script you need to download the Nuget commandline tool from here:

  https://dist.nuget.org/index.html

Place it "somewhere good" and add that location to your path variable (what Nuget says you should do).


-----------------------------==-----------------------------------------


Before pushing a package to the Nuget gallery you'll need to set your API-key using the following command:

  nuget setApiKey <API_KEY> 

The api key is found in the Mir Software Nuget account on www.nuget.org.
 
For more info go to: https://docs.nuget.org/consume/command-line-reference#user-content-setapikey-command


-----------------------------==-----------------------------------------


The Nuget package Explorer is a tool that can be used to inspect Nuget packages. Find it here:

  https://npe.codeplex.com/