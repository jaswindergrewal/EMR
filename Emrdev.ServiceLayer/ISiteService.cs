﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISite" in both code and config file together.
    [ServiceContract]
    public interface ISiteService
    {
        [OperationContract]
        dynamic GetAllPatients(string prefixText);
    }
}
