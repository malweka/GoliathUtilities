using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Goliath.BuildTasks
{
    public class AssignBuildVersion : Task
    {
        /// <summary>
        /// Gets or sets the version file.
        /// </summary>
        /// <value>
        /// The version file.
        /// </value>
        public string AssemblyVersionFile { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>
        /// The version number.
        /// </value>
        [Output]
        public string VersionNumber { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether [automatic increment].
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if [automatic increment]; otherwise, <c>false</c>.
        ///// </value>
        //public bool AutoIncrement { get; set; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.Normal, "Accessing {0} to increment version number.", AssemblyVersionFile);
            if (!File.Exists(AssemblyVersionFile))
            {
                Log.LogWarning("Assembly version file [{0}] was not found", AssemblyVersionFile);
                return false;
            }

            string versionNumber = string.Empty;
            bool readAssemblyVersion = false;

            if (!string.IsNullOrEmpty(VersionNumber))
            {
                versionNumber = VersionNumber;
            }
            else
            {
                readAssemblyVersion = true;
                Log.LogMessage(MessageImportance.Normal, "Version number was not provided. The version will be auto-incremented.");
            }

            var fileLines = new List<string>();

            //we're going to auto increment;
            using (var sr = new StreamReader(AssemblyVersionFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("AssemblyVersion") || line.Contains("AssemblyFileVersion"))
                    {
                        if (readAssemblyVersion)
                        {
                            versionNumber = line.Substring(line.IndexOf('"') + 1);
                            versionNumber = versionNumber.Remove(versionNumber.IndexOf('"'));

                            var parts = versionNumber.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            var build = parts[parts.Length - 1];
                            int buildNumber;
                            Log.LogMessage(MessageImportance.Normal, "build number {0}", build);
                            if (int.TryParse(build, out buildNumber))
                            {
                                buildNumber = buildNumber + 1;
                                parts[parts.Length - 1] = buildNumber.ToString();
                                var newVersion = string.Join(".", parts);
                                line = line.Replace(versionNumber, newVersion);
                                Log.LogMessage(MessageImportance.Normal, "Current version number is {0}", newVersion);
                                VersionNumber = newVersion;
                            }
                        }
                        else
                        {
                            var oldVersion = line.Substring(line.IndexOf('"') + 1);
                            oldVersion = oldVersion.Remove(oldVersion.IndexOf('"'));
                            line = line.Replace(oldVersion, versionNumber);
                        }
                    }


                    fileLines.Add(line);
                }

            }

            using (var file = new System.IO.StreamWriter(AssemblyVersionFile))
            {
                foreach (var fileLine in fileLines)
                {
                    file.WriteLine(fileLine);
                }
            }


            return true;
        }
    }
}
