using EASoundbankTools.Model;
using EASoundbankTools.Parser;

namespace MaddenMixer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openSoundFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SoundbankParser parser = new SoundbankParser();
                    ISoundbank soundbank = parser.ParseSbrStandalone(openSoundFileDialog.FileName);
                    soundbankEntryBindingSource.DataSource = soundbank.Entries;

                    lbl_Status.Text = "Valid";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    soundbankEntryBindingSource.DataSource = null;

                    lbl_Status.Text = "X Invalid";
                }
                finally
                {
                    lbl_FileName.Text = openSoundFileDialog.FileName;
                    lbl_FileName.Show();
                }
            }
        }
    }
}