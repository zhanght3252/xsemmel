﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using PropertyTools.DataAnnotations;
using Category = PropertyTools.DataAnnotations.CategoryAttribute;
using DisplayName = PropertyTools.DataAnnotations.DisplayNameAttribute;
using Browsable = PropertyTools.DataAnnotations.BrowsableAttribute;

namespace XSemmel.Configuration
{

    public class ConfigObj
    {

        public enum OpenType
        {
            Nothing,
            LastOpenedFile,
            XmlInClipboard
        }

        public class XsdMapItem : INotifyPropertyChanged
        {
            public string Name { get; set; }
            public string Mapping { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

//        private string _fileEncoding = "UTF-8";

        private ushort _fontSize = 13;
        private bool _enableCodeCompletion = true;
        private const int MAX_RECENTLY_USED_FILES = 5;
        private readonly List<string> _recentlyUsedFiles = new List<string>(); //cannot use LinkedList due to serialization


        [Category("General|Application")]
        public bool ShowSplashScreen { get; set; } = false;

        [Category("General|Application")]
        [DisplayName("Replace \"?> by \" ?> as workaround for UTF8-BOM files: ")]
        public bool WorkaroundUtf8BomBug { get; set; } = true;

        [Category("Schema|XSD mapping")]
//        [DisplayName(" ")]
//        [Column(0, "Name", "Replace", null, "1*", 'L')]  //TODO bug in PropertyTools prevents to show this columnname correctly
//        [Column(1, "Mapping", "by", null, "2*", 'R')]
        [HeaderPlacement(HeaderPlacement.Collapsed)]
        public List<XsdMapItem> XsdMapping { get; set; } = new List<XsdMapItem>();


        [Category("Schema|XSD mapping")]
        [DisplayName(" ")]
        [Comment]
        [XmlIgnore]
        [HeaderPlacement(HeaderPlacement.Collapsed)]
        public string Comment
        {
            get { return "All xsd's with the specified name will be replaced by the path of the mapping"; }
            set { string dummy = value; }  //needed becuase ShowReadOnlyProperties="False"
        }

        [Category("General|Application")]
        public bool WatchCurrentFileForChanges { get; set; } = true;

        [Category("General|Application")]
        public bool AlwaysPrettyprintFragments { get; set; } = true;

        [Category("General|Editor")]
        public ushort FontSize
        {
            get 
            {
                if (_fontSize < 3) return 3;
                return _fontSize;
            }
            set 
            {
                _fontSize = value; 
            }
        }

        [Category("General|Editor")]
        public bool EnableCodeCompletion
        {
            get { return _enableCodeCompletion; }
            set { _enableCodeCompletion = value; }
        }

        [Category("General|Editor")]
        public Encoding Encoding
        {
            get { return new UTF8Encoding(false); }
        }

        [Browsable(false)]
        public string LastOpenedFile
        {
            get; set;
        }

        [Category("General|Application")]
        public OpenType OpenAtStartup { get; set; }

        [Category("External Tools|")]
        [Comment]
//        [DisplayName(" ")]
        [XmlIgnore]
        [HeaderPlacement(HeaderPlacement.Collapsed)]
        public string Comment3
        {
            get { return "You can specifiy up to three external tools. Please specify command lines to run the tools:"; }
            set { string dummy = value; }  //needed becuase ShowReadOnlyProperties="False"
        }

        [Category("External Tools|Tool 1")]
        [DisplayName("File name: ")]
        public string ExternalTool1Filename { get; set; }

        [Category("External Tools|Tool 1")]
        [DisplayName("Arguments: ")]
        public string ExternalTool1Arguments { get; set; }

        [Category("External Tools|Tool 2")]
        [DisplayName("File name: ")]
        public string ExternalTool2Filename { get; set; }

        [Category("External Tools|Tool 2")]
        [DisplayName("Arguments: ")]
        public string ExternalTool2Arguments { get; set; }
        
        [Category("External Tools|Tool 3")]
        [DisplayName("File name: ")]
        public string ExternalTool3Filename { get; set; }

        [Category("External Tools|Tool 3")]
        [DisplayName("Arguments: ")]
        public string ExternalTool3Arguments { get; set; }

        [Category("External Tools|")]
        [Comment]
//        [DisplayName(" ")]
        [HeaderPlacement(HeaderPlacement.Collapsed)]
        [XmlIgnore]
        public string Comment2
        {
            get
            {
                return "Placeholder:\n" +
                       "$(ItemPath) The complete file name with path of the current file\n" +
                       "$(ItemDir) The directory of the current file\n" +
                       "$(ItemFilename) The file name of the current file\n" +
                       "$(ItemExt) The file name extension of the current file\n" +
                       "$(ItemFilenameWithoutExt) The file name without extension of the current file\n" +
                       "$(CurText) The selected text\n" +
                       "$(CurLine) The current line position of the cursor\n" +
                       "$(CurCol) The current column position of the cursor";
            }
            set { string dummy = value; }  //needed becuase ShowReadOnlyProperties="False"
        }

        [Browsable(false)]
        public List<string> RecentlyUsedFiles  //cannot use IList due to serialization
        {
            get { return _recentlyUsedFiles; }
        }

        [Browsable(false)]
        public string TcpListenerPort
        {
            get; set;
        }

        [Browsable(false)]
        public string TcpListenerEncoding
        {
            get; set;
        }

        [Browsable(false)]
        public string TcpListenerNic
        {
            get; set;
        }


        public void AddRecentlyUsedFile(string file)
        {
            if (!_recentlyUsedFiles.Contains(file))
            {
                _recentlyUsedFiles.Add(file);
            }
            while (_recentlyUsedFiles.Count > MAX_RECENTLY_USED_FILES)
            {
                _recentlyUsedFiles.RemoveAt(0);
            }
        }


    }
}
