using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jhjo.Common;

namespace FourHeadScriber
{
    public partial class UcScreen : UserControl
    {
        #region DELEGATE & EVENT
        public delegate void ScreenFixedHandler(bool BFixed);
        public event ScreenFixedHandler ScreenFixed = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public UcScreen()
        {
            InitializeComponent();
        }
        #endregion


        #region FUNCTION
        public virtual void Add() { }


        public virtual void Remove() { }


        public virtual void NotifyRecipeChanged() { }


        protected void OnScreenFixed(bool BFixed)
        {
            try
            {
                if (this.ScreenFixed != null)
                {
                    this.ScreenFixed(BFixed);
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion
    }
}
