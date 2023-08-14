using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jhjo.Common;

namespace FourHeadScriber
{
    public partial class frmPassword : Form
    {
        #region CONSTRUCTOR & DESTRUCTOR
        public frmPassword()
        {
            InitializeComponent();
        }
        #endregion


        #region EVENT
        #region BUTTON EVENT
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.TxtPassword.Text.Trim()) == true)
                {
                    CMsgBox.Warning("Please input password!");
                }

                //int I32Password = 0;
                if (this.TxtPassword.Text == "1111" || this.TxtPassword.Text == "bmdt" || this.TxtPassword.Text == "truly")
                { //int.TryParse(this.TxtPassword.Text.Trim(), out I32Password) == true || 
                    if (this.TxtPassword.Text == "1111" || this.TxtPassword.Text == "bmdt" || this.TxtPassword.Text == "truly")
                    { //I32Password == DateTime.Now.Hour + DateTime.Now.Minute || 
                        base.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        CMsgBox.Warning("Please input correct password!");
                    }
                }
                else
                {
                    CMsgBox.Warning("Please input correct password!");
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }

        #endregion

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion
    }
}
