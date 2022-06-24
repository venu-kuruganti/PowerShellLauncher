using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Configuration;
using Windows.Storage;

string GameName = ApplicationData.Current.LocalSettings.Values["gamename"] as string;
string path = ConfigurationSettings.AppSettings["MameDirectoryLocation"].ToString().Trim();

//create runspace
Runspace runspace = RunspaceFactory.CreateRunspace();

//Open it
runspace.Open();

//Create a pipeline

Pipeline pipeline = runspace.CreatePipeline();
pipeline.Commands.AddScript("Set-ExecutionPolicy -Scope Process -ExecutionPolicy Unrestricted");
pipeline.Commands.AddScript("$myCommand = \"cd " + path + "\"");
pipeline.Commands.AddScript("iex $myCommand");
pipeline.Commands.AddScript("$runGameCommand = \"./mame.exe \'" + GameName + "' -plugin autofire\"");
pipeline.Commands.AddScript("iex $runGameCommand");        


try
{
    pipeline.Invoke();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();

}


//Close runspace
runspace.Close();


/*
 *$myCommand = "cd C:\Users\venuk\Documents\Personal\Games\MAME"
iex $myCommand

$runGameCommand = "./mame.exe '" + $args[0] + "' -plugin autofire"
iex $runGameCommand
 
 */





