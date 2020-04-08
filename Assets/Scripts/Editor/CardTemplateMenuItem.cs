using UnityEditor;

// https://forum.unity.com/threads/how-to-create-your-own-c-script-template.459977/#post-5365599
public class CardTemplateMenuItem
{
    private const string pathToYourScriptTemplate = "Assets/Scripts/CardStuff/CardTemplate.cs.txt";
 
    [MenuItem(itemName: "Assets/Create/Card Game Stuff/New Card (C# Script)", isValidateFunction: false, priority: 51)]
    public static void CreateScriptFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToYourScriptTemplate, "CardTitle.cs");
    }
}