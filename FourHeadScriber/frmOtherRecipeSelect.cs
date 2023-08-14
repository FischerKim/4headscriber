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
    public partial class frmOtherRecipeSelect : Form
    {
        #region VARIABLE
        private ESTAGE_ANGLE m_EAngle = ESTAGE_ANGLE.NONE;
        private EVIEW m_EView = EVIEW.NONE;

        private bool m_BZeroHead1 = true;
        private bool m_BZeroHead2 = true;
        private bool m_BZeroHead3 = true;
        private bool m_BZeroHead4 = true;
        private bool m_BNinetyHead1 = true;
        private bool m_BNinetyHead2 = true;
        private bool m_BNinetyHead3 = true;
        private bool m_BNinetyHead4 = true;

        private bool m_BPreventEvent = false;
        #endregion


        #region PROPERTIES
        public ESTAGE_ANGLE EAngle
        {
            set { this.m_EAngle = value; }
        }


        public EVIEW EView
        {
            set { this.m_EView = value; }
        }


        public bool BZeroHead1 
        {
            get { return this.m_BZeroHead1; }
        }


        public bool BZeroHead2 
        { 
            get { return this.m_BZeroHead2; }
        }


        public bool BZeroHead3 
        { 
            get { return this.m_BZeroHead3; } 
        }


        public bool BZeroHead4 
        { 
            get { return this.m_BZeroHead4; }
        }


        public bool BNinetyHead1 
        { 
            get { return this.m_BNinetyHead1; }
        }


        public bool BNinetyHead2
        { 
            get { return this.m_BNinetyHead2; }
        }


        public bool BNinetyHead3 
        { 
            get { return this.m_BNinetyHead3; }
        }


        public bool BNinetyHead4 
        { 
            get { return this.m_BNinetyHead4; } 
        }
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public frmOtherRecipeSelect()
        {
            InitializeComponent();
        }
        #endregion
            

        #region EVENT
        #region FORM EVENT
        private void frmOtherRecipeSelect_Load(object sender, EventArgs e)
        {
            try
            {
                this.m_BPreventEvent = true;

                if (this.m_EAngle == ESTAGE_ANGLE.ZERO)
                {
                    if (this.m_EView == EVIEW.HEAD1)
                    {
                        this.m_BZeroHead1 = false;

                        this.ChkZeroHead1.Text = "Not Select";
                        this.ChkZeroHead1.Checked = false;
                        this.ChkZeroHead1.BackColor = SystemColors.Control;
                        this.ChkZeroHead1.Enabled = false;
                    }
                    else if (this.m_EView == EVIEW.HEAD2)
                    {
                        this.m_BZeroHead2 = false;

                        this.ChkZeroHead2.Text = "Not Select";
                        this.ChkZeroHead2.Checked = false;
                        this.ChkZeroHead2.BackColor = SystemColors.Control;
                        this.ChkZeroHead2.Enabled = false;
                    }
                    else if (this.m_EView == EVIEW.HEAD3)
                    {
                        this.m_BZeroHead3 = false;

                        this.ChkZeroHead3.Text = "Not Select";
                        this.ChkZeroHead3.Checked = false;
                        this.ChkZeroHead3.BackColor = SystemColors.Control;
                        this.ChkZeroHead3.Enabled = false;
                    }
                    else if (this.m_EView == EVIEW.HEAD4)
                    {
                        this.m_BZeroHead4 = false;

                        this.ChkZeroHead4.Text = "Not Select";
                        this.ChkZeroHead4.Checked = false;
                        this.ChkZeroHead4.BackColor = SystemColors.Control;
                        this.ChkZeroHead4.Enabled = false;
                    }
                }
                else if (this.m_EAngle == ESTAGE_ANGLE.NINETY)
                {
                    if (this.m_EView == EVIEW.HEAD1)
                    {
                        this.m_BNinetyHead1 = false;

                        this.ChkNinetyHead1.Text = "Not Select";
                        this.ChkNinetyHead1.Checked = false;
                        this.ChkNinetyHead1.BackColor = SystemColors.Control;
                        this.ChkNinetyHead1.Enabled = false;
                    }
                    else if (this.m_EView == EVIEW.HEAD2)
                    {
                        this.m_BNinetyHead2 = false;

                        this.ChkNinetyHead2.Text = "Not Select";
                        this.ChkNinetyHead2.Checked = false;
                        this.ChkNinetyHead2.BackColor = SystemColors.Control;
                        this.ChkNinetyHead2.Enabled = false;
                    }
                    else if (this.m_EView == EVIEW.HEAD3)
                    {
                        this.m_BNinetyHead3 = false;

                        this.ChkNinetyHead3.Text = "Not Select";
                        this.ChkNinetyHead3.Checked = false;
                        this.ChkNinetyHead3.BackColor = SystemColors.Control;
                        this.ChkNinetyHead3.Enabled = false;
                    }
                    else if (this.m_EView == EVIEW.HEAD4)
                    {
                        this.m_BNinetyHead4 = false;

                        this.ChkNinetyHead4.Text = "Not Select";
                        this.ChkNinetyHead4.Checked = false;
                        this.ChkNinetyHead4.BackColor = SystemColors.Control;
                        this.ChkNinetyHead4.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
            finally
            {
                this.m_BPreventEvent = false;
            }
        }
        #endregion


        #region BUTTON EVENT
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string StrMsg = "Do you want to overwrite the other recipe selected, Really?" + "\n"
                              + "(It means will be disappered context of selected recipe.)";

                if (CMsgBox.OKCancel(StrMsg) == DialogResult.OK)
                {
                    base.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion


        #region ETC EVENT
        private void ChkSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_BPreventEvent == true) return;
            
            try
            {
                CheckBox OControl = (CheckBox)sender;
                if (OControl.Checked == true)
                {
                    OControl.Text = "Select";
                    OControl.BackColor = Color.ForestGreen;
                }
                else if (OControl.Checked == false)
                {
                    OControl.Text = "Not Select";
                    OControl.BackColor = Color.SteelBlue;
                }


                string StrTag = (string)OControl.Tag;
                switch (StrTag)
                {
                    case "ZERO_HEAD1":
                        this.m_BZeroHead1 = OControl.Checked;
                        break;

                    case "ZERO_HEAD2":
                        this.m_BZeroHead2 = OControl.Checked;
                        break;

                    case "ZERO_HEAD3": 
                        this.m_BZeroHead3 = OControl.Checked; 
                        break;

                    case "ZERO_HEAD4": 
                        this.m_BZeroHead4 = OControl.Checked; 
                        break;

                    case "NINETY_HEAD1": 
                        this.m_BNinetyHead1 = OControl.Checked; 
                        break;

                    case "NINETY_HEAD2": 
                        this.m_BNinetyHead2 = OControl.Checked; 
                        break;
                        
                    case "NINETY_HEAD3":
                        this.m_BNinetyHead3 = OControl.Checked;
                        break;

                    case "NINETY_HEAD4":
                        this.m_BNinetyHead4 = OControl.Checked;
                        break;
                }
            }
            catch (Exception ex)
            {
                CError.Show(ex);
            }
        }
        #endregion
        #endregion
    }
}
