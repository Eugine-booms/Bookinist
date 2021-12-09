using Bookinist.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.Services.Interfaces
{
    public interface IUserDialog
    {
        bool ConfirmInformation(string Information, string Caption);
        bool Edit(Book book);
        bool Error(string Information, string Caption);
        bool Warninng(string Information, string Caption);
    }
}
