using System;
using System.Linq;
using System.Windows;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Services
{
    public class WPFUIVisualizerService : IUIVisualizerService
    {
    	ILog Logger = Log.Get(typeof(WPFMessageBoxService));
    	
    	Guid id;
    	
    	public Guid Id {
    		get { return id; }
    		set { 
    				id = value; 
    		}
    	}
    	
		public WPFUIVisualizerService()
		{
			Logger.InstanceCreated();
			Id = Guid.NewGuid();
		}
 
        #region Public Methods
        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="dataContextForPopup">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public bool? ShowDialog(object dataContextForPopup)
        {
            Window win = new Window();
            win.DataContext = dataContextForPopup;
            win.Owner = Application.Current.MainWindow;
            if (win != null)
                return win.ShowDialog();

            return false;
        }
        #endregion

		public override string ToString()
		{
			return string.Format("[WPFUIVisualizerService Id={0}]", id);
		}

    }
}
