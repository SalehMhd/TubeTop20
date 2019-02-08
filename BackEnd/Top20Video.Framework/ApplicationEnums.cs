using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top20Video.Framework
{
    /// <summary>
    /// Enumerator 
    /// </summary>
    public enum MessageStatus
    {
        Success = 0,
        Error = 1,
        Info = 2
    }

    /// <summary>
    /// Enumerator
    /// </summary>
    public enum FileType
    {
        ProfilePic,
        Image,
        Video,
        Document,
        Logo
    }
 

    /// <summary>
    /// Enumerator
    /// </summary>
    public enum Status
    {
        Deleted = 0,
        Active = 1,
        
    }

    public enum ThumbnailType
    {
        Default,
        Medium,
        High,
        Standard,
        HD
    }

    
}

