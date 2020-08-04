using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Gosol.CMS.Utility;

namespace Com.Gosol.CMS.Web
{
    public partial class EncryptTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
            }
        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            String encryptKey = Constant.EncryptKey;
            String originalText = txtOriginalText.Text.Trim();

            String encryptText = new AES().Encrypt(originalText, encryptKey);
            lblEncryptText.Text = encryptText;
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            String encryptText = lblEncryptText.Text;

            if (encryptText != String.Empty)
            {
                String decryptText = new AES().Decrypt(encryptText, Constant.EncryptKey);
                lblDecryptText.Text = decryptText;
            }
        }
    }
}