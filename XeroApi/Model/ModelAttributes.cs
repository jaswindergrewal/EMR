using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XeroApi.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ItemIdAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ItemNumberAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ItemUpdatedDateAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReadOnlyAttribute : Attribute
    {
        
    }

    
    public interface IAttachmentParent
    {
    }
  
}
