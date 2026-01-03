using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/**
 * @brief An editor script to synchornize the samples we make to the actual samples folder as we develop.
 */
public class SyncSamples : Editor
{
    // Where we create the samples (that is, where they copy from)
    private static readonly string sourcePath = "Assets/BJSamples";
    // Where the samples are copied to (in this case, within the package)
    private static readonly string destinationPath = "Packages/blueberry-jam-core/Samples~";

    /**
     * @brief Provides manual access to synchronize the samples from the toolbar.
     */
    [MenuItem("Blueberry Jam/Sync Samples")]
    public static void SyncSampleFolders()
    {
        EditorUtility.DisplayProgressBar("Synchronizing Samples", "Copying samples from the project working directory to teh packages sample directory.", 0.1f);
        if (Directory.Exists(sourcePath))
        {
            Debug.Log("Starting samples synchronization.");
            CopyNewOrUpdatedFiles(new DirectoryInfo(sourcePath), new DirectoryInfo(destinationPath));
            AssetDatabase.Refresh();
            Debug.Log("Samples synchronized successfully.");
        }
        else
        {
            Debug.LogError("Source path does not exist: " + sourcePath);
        }
        EditorUtility.ClearProgressBar();
    }

    /**
     * @brief Triggered on project starts, preemptively runs a sample synchronization and then sets up a listener to synchronize again on project close.
     */
    [InitializeOnLoadMethod]
    private static void OnProjectLoaded()
    {
        Debug.Log("Samples synchronizing on project open.");
        SyncSampleFolders();
        EditorApplication.quitting += SyncSampleFoldersOnClose;
    }

    /**
     * @brief Listens for the project closing and performs a sample sync to make sure all development work is captured.
     */
    private static void SyncSampleFoldersOnClose()
    {
        SyncSampleFolders();
        Debug.Log("Samples synchronized on project close.");
    }

    /**
     * @brief Copies only the files that are new or have changed in source to target.
     * @param source The source directory to copy data from.
     * @param target The destination directory to copy data to.
     */
    private static void CopyNewOrUpdatedFiles(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);

        // Copy each file into the new directory if it is new or has been modified.
        foreach (FileInfo sourceFile in source.GetFiles())
        {
            string targetFilePath = Path.Combine(target.FullName, sourceFile.Name);
            FileInfo targetFile = new FileInfo(targetFilePath);

            if (!targetFile.Exists || sourceFile.LastWriteTime > targetFile.LastWriteTime)
            {
                sourceFile.CopyTo(targetFilePath, true);
            }
        }

        // Copy each subdirectory using recursion if it is new or has been modified.
        foreach (DirectoryInfo sourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(sourceSubDir.Name);
            CopyNewOrUpdatedFiles(sourceSubDir, nextTargetSubDir);
        }
    }
}
