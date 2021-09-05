using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DemoApp
{
    public class WPFUIVisualizerService : IUIVisualizerService
    {
 
        #region Public Methods
        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="dataContextForPopup">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public bool? ShowDialog(object dataContextForPopup)
        {
            Window win = new PopupWindow();
            win.DataContext = dataContextForPopup;
            win.Owner = Application.Current.MainWindow;
            if (win != null)
                return win.ShowDialog();

            return false;
        }

        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="dataContextForPopup">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public CD2C.Common.DataMemberModel ShowDataMemberPopup(object dataContextForPopup)
        {
            PopupDataMember win = new PopupDataMember();
            win.DataContext = dataContextForPopup;
            win.Owner = Application.Current.MainWindow;

            CD2C.Common.DataMemberModel result = null;

            if (win != null)
            {
                win.ShowDialog();

                result = win.Result;


            }

            return result;
        }

        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="dataContextForPopup">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public CD2C.Common.MethodModel ShowMethodPopup(object dataContextForPopup)
        {
            PopupMethod win = new PopupMethod();
            win.DataContext = dataContextForPopup;
            win.Owner = Application.Current.MainWindow;

            CD2C.Common.MethodModel result = null;

            if (win != null)
            {
                win.ShowDialog();

                result = win.Result;


            }

            return result;
        }

        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="dataContextForPopup">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public KeyValuePair<string, TypeEnum> ShowInputParameterPopup(object dataContextForPopup)
        {
            PopupInputParameter win = new PopupInputParameter();
            win.DataContext = dataContextForPopup;
            win.Owner = Application.Current.MainWindow;

            KeyValuePair<string, TypeEnum> result = default;

            if (win != null)
            {
                win.ShowDialog();

                result = win.Result;


            }

            return result;
        }
        #endregion


    }
}
