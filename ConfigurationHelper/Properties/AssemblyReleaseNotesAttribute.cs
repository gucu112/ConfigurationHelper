using System;

namespace Gucu112.ConfigurationHelper.Properties
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class AssemblyReleaseNotesAttribute : Attribute
    {
        public AssemblyReleaseNotesAttribute(string releaseNotes) : base()
        {
            ReleaseNotes = releaseNotes;
        }

        public string ReleaseNotes { get; }
    }
}
