using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public interface IFillable
    {
        string TableName { get; set; }

        void Fill(object[] dataObjects);
    }
}
