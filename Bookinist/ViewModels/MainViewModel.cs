using MathCore.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.ViewModels
{
  public  class MainViewModel: ViewModel
    {


        #region  string Title Заголовок
        ///<summary> Заголовок
        private string _Title ="Заголовок окна";
        ///<summary> Заголовок
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value, nameof(Title));
        }
        #endregion

    }
}
